using DemonCastle.ProjectFiles.Projects.Data.Levels.Tiles;
using Godot;

namespace DemonCastle.Editor.Editors.TileSet.Tiles;

public class TileStairsProxy : InfoProxy<StairInfo> {
	protected override void NotifyProxyChanged() {
		OnPropertyChanged(nameof(Enabled));
		OnPropertyChanged(nameof(Start));
		OnPropertyChanged(nameof(End));
	}

	public bool Enabled {
		get => Proxy?.Enabled ?? false;
		set {
			if (Proxy != null) Proxy.Enabled = value;
		}
	}

	public Vector2 Start {
		get => Proxy?.Start ?? Vector2.Down;
		set {
			if (Proxy != null) Proxy.Start = value;
		}
	}

	public Vector2 End {
		get => Proxy?.End ?? Vector2.Down;
		set {
			if (Proxy != null) Proxy.End = value;
		}
	}
}