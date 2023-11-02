using System;
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
	protected SpriteDetails SpriteDetails { get; }

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

		AddChild(SpriteDetails = new SpriteDetails(_proxy));
	}

	public void SelectSprite(SpriteAtlasDataInfo? sprite) {
		_proxy.Proxy = sprite;
		SpriteSelector.SelectSprite(sprite);
	}

	private void SpriteSelector_OnSpriteSelected(ISpriteDefinition? sprite) {
		_proxy.Proxy = sprite as SpriteAtlasDataInfo;
		SpriteSelected?.Invoke(sprite as SpriteAtlasDataInfo);
	}

	private void AddSpriteButton_OnPressed() {
		_spriteAtlas.CreateSprite();
	}
}