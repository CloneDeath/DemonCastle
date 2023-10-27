using DemonCastle.Editor.Editors.Level.Area;
using DemonCastle.Editor.Editors.Level.TileMap;
using DemonCastle.Editor.Editors.Properties;
using DemonCastle.Editor.Extensions;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Editors.Level.LevelOverview;

public partial class LevelDetailsPanel : VBoxContainer {
	protected LevelInfo LevelInfo { get; }

	protected PropertyCollection Properties { get; }
	protected Button AddAreaButton { get; }
	protected Button AddTileButton { get; }
	protected Button EditTileButton { get; }
	protected Button DeleteTileButton { get; }
	protected TileSelectorPanel TileSelector { get; }

	public LevelDetailsPanel(LevelInfo levelInfo) {
		LevelInfo = levelInfo;

		AddChild(Properties = new PropertyCollection());
		Properties.AddString("Name", levelInfo, x => x.Name);
		Properties.AddVector2I("Tile Size", levelInfo, x => x.TileSize);
		Properties.AddVector2I("Area Size", levelInfo, x => x.AreaSize);

		AddChild(AddAreaButton = new Button { Text = "Add Area" });
		AddAreaButton.Pressed += AddAreaButtonOnPressed;

		AddChild(AddTileButton = new Button { Text = "Add Tile" });
		AddTileButton.Pressed += AddTileButtonOnPressed;

		AddChild(EditTileButton = new Button { Text = "Edit Tile" });
		EditTileButton.Pressed += EditTileButtonOnPressed;

		AddChild(DeleteTileButton = new Button { Text = "Delete Tile" });
		DeleteTileButton.Pressed += DeleteTileButtonOnPressed;

		AddChild(TileSelector = new TileSelectorPanel(levelInfo.TileSet));
	}

	public override void _Process(double delta) {
		base._Process(delta);

		var itemSelected = TileSelector.SelectedTile != null;
		EditTileButton.Disabled = !itemSelected;
		DeleteTileButton.Disabled = !itemSelected;
	}

	private void AddAreaButtonOnPressed() {
		var area = LevelInfo.CreateArea();
		var editor = new AreaEditor(area);
		this.GetEditArea().ShowEditor(editor);
	}

	private void AddTileButtonOnPressed() {
		var tile = LevelInfo.TileSet.CreateTile();
		var editor = new TileEditor(tile);
		this.GetEditArea().ShowEditor(editor);
		TileSelector.Reload();
	}

	private void EditTileButtonOnPressed() {
		var tile = TileSelector.SelectedTile;
		if (tile == null) return;
		var window = new TileEditor(tile);
		this.GetEditArea().ShowEditor(window);
	}

	private void DeleteTileButtonOnPressed() {
		var tile = TileSelector.SelectedTile;
		if (tile == null) return;
		LevelInfo.TileSet.DeleteTile(tile);
		TileSelector.Reload();
	}
}