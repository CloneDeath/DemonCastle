using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Levels;

public class LevelInfo : FileInfo<LevelFile>, IListableInfo, INotifyPropertyChanged {
	private readonly List<AreaInfo> _areas;

	public LevelInfo(FileNavigator<LevelFile> file) : base(file) {
		TileSet = new LevelTileSet(file.Resource, File);
		_areas = Resource.Areas.Select(area => new AreaInfo(area, this)).ToList();
	}

	public LevelTileSet TileSet { get; }

	public Vector2I TileSize {
		get => new(Resource.TileWidth, Resource.TileHeight);
		set {
			Resource.TileWidth = value.X;
			Resource.TileHeight = value.Y;
			Save();
			OnPropertyChanged();
		}
	}

	public Vector2I AreaSize {
		get => new(Resource.AreaWidth, Resource.AreaHeight);
		set {
			Resource.AreaWidth = value.X;
			Resource.AreaHeight = value.Y;
			Save();
			OnPropertyChanged();
		}
	}

	public IEnumerable<AreaInfo> Areas => _areas;

	public Vector2 StartingLocation => TileSize * (
													  GetAreaByName(Resource.StartingPosition.Area).TilePosition
													  + new Vector2(Resource.StartingPosition.X,
														  Resource.StartingPosition.Y)
												  ) + TileSize / new Vector2(1 / 2f, 1);

	public string Name {
		get => Resource.Name;
		set {
			Resource.Name = value;
			Save();
			OnPropertyChanged();
		}
	}

	public AreaInfo CreateArea() {
		var area = new AreaData();
		Resource.Areas.Add(area);
		_areas.Add(new AreaInfo(area, this));
		OnPropertyChanged(nameof(Areas));
		return new AreaInfo(area, this);
	}

	private AreaInfo GetAreaByName(string name) {
		var area = Resource.Areas.First(a => a.Name == name);
		return new AreaInfo(area, this);
	}

	public TileInfo GetTileInfo(string tileName) => TileSet.GetTileInfo(tileName);

	#region INotifyPropertyChanged
	public event PropertyChangedEventHandler? PropertyChanged;

	protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null) {
		if (EqualityComparer<T>.Default.Equals(field, value)) return false;
		field = value;
		OnPropertyChanged(propertyName);
		return true;
	}
	#endregion
}