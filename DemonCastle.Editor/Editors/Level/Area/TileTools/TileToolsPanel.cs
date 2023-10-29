using DemonCastle.Editor.Extensions;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.TileTools;

public partial class TileToolsPanel : VBoxContainer {
	protected LevelInfo Level { get; }

	protected Button AddTileButton { get; }
	protected Button EditTileButton { get; }
	protected Button DeleteTileButton { get; }
	protected TileSelectorPanel TileSelector { get; }

	public TileToolsPanel(LevelInfo level) {
		Name = nameof(TileToolsPanel);
		Level = level;

		AddChild(AddTileButton = new Button { Text = "Add Tile" });
		AddTileButton.Pressed += AddTileButtonOnPressed;

		AddChild(EditTileButton = new Button { Text = "Edit Tile" });
		EditTileButton.Pressed += EditTileButtonOnPressed;

		AddChild(DeleteTileButton = new Button { Text = "Delete Tile" });
		DeleteTileButton.Pressed += DeleteTileButtonOnPressed;

		AddChild(TileSelector = new TileSelectorPanel(level.TileSet));
	}

	public override void _Process(double delta) {
		base._Process(delta);

		var itemSelected = TileSelector.SelectedTile != null;
		EditTileButton.Disabled = !itemSelected;
		DeleteTileButton.Disabled = !itemSelected;
	}

	private void AddTileButtonOnPressed() {
		var tile = Level.TileSet.CreateTile();
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
		Level.TileSet.DeleteTile(tile);
		TileSelector.Reload();
	}
}