using System;
using System.Collections.Generic;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using Godot;

namespace DemonCastle.Editor.Editors.Animations.Editor.Frames;

public class FrameInfoProxy : InfoProxy<FrameInfo>, IFrameInfo {
	protected override void NotifyProxyChanged() {
		OnPropertyChanged(nameof(Duration));
		OnPropertyChanged(nameof(Anchor));
		OnPropertyChanged(nameof(Offset));

		OnPropertyChanged(nameof(SourceFile));
		OnPropertyChanged(nameof(SpriteId));
		OnPropertyChanged(nameof(SpriteDefinition));

		OnPropertyChanged(nameof(SpriteDefinitions));
	}

	public float Duration {
		get => Proxy?.Duration ?? 0;
		set {
			if (Proxy != null) Proxy.Duration = value;
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

	public Vector2I Anchor {
		get => Proxy?.Anchor ?? Vector2I.Zero;
		set {
			if (Proxy != null) Proxy.Anchor = value;
		}
	}

	public Vector2I Offset {
		get => Proxy?.Offset ?? Vector2I.Zero;
		set {
			if (Proxy != null) Proxy.Offset = value;
		}
	}

	public ISpriteDefinition SpriteDefinition => Proxy?.SpriteDefinition ?? new NullSpriteDefinition();
	public IEnumerable<ISpriteDefinition> SpriteDefinitions => Proxy?.SpriteDefinitions ?? Array.Empty<ISpriteDefinition>();

	public void DeleteFrame() => Proxy?.Delete();
}