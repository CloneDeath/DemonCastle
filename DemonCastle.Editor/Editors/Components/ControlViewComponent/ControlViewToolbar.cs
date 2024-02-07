using System;
using DemonCastle.Editor.Icons;
using Godot;

namespace DemonCastle.Editor.Editors.Components.ControlViewComponent;

public partial class ControlViewToolbar : HBoxContainer {
	public bool ShowGrid { get; set; }

	protected Button ToggleGrid { get; }
	protected Button MagPlus { get; }
	protected Button MagMinus { get; }
	protected Button OneToOne { get; }

	public float Zoom { get; set; } = 1;
	public event Action<float>? ZoomLevelChanged;

	public ControlViewToolbar() {
		AddChild(ToggleGrid = new Button {
			Icon = IconTextures.GridIcon,
			TooltipText = "Toggle Grid"
		});
		ToggleGrid.Pressed += ToggleGrid_OnPressed;

		AddChild(MagPlus = new Button {
			Icon = IconTextures.MagnifyPlusIcon,
			TooltipText = "Zoom In"
		});
		MagPlus.Pressed += MagPlus_OnPressed;

		AddChild(MagMinus = new Button {
			Icon = IconTextures.MagnifyMinusIcon,
			TooltipText = "Zoom Out"
		});
		MagMinus.Pressed += MagMinus_OnPressed;

		AddChild(OneToOne = new Button {
			Icon = IconTextures.OneToOneIcon,
			TooltipText = "Reset Zoom"
		});
		OneToOne.Pressed += OneToOne_OnPressed;
	}

	private void ToggleGrid_OnPressed() {
		ShowGrid = !ShowGrid;
	}

	private void MagPlus_OnPressed() {
		Zoom *= 2;
		ZoomLevelChanged?.Invoke(Zoom);
	}

	private void MagMinus_OnPressed() {
		Zoom /= 2;
		ZoomLevelChanged?.Invoke(Zoom);
	}

	private void OneToOne_OnPressed() {
		Zoom = 1;
		ZoomLevelChanged?.Invoke(Zoom);
	}
}