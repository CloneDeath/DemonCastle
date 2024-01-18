using System;
using System.Collections.Generic;
using System.Linq;
using DemonCastle.Files;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinitions;
using DemonCastle.ProjectFiles.Projects.Data.States;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;
using TileData = DemonCastle.Files.TileData;

namespace DemonCastle.ProjectFiles.Projects.Data.Levels.Tiles;

public class TileInfo : BaseEntityInfo<TileData> {
	protected FileNavigator<LevelFile> Level { get; }

	public TileInfo(FileNavigator<LevelFile> level, TileData tileData) : base(level, tileData) {
		Level = level;
	}

	public Vector2I TileSize => new(Level.Resource.TileWidth, Level.Resource.TileHeight);

	public string Directory => Level.Directory;

	public string SourceFile {
		get => PreviewFrame?.SourceFile ?? string.Empty;
		set {
			var frame = GetOrCreatePreviewFrame();
			frame.SourceFile = value;
			Save();
			OnPropertyChanged();
			OnPropertyChanged(nameof(Sprite));
		}
	}

	public Guid SpriteId {
		get => PreviewFrame?.SpriteId ?? Guid.Empty;
		set {
			var frame = GetOrCreatePreviewFrame();
			frame.SpriteId = value;
			Save();
			OnPropertyChanged();
			OnPropertyChanged(nameof(Sprite));
		}
	}

	private IFrameInfo GetOrCreatePreviewFrame() {
		if (PreviewFrame != null) return PreviewFrame;
		var animation = GetOrCreateInitialAnimation();
		var frame = animation.Frames.FirstOrDefault() ?? animation.Frames.AppendNew();
		return frame;
	}

	private IAnimationInfo GetOrCreateInitialAnimation() {
		var state = GetOrCreateInitialState();
		var animation = Animations.FirstOrDefault(a => a.Id == state.Animation) ??
						Animations.FirstOrDefault() ?? Animations.AppendNew();
		state.Animation = animation.Id;
		return animation;
	}

	private EntityStateInfo GetOrCreateInitialState() {
		var state = States.FirstOrDefault(s => s.Id == InitialState) ?? States.FirstOrDefault() ?? States.AppendNew();
		InitialState = state.Id;
		return state;
	}

	public Vector2[] Collision {
		get => Data.Collision.Select(c => new Vector2(c.X, c.Y)).ToArray();
		set {
			Data.Collision = value.ToList();
			Save();
			OnPropertyChanged();
		}
	}

	public StairData? Stairs {
		get => Data.Stairs;
		set {
			Data.Stairs = value;
			Save();
			OnPropertyChanged();
		}
	}

	protected ISpriteSource Source => Level.FileExists(SourceFile) ? Level.GetSprite(SourceFile) : new NullSpriteSource();
	public ISpriteDefinition Sprite => Source.Sprites.FirstOrDefault(s => s.Id == SpriteId)
										  ?? new NullSpriteDefinition();
	public IEnumerable<ISpriteDefinition> SpriteOptions => Source.Sprites;
	public Texture2D Texture => Sprite.Texture;
	public Rect2 Region => Sprite.Region;
	public bool FlipHorizontal => Sprite.FlipHorizontal;
}