using DemonCastle.Editor.Editors.Level.Area.AreaTiles;
using DemonCastle.Editor.Editors.Level.Area.Details;
using DemonCastle.Editor.Editors.Level.Area.TileTools;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area;

public partial class AreaEdit : HSplitContainer {
	protected AreaDetails Details { get; }

	protected VBoxContainer LeftPanel { get; }
	protected LevelAreasView RightPanel { get; }

	public AreaEdit(LevelInfo level) {
		Name = nameof(AreaEdit);

		AddChild(LeftPanel = new VBoxContainer());
		LeftPanel.AddChild(Details = new AreaDetails());
		LeftPanel.AddChild(new TileToolsPanel(level));
		AddChild(RightPanel = new LevelAreasView(level));
	}

	public AreaInfo? SelectedArea {
		get => Details.Proxy;
		set {
			Details.Proxy = value;
			if (value == null) {
				RightPanel.DeselectAllAreas();
			}
			else {
				RightPanel.SelectArea(value);
			}
		}
	}
}