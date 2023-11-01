using System;
using System.Collections.Generic;
using DemonCastle.Editor.Editors.Components;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using Godot;

namespace DemonCastle.Editor.Editors.SpriteAtlas.Details.Sprites;

public partial class SpriteSelectorPanel : HFlowContainer {
	private readonly List<SelectableSprite> _selection = new();
	private ISpriteDefinition? _spriteDefinition;

	public SpriteAtlasInfo SpriteAtlas { get; }

	public ISpriteDefinition? SpriteDefinition {
		get => _spriteDefinition;
		set {
			if (_spriteDefinition == value) return;
			_spriteDefinition = value;
			SelectSpriteDefinition(value);
		}
	}

	public event Action<ISpriteDefinition?>? SpriteSelected;

	public SpriteSelectorPanel(SpriteAtlasInfo spriteAtlas) {
		SpriteAtlas = spriteAtlas;
		Reload();
	}

	private void SelectSpriteDefinition(ISpriteDefinition? spriteDefinition) {
		foreach (var selectableSprite in _selection) {
			selectableSprite.IsSelected = selectableSprite.SpriteDefinition == spriteDefinition;
		}
		SpriteSelected?.Invoke(SpriteDefinition);
	}

	public void Reload() {
		foreach (var node in GetChildren()) {
			node.QueueFree();
		}
		_selection.Clear();

		foreach (var sprite in SpriteAtlas.Sprites) {
			var c = new SelectableSprite(sprite);
			AddChild(c);
			_selection.Add(c);
			c.Selected += OnTileSelected;
		}
	}

	private void OnTileSelected(SelectableControl selection) {
		if (selection is not SelectableSprite selectableSprite) return;
		SpriteDefinition = selectableSprite.SpriteDefinition;
		SpriteSelected?.Invoke(SpriteDefinition);
	}
}