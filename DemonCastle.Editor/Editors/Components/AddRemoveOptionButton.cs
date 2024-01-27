using System;
using DemonCastle.Editor.Icons;
using Godot;

namespace DemonCastle.Editor.Editors.Components;

public partial class AddRemoveOptionButton : HBoxContainer {
	private bool _removeDisabled;

	private readonly Label _label;
	private readonly OptionButton _options;
	private readonly Button _addButton;
	private readonly Button _removeButton;

	public event Action? AddPressed;
	public event Action? RemovePressed;
	public event OptionButton.ItemSelectedEventHandler? ItemSelected;

	public AddRemoveOptionButton() {
		AddChild(_label = new Label { Text = "Options" });
		AddChild(_options = new OptionButton {
			Text = "<None>",
			SizeFlagsHorizontal = SizeFlags.ExpandFill
		});
		_options.ItemSelected += Options_OnItemSelected;
		AddChild(_addButton = new Button { Icon = IconTextures.AddIcon });
		_addButton.Pressed += () => AddPressed?.Invoke();
		AddChild(_removeButton = new Button { Icon = IconTextures.DeleteIcon });
		_removeButton.Pressed += () => RemovePressed?.Invoke();
	}

	private void Options_OnItemSelected(long index) {
		_removeButton.Disabled = index < 0 || _removeDisabled;
		ItemSelected?.Invoke(index);
	}

	public string Label {
		get => _label.Text;
		set => _label.Text = value;
	}

	public bool Disabled {
		get => _options.Disabled;
		set {
			_options.Disabled = value;
			_addButton.Disabled = value;
			_removeButton.Disabled = value || _options.Selected < 0 || _removeDisabled;
		}
	}

	public bool RemoveDisabled {
		get => _removeButton.Disabled;
		set {
			_removeButton.Disabled = value || _options.Selected < 0;
			_removeDisabled = value;
		}
	}

	public int Selected {
		get => _options.Selected;
		set {
			_options.Selected = value;
			_removeButton.Disabled = _options.Disabled || value < 0 || _removeDisabled;
		}
	}

	public string Text {
		get => _options.Text;
		set => _options.Text = value;
	}

	public void Clear() => _options.Clear();
	public int GetSelectedId() => _options.GetSelectedId();
	public void AddItem(string label, int id = -1) => _options.AddItem(label, id);
	public void SetItemMetadata(int idx, Variant metadata) => _options.SetItemMetadata(idx, metadata);
	public Variant GetItemMetadata(int idx) => _options.GetItemMetadata(idx);
	public int ItemCount => _options.ItemCount;
}