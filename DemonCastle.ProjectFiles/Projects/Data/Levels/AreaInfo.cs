using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Levels;

public class AreaInfo : INotifyPropertyChanged {
	public AreaInfo(AreaData area, LevelInfo levelInfo) {
		Area = area;
		LevelInfo = levelInfo;
	}

	protected AreaData Area { get; }
	public LevelInfo LevelInfo { get; }

	public string Name {
		get => Area.Name;
		set {
			Area.Name = value;
			LevelInfo.Save();
			OnPropertyChanged();
		}
	}

	public LevelTileSet LevelTileSet => LevelInfo.TileSet;
	public IEnumerable<TileMapInfo> TileMap => Area.TileMap.Select(tm => new TileMapInfo(tm, this));

	public Vector2I AreaPosition {
		get => new(Area.X, Area.Y);
		set {
			Area.X = value.X;
			Area.Y = value.Y;
			LevelInfo.Save();
			OnPropertyChanged();
		}
	}

	public Vector2I Size {
		get => new(Area.Width, Area.Height);
		set {
			Area.Width = value.X;
			Area.Height = value.Y;
			LevelInfo.Save();
			OnPropertyChanged();
		}
	}

	public Vector2I TilePosition => LevelInfo.TileSize * AreaPosition * LevelInfo.AreaSize;

	public Vector2I TileSize => LevelInfo.TileSize;
	public Vector2I AreaSize => LevelInfo.AreaSize;
	public LevelTileSet TileSet => LevelInfo.TileSet;

	public TileInfo GetTileInfo(string tileName) => LevelInfo.GetTileInfo(tileName);

	public void SetTile(Vector2I position, string tileName) {
		var tile = Area.TileMap.FirstOrDefault(t => t.X == position.X && t.Y == position.Y);
		if (tile != null) {
			tile.Tile = tileName;
		}
		else {
			Area.TileMap.Add(new TileMapData {
				X = position.X,
				Y = position.Y,
				Tile = tileName
			});
		}

		LevelInfo.Save();
	}

	public void ClearTile(Vector2I position) {
		var tile = Area.TileMap.FirstOrDefault(t => t.X == position.X && t.Y == position.Y);
		if (tile == null) return;

		Area.TileMap.Remove(tile);
		LevelInfo.Save();
	}

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