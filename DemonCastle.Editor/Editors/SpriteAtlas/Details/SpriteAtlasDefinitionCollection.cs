using System;
using DemonCastle.Editor.Editors.Components;
using DemonCastle.Editor.Editors.SpriteAtlas.Details.Sprites;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using Godot;

namespace DemonCastle.Editor.Editors.SpriteAtlas.Details;

public partial class SpriteAtlasDefinitionCollection : VBoxContainer {
	private readonly SpriteAtlasDataInfoProxy _proxy = new();
	private readonly SpriteAtlasInfo _spriteAtlas;

	protected Button AddSpriteButton { get; }
	protected SpriteSelectorPanel SpriteSelector { get; }
	protected SpriteAtlasDataDetails SpriteAtlasDataDetails { get; }
	private readonly Button DeleteButton;

	public event Action<SpriteAtlasDataInfo?>? SpriteSelected;

	public SpriteAtlasDefinitionCollection(SpriteAtlasInfo spriteAtlas) {
		_spriteAtlas = spriteAtlas;

		Name = nameof(SpriteAtlasDefinitionCollection);

		AddChild(AddSpriteButton = new Button {
			Text = "Add Sprite"
		});
		AddSpriteButton.Pressed += AddSpriteButton_OnPressed;

		AddChild(SpriteSelector = new SpriteSelectorPanel(spriteAtlas) {
			SizeFlagsVertical = SizeFlags.ExpandFill
		});
		SpriteSelector.SpriteSelected += SpriteSelector_OnSpriteSelected;

		AddChild(SpriteAtlasDataDetails = new SpriteAtlasDataDetails(_proxy));
		SpriteAtlasDataDetails.Disable();

		AddChild(DeleteButton = new Button {
			Text = "Delete Sprite"
		});
		DeleteButton.Pressed += DeleteButton_OnPressed;
	}

	public void SelectSprite(SpriteAtlasDataInfo? sprite) {
		_proxy.Proxy = sprite;
		if (sprite != null) SpriteAtlasDataDetails.Enable();
		else SpriteAtlasDataDetails.Disable();
		SpriteSelector.SelectSprite(sprite);
	}

	private void SpriteSelector_OnSpriteSelected(ISpriteDefinition? sprite) {
		_proxy.Proxy = sprite as SpriteAtlasDataInfo;
		if (sprite != null) {
			SpriteAtlasDataDetails.Enable();
			DeleteButton.Disabled = false;
		}
		else {
			SpriteAtlasDataDetails.Disable();
			DeleteButton.Disabled = true;
		}
		SpriteSelected?.Invoke(sprite as SpriteAtlasDataInfo);
	}

	private void AddSpriteButton_OnPressed() {
		var sprite = _spriteAtlas.AtlasSprites.AppendNew();
		SelectSprite(sprite);
		SpriteSelected?.Invoke(sprite);
	}

	private void DeleteButton_OnPressed() {
		var selectedSprite = _proxy.Proxy;
		if (selectedSprite == null) return;
		_spriteAtlas.AtlasSprites.Remove(selectedSprite);
		SelectSprite(null);
		SpriteSelected?.Invoke(null);
	}
}