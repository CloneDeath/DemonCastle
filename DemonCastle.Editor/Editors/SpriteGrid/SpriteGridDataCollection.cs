using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using Godot;

namespace DemonCastle.Editor.Editors.SpriteGrid;

public partial class SpriteGridDataCollection : ScrollContainer {
	protected SpriteGridInfo SpriteGrid { get; }

	protected void ReloadSpriteData() {
		foreach (var child in SpriteCollection.GetChildren()) {
			child.QueueFree();
		}
		foreach (var data in SpriteGrid.GridSprites) {
			SpriteCollection.AddChild(new SpriteGridDataPanel(data));
		}
	}

	protected void OnAddSpriteDataButtonPressed() {
		SpriteGrid.CreateSprite();
		ReloadSpriteData();
	}
}