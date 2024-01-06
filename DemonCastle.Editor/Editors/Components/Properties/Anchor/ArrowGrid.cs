using System;
using Godot;

namespace DemonCastle.Editor.Editors.Components.Properties.Anchor;

public partial class ArrowGrid : GridContainer {
	private Vector2I _value = Vector2I.Zero;
	private readonly Button[,] _buttons = new Button[3,3];
	public event Action<Vector2I>? ValueChanged;

	public ArrowGrid() {
		Name = nameof(ArrowGrid);
		Columns = 3;

		var emoji = new[,] {
			{ "\u2196", "\u2191", "\u2197" },
			{ "\u2190", "\u2022", "\u2192" },
			{ "\u2199", "\u2193", "\u2198" }
		};
		for (var y = 0; y < 3; y++) {
			for (var x = 0; x < 3; x++) {
				AddChild(_buttons[y, x] = new Button {
					Text = emoji[y, x],
					ToggleMode = true,
					CustomMinimumSize = new Vector2(32, 32)
				});
				var coordinate = new Vector2I(x - 1, y - 1);
				_buttons[y, x].Toggled += pressed => Button_OnToggled(coordinate, pressed);
			}
		}

		Refresh();
	}

	private void Button_OnToggled(Vector2I coordinate, bool buttonPressed) {
		if (buttonPressed) {
			Value = coordinate;
		} else if (Value == coordinate) {
			Value = Vector2I.Zero;
		}
		Refresh();
	}

	public Vector2I Value {
		get => _value;
		set {
			if (_value == value) return;
			_value = value;
			Refresh();
			ValueChanged?.Invoke(_value);
		}
	}

	public void Enable() {
		for (var x = 0; x < 3; x++) {
			for (var y = 0; y < 3; y++) {
				_buttons[y, x].Disabled = false;
			}
		}
	}

	public void Disable() {
		for (var x = 0; x < 3; x++) {
			for (var y = 0; y < 3; y++) {
				_buttons[y, x].Disabled = true;
			}
		}
	}

	private void Refresh() {
		for (var x = 0; x < 3; x++) {
			for (var y = 0; y < 3; y++) {
				var coordinate = new Vector2I(x - 1, y - 1);
				_buttons[y, x].ButtonPressed = coordinate == _value;
			}
		}
	}
}