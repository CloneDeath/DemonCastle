using System;
using System.Collections.Generic;
using System.Linq;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using Godot;

namespace DemonCastle.Editor.Editors.Animations.Editor.Frames;

public class FrameInfoProxy : InfoProxy<IFrameInfo>, IFrameInfo {
	protected override void NotifyProxyChanged() {
		OnPropertyChanged(nameof(Duration));
		OnPropertyChanged(nameof(Anchor));
		OnPropertyChanged(nameof(Offset));
		OnPropertyChanged(nameof(Origin));

		OnPropertyChanged(nameof(SourceFile));
		OnPropertyChanged(nameof(SpriteId));
		OnPropertyChanged(nameof(SpriteDefinition));

		OnPropertyChanged(nameof(WeaponEnabled));
		OnPropertyChanged(nameof(WeaponAnimation));
		OnPropertyChanged(nameof(WeaponPosition));

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

	public Vector2I Origin => Proxy?.Origin ?? Vector2I.Zero;

	public ISpriteDefinition SpriteDefinition => Proxy?.SpriteDefinition ?? new NullSpriteDefinition();
	public IEnumerable<ISpriteDefinition> SpriteDefinitions => Proxy?.SpriteDefinitions ?? Array.Empty<ISpriteDefinition>();
	public IEnumerableInfo<IFrameSlotInfo> Slots => Proxy?.Slots ?? new NullEnumerableInfo<IFrameSlotInfo>();

	public void Delete() => Proxy?.Delete();

	public bool WeaponEnabled {
		get {
			var slot = Proxy?.Slots.FirstOrDefault(s => s.Name == "Weapon");
			return slot != null;
		}
		set {
			if (Proxy == null) return;
			if (value) {
				var slot = Proxy.Slots.FirstOrDefault(s => s.Name == "Weapon");
				if (slot != null) return;
				slot = Proxy.Slots.AppendNew();
				slot.Name = "Weapon";
			} else {
				var slot = Proxy.Slots.FirstOrDefault(s => s.Name == "Weapon");
				if (slot == null) return;
				Proxy.Slots.Remove(slot);
			}
		}
	}

	public string WeaponAnimation {
		get {
			var slot = Proxy?.Slots.FirstOrDefault(s => s.Name == "Weapon");
			return slot?.Animation ?? string.Empty;
		}
		set {
			var slot = Proxy?.Slots.FirstOrDefault(s => s.Name == "Weapon");
			if (slot == null) return;
			slot.Animation = value;
		}
	}

	public Vector2I WeaponPosition {
		get {
			var slot = Proxy?.Slots.FirstOrDefault(s => s.Name == "Weapon");
			return slot?.Position ?? Vector2I.Zero;
		}
		set {
			var slot = Proxy?.Slots.FirstOrDefault(s => s.Name == "Weapon");
			if (slot == null) return;
			slot.Position = value;
		}
	}
}