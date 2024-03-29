using System;
using System.Linq;
using DemonCastle.Game;
using Godot;

namespace DemonCastle.Editor.Editors.Components;

public partial class SelectableControl : Control {
	public CursorShape SelectedCursorShape { get; set; } = CursorShape.PointingHand;
	public CursorShape DefaultCursorShape { get; set; } = CursorShape.Arrow;

	private bool _isSelected;

	public bool IsSelected {
		get => _isSelected;
		set {
			if (_isSelected == value) return;
			_isSelected = value;
			TriggerSelectionChange();
		}
	}

	public event Action<SelectableControl>? Selected;

	public override void _Process(double delta) {
		base._Process(delta);

		MouseDefaultCursorShape = IsSelected
		  ? SelectedCursorShape
		  : DefaultCursorShape;
	}

	public override void _GuiInput(InputEvent @event) {
		base._GuiInput(@event);

		if (!@event.IsActionPressed(InputActions.EditorClick)) return;
		if (IsSelected) return;

		IsSelected = true;
	}

	private void TriggerSelectionChange() {
		if (!IsSelected) return;

		OnSelected();
		Selected?.Invoke(this);
	}

	protected virtual void OnSelected() {}

	protected void DeselectSiblings() {
		var siblings = GetParent().GetChildren().Where(c => c is SelectableControl && c != this).Cast<SelectableControl>();
		foreach (var sibling in siblings) {
			sibling.IsSelected = false;
		}
	}
}