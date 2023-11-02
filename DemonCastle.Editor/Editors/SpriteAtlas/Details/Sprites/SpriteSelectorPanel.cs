using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

	public override void _EnterTree() {
		base._EnterTree();
		SpriteAtlas.PropertyChanged += SpriteAtlas_OnPropertyChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		SpriteAtlas.PropertyChanged -= SpriteAtlas_OnPropertyChanged;
	}

	private void SpriteAtlas_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		if (e.PropertyName != nameof(SpriteAtlas.AtlasSprites)) return;

		var selected = GetSelectedSprite();
		Reload();
		SelectSpriteDefinition(selected);
	}

	private ISpriteDefinition? GetSelectedSprite() {
		return _selection.FirstOrDefault(s => s.IsSelected)?.SpriteDefinition;
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
			c.Selected += OnSpriteSelected;
		}
	}

	private void OnSpriteSelected(SelectableControl selection) {
		if (selection is not SelectableSprite selectableSprite) return;
		SpriteDefinition = selectableSprite.SpriteDefinition;
		SpriteSelected?.Invoke(SpriteDefinition);
	}
}