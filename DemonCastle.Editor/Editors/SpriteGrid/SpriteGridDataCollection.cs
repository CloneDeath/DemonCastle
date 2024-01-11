using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using Godot;

namespace DemonCastle.Editor.Editors.SpriteGrid;

public partial class SpriteGridDataCollection : ScrollContainer {
	protected SpriteGridInfo SpriteGrid { get; }

	protected VBoxContainer SpriteCollection { get; }
	protected VBoxContainer Contents { get; }
	protected Button AddSpriteDataButton { get; }

	public SpriteGridDataCollection(SpriteGridInfo spriteGrid) {
		SpriteGrid = spriteGrid;

		Name = nameof(SpriteGridDataCollection);
		AnchorBottom = 1;
		SizeFlagsVertical = SizeFlags.ExpandFill;

		AddChild(Contents = new VBoxContainer {
			SizeFlagsHorizontal = SizeFlags.ExpandFill
		});

		Contents.AddChild(SpriteCollection = new VBoxContainer {
			AnchorRight = 1,
			SizeFlagsHorizontal = SizeFlags.ExpandFill
		});
		ReloadSpriteData();

		Contents.AddChild(AddSpriteDataButton = new Button {
			Text = "Add Sprite Data"
		});
		AddSpriteDataButton.Pressed += AddSpriteDataButton_OnPressed;
	}

	protected void ReloadSpriteData() {
		foreach (var child in SpriteCollection.GetChildren()) {
			child.QueueFree();
		}
		foreach (var data in SpriteGrid.GridSprites) {
			SpriteCollection.AddChild(new SpriteGridDataPanel(data));
		}
	}

	protected void AddSpriteDataButton_OnPressed() {
		SpriteGrid.CreateSprite();
		ReloadSpriteData();
	}
}