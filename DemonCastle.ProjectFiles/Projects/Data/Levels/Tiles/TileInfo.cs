using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinitions;
using DemonCastle.ProjectFiles.Projects.Data.States;
using DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;
using TileData = DemonCastle.Files.TileData;

namespace DemonCastle.ProjectFiles.Projects.Data.Levels.Tiles;

public interface ITileInfo : IBaseEntityInfo {
	ISpriteDefinition Sprite { get; }
	Rect2 Region { get; }
	Vector2[] Collision { get; }
	IStairInfo Stairs { get; }
}

public class TileInfo : BaseEntityInfo<TileData>, ITileInfo {
	public TileInfo(IFileNavigator file, TileData tileData) : base(file, tileData) {
		Stairs = new StairInfo(file, tileData);
	}

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
		set => SaveField(ref Data.Collision, value.ToList());
	}

	IStairInfo ITileInfo.Stairs => Stairs;
	public StairInfo Stairs { get; }

	protected ISpriteSource Source => File.FileExists(SourceFile) ? File.GetSprite(SourceFile) : new NullSpriteSource();
	public ISpriteDefinition Sprite => Source.Sprites.FirstOrDefault(s => s.Id == SpriteId)
										  ?? new NullSpriteDefinition();
	public IEnumerableInfo<ISpriteDefinition> SpriteOptions => Source.Sprites;
	public Rect2 Region => Sprite.Region;
}

public class NullTileInfo : ITileInfo {
	public Guid Id { get; }

	public NullTileInfo(Guid id) {
		Id = id;
	}

	public ISpriteDefinition Sprite { get; } = new NullSpriteDefinition();
	public Rect2 Region => Sprite.Region;
	public Vector2[] Collision { get; } = Array.Empty<Vector2>();
	public IStairInfo Stairs { get; } = new NullStairInfo();
	public string Name { get; set; } = "<NULL>";
	public Guid InitialState { get; set; } = Guid.Empty;
	public Vector2I Size { get; } = Vector2I.One;
	public IAnimationInfoCollection Animations { get; } = new NullAnimationInfoCollection();
	public IEntityStateInfoCollection States { get; } = new NullEntityStateInfoCollection();
	public IVariableDeclarationInfoCollection Variables { get; } = new NullVariableDeclarationInfoCollection();
	public string ListLabel => Name;

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