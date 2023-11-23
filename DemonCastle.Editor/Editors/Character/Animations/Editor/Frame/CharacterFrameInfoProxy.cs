using System;
using System.Collections.Generic;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using Godot;

namespace DemonCastle.Editor.Editors.Character.Animations.Editor.Frame;

public class CharacterFrameInfoProxy : InfoProxy<CharacterFrameInfo>, ICharacterFrameInfo {
	protected override void NotifyProxyChanged() {
		OnPropertyChanged(nameof(Duration));
		OnPropertyChanged(nameof(SourceFile));
		OnPropertyChanged(nameof(SpriteId));
		OnPropertyChanged(nameof(WeaponEnabled));
		OnPropertyChanged(nameof(WeaponAnimation));
		OnPropertyChanged(nameof(WeaponPosition));
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

	public bool WeaponEnabled => Proxy?.WeaponEnabled ?? false;
	public string WeaponAnimation => Proxy?.WeaponAnimation ?? string.Empty;
	public Vector2I WeaponPosition => Proxy?.WeaponPosition ?? Vector2I.Zero;

	public ISpriteDefinition SpriteDefinition => Proxy?.SpriteDefinition ?? new NullSpriteDefinition();
	public IEnumerable<ISpriteDefinition> SpriteDefinitions => Proxy?.SpriteDefinitions ?? Array.Empty<ISpriteDefinition>();

	public void DeleteFrame() => Proxy?.Delete();
}