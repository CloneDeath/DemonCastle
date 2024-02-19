using System;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Tiles;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinitions;
using Godot;

namespace DemonCastle.Editor.Editors.TileSet.Tiles;

public class TileProxy : InfoProxy<TileInfo> {
	protected override void NotifyProxyChanged() {
		Stairs.Proxy = Proxy?.Stairs;

		OnPropertyChanged(nameof(Name));
		OnPropertyChanged(nameof(SourceFile));
		OnPropertyChanged(nameof(SpriteId));
		OnPropertyChanged(nameof(SpriteOptions));
		OnPropertyChanged(nameof(Size));
		OnPropertyChanged(nameof(Collision));
		OnPropertyChanged(nameof(Stairs));
	}

	public string Name {
		get => Proxy?.Name ?? string.Empty;
		set {
			if (Proxy != null) Proxy.Name = value;
		}
	}

	public string SourceFile {
		get => Proxy?.SourceFile ?? string.Empty;
		set {
			if (Proxy != null) Proxy.SourceFile = value;
		}
	}

	public Guid SpriteId {
		get => Proxy?.SpriteId ?? Guid.Empty;
		set {
			if (Proxy != null) Proxy.SpriteId = value;
		}
	}

	public Vector2I Size {
		get => Proxy?.Size ?? Vector2I.One;
		set {
			if (Proxy != null) Proxy.Size = value;
		}
	}

	public Vector2[] Collision {
		get => Proxy?.Collision ?? Array.Empty<Vector2>();
		set {
			if (Proxy != null) Proxy.Collision = value;
		}
	}

	public TileStairsProxy Stairs { get; } = new();

	public Guid InitialState {
		get => Proxy?.InitialState ?? Guid.Empty;
		set {
			if (Proxy != null) Proxy.InitialState = value;
		}
	}

	public IEnumerableInfo<ISpriteDefinition> SpriteOptions => Proxy?.SpriteOptions ?? new NullEnumerableInfo<ISpriteDefinition>();
}