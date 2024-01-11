using System;
using DemonCastle.Editor.Editors.Components;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using Godot;

namespace DemonCastle.Editor.Editors.SpriteGrid.Details;

public partial class SpriteGridDefinitionCollection : VBoxContainer {
	private readonly SpriteGridInfo _spriteGrid;

	protected Button AddSpriteButton { get; }
	protected SpriteSelectorPanel SpriteSelector { get; }

	public event Action<SpriteGridDataInfo?>? SpriteSelected;

	public SpriteGridDefinitionCollection(SpriteGridInfo spriteGrid) {
		_spriteGrid = spriteGrid;

		Name = nameof(SpriteGridDefinitionCollection);

		AddChild(AddSpriteButton = new Button {
			Text = "Add Sprite"
		});
		AddSpriteButton.Pressed += AddSpriteButton_OnPressed;

		AddChild(SpriteSelector = new SpriteSelectorPanel(spriteGrid) {
			SizeFlagsVertical = SizeFlags.ExpandFill
		});
		SpriteSelector.SpriteSelected += SpriteSelector_OnSpriteSelected;
	}

	public void SelectSprite(SpriteGridDataInfo? sprite) {
		SpriteSelected?.Invoke(sprite);
		SpriteSelector.SelectSprite(sprite);
	}

	private void SpriteSelector_OnSpriteSelected(ISpriteDefinition? sprite) {
		SpriteSelected?.Invoke(sprite as SpriteGridDataInfo);
	}

	private void AddSpriteButton_OnPressed() {
		var sprite = _spriteGrid.GridSprites.AppendNew();
		SelectSprite(sprite);
	}
}