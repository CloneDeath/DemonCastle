using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Levels;

public class TileInfo : INotifyPropertyChanged {
	public TileInfo(FileNavigator<LevelFile> level, TileData tileData) {
		TileData = tileData;
		Level = level;
	}

	protected TileData TileData { get; }
	protected FileNavigator<LevelFile> Level { get; }
	public Vector2I TileSize => new(Level.Resource.TileWidth, Level.Resource.TileHeight);

	public string Directory => Level.Directory;

	public Guid Id => TileData.Id;

	public string Name {
		get => TileData.Name;
		set {
			TileData.Name = value;
			Save();
			OnPropertyChanged();
		}
	}

	public string SourceFile {
		get => TileData.Source;
		set {
			TileData.Source = value;
			Save();
			OnPropertyChanged();
		}
	}

	public Guid SpriteId {
		get => TileData.SpriteId;
		set {
			TileData.SpriteId = value;
			Save();
			OnPropertyChanged();
		}
	}

	protected ISpriteSource Source => Level.FileExists(SourceFile) ? Level.GetSprite(SourceFile) : new NullSpriteSource();
	public ISpriteDefinition Sprite => Source.Sprites.FirstOrDefault(s => s.Id == TileData.SpriteId)
										  ?? new NullSpriteDefinition();
	public IEnumerable<ISpriteDefinition> SpriteOptions => Source.Sprites;
	public Texture2D Texture => Sprite.Texture;
	public Rect2 Region => Sprite.Region;
	public Vector2[] Collision => TileData.Collision.Select(c => new Vector2(c.X, c.Y) * TileSize).ToArray();
	public bool FlipHorizontal => Sprite.FlipHorizontal;

	private void Save() => Level.Save();

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