using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using Godot;

namespace DemonCastle.Editor.Editors.Components;

public partial class SpriteSelectorPanel : HFlowContainer {
	private readonly List<SelectableSprite> _selection = new();
	private ISpriteDefinition? _spriteDefinition;

	public ISpriteSource SpriteSource { get; }

	public ISpriteDefinition? SpriteDefinition {
		get => _spriteDefinition;
		set {
			if (_spriteDefinition == value) return;
			_spriteDefinition = value;
			SelectSpriteDefinition(value);
		}
	}

	public event Action<ISpriteDefinition?>? SpriteSelected;

	public SpriteSelectorPanel(ISpriteSource spriteSource) {
		SpriteSource = spriteSource;
		Reload();
	}

	public override void _EnterTree() {
		base._EnterTree();
		if (SpriteSource is INotifyPropertyChanged notifySource) {
			notifySource.PropertyChanged += SpriteSource_OnPropertyChanged;
		}
	}

	public override void _ExitTree() {
		base._ExitTree();
		if (SpriteSource is INotifyPropertyChanged notifySource) {
			notifySource.PropertyChanged -= SpriteSource_OnPropertyChanged;
		}
	}

	private void SpriteSource_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		if (e.PropertyName != nameof(SpriteSource.Sprites)) return;
		var selected = GetSelectedSprite();
		Reload();
		SelectSpriteDefinition(selected);
	}

	private ISpriteDefinition? GetSelectedSprite() {
		return _selection.FirstOrDefault(s => s.IsSelected)?.SpriteDefinition;
	}

	private void SelectSpriteDefinition(ISpriteDefinition? spriteDefinition) {
		SelectSprite(spriteDefinition);
		SpriteSelected?.Invoke(SpriteDefinition);
	}

	public void Reload() {
		foreach (var node in GetChildren()) {
			node.QueueFree();
		}
		_selection.Clear();

		foreach (var sprite in SpriteSource.Sprites) {
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

	public void SelectSprite(ISpriteDefinition? sprite) {
		foreach (var selectableSprite in _selection) {
			selectableSprite.IsSelected = selectableSprite.SpriteDefinition == sprite;
		}
	}
}