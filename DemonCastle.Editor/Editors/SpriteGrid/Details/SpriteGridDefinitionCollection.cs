using System;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using Godot;
using SpriteSelectorPanel = DemonCastle.Editor.Editors.Components.SpriteSelectorPanel;

namespace DemonCastle.Editor.Editors.SpriteGrid.Details;

public partial class SpriteGridDefinitionCollection : VBoxContainer {
	private readonly SpriteGridDataInfoProxy _proxy = new();
	private readonly SpriteGridInfo _spriteGrid;

	protected Button AddSpriteButton { get; }
	protected SpriteSelectorPanel SpriteSelector { get; }
	protected SpriteDetails SpriteDetails { get; }

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

		AddChild(SpriteDetails = new SpriteDetails(_proxy));
		SpriteDetails.Disable();
	}

	public void SelectSprite(SpriteGridDataInfo? sprite) {
		_proxy.Proxy = sprite;
		if (sprite != null) SpriteDetails.Enable();
		else SpriteDetails.Disable();
		SpriteSelector.SelectSprite(sprite);
	}

	private void SpriteSelector_OnSpriteSelected(ISpriteDefinition? sprite) {
		_proxy.Proxy = sprite as SpriteGridDataInfo;
		if (sprite != null) SpriteDetails.Enable();
		else SpriteDetails.Disable();
		SpriteSelected?.Invoke(sprite as SpriteGridDataInfo);
	}

	private void AddSpriteButton_OnPressed() {
		var sprite = _spriteGrid.CreateSprite();
		SelectSprite(sprite);
		SpriteSelected?.Invoke(sprite);
	}
}