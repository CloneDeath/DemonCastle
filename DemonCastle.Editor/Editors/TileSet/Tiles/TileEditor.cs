using DemonCastle.Editor.Editors.Components.BaseEntity;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Tiles;
using Godot;

namespace DemonCastle.Editor.Editors.TileSet.Tiles;

public partial class TileEditor : HSplitContainer {
	protected TileDetails Details { get; }
	protected BaseEntityTabContainer Tabs { get; }

	public TileEditor(ProjectInfo project, IFileInfo tileSet) {
		Name = nameof(TileEditor);

		AddChild(Details = new TileDetails());
		AddChild(Tabs = new BaseEntityTabContainer(project, tileSet));
	}

	public void Load(TileInfo? tile) {
		Details.Tile = tile;
		Tabs.Load(tile);
	}
}