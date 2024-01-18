using DemonCastle.ProjectFiles.Projects.Data.Levels.Tiles;

namespace DemonCastle.Editor.Editors.Level.Area.Tools.TileTools.Layers;

public class TileLayerProxy : InfoProxy<TileMapLayerInfo> {
	protected override void NotifyProxyChanged() {
		OnPropertyChanged(nameof(ZIndex));
		OnPropertyChanged(nameof(Name));
	}

	public int ZIndex {
		get => Proxy?.ZIndex ?? 0;
		set { if (Proxy != null) Proxy.ZIndex = value; }
	}

	public string Name {
		get => Proxy?.Name ?? string.Empty;
		set { if (Proxy != null) Proxy.Name = value; }
	}
}