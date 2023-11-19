using System;
using System.Collections.Generic;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;

namespace DemonCastle.Editor.Editors.Weapon.Animations.Editor.Frames;

public class WeaponFrameInfoProxy : InfoProxy<WeaponFrameInfo> {
	protected override void NotifyProxyChanged() {
		OnPropertyChanged(nameof(Duration));
		OnPropertyChanged(nameof(SourceFile));
		OnPropertyChanged(nameof(SpriteId));
		OnPropertyChanged(nameof(Proxy.SpriteDefinition));
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

	public IEnumerable<ISpriteDefinition> SpriteDefinitions => Proxy?.SpriteDefinitions ?? Array.Empty<ISpriteDefinition>();

	public void DeleteFrame() => Proxy?.Delete();
}