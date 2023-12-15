using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.Details;

public class AreaProxy : InfoProxy<AreaInfo> {
	protected override void NotifyProxyChanged() {
		OnPropertyChanged(nameof(Name));
		OnPropertyChanged(nameof(AreaPosition));
		OnPropertyChanged(nameof(Size));
	}

	public string Name {
		get => Proxy?.Name ?? string.Empty;
		set {
			if (Proxy != null) Proxy.Name = value;
		}
	}

	public Vector2I AreaPosition {
		get => Proxy?.AreaPosition ?? Vector2I.Zero;
		set {
			if (Proxy != null) Proxy.AreaPosition = value;
		}
	}

	public Vector2I Size {
		get => Proxy?.Size ?? Vector2I.One;
		set {
			if (Proxy != null) Proxy.Size = value;
		}
	}
}