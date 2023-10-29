using DemonCastle.Editor.Editors.Level.Area.Details;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;
using TileToolsPanel = DemonCastle.Editor.Editors.Level.Area.TileTools.TileToolsPanel;

namespace DemonCastle.Editor.Editors.Level.Area;

public partial class AreaEdit : HSplitContainer {
	protected VBoxContainer LeftPanel { get; }
	protected AreaDetails Details { get; }

	public AreaEdit(LevelInfo level) {
		Name = nameof(AreaEdit);

		AddChild(LeftPanel = new VBoxContainer());
		LeftPanel.AddChild(Details = new AreaDetails());
		LeftPanel.AddChild(new TileToolsPanel(level));
		AddChild(new AreaTiles.LevelTilesView(level));
	}

	public AreaInfo? SelectedArea {
		get => Details.Proxy;
		set => Details.Proxy = value;
	}
}