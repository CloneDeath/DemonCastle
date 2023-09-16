using DemonCastle.Editor.Editors.Level.TileMap;
using DemonCastle.Editor.Extensions;
using DemonCastle.Editor.Icons;
using Godot;

namespace DemonCastle.Editor.Editors.Level; 

public partial class LevelEditor : BaseEditor {
	public override Texture2D TabIcon => IconTextures.LevelIcon;
	public override string TabText { get; }
	
	public override void _Process(double delta) {
		base._Process(delta);

		var itemSelected = TileSelector.SelectedTile != null;
		EditTileButton.Disabled = !itemSelected;
		DeleteTileButton.Disabled = !itemSelected;
	}

	private void AddAreaButtonOnPressed() {
		var area = LevelInfo.CreateArea();
		var editor = new Area.AreaEditor(area);
		this.GetEditArea().ShowEditor(editor);
		AreaEditor.Reload();
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