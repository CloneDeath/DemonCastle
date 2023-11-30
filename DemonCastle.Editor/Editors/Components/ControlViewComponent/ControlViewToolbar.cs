using System;
using DemonCastle.Editor.Icons;
using Godot;

namespace DemonCastle.Editor.Editors.Components.ControlViewComponent;

public partial class ControlViewToolbar : HBoxContainer {
	public bool ShowGrid { get; set; }

	protected Button Controls_Grid { get; }
	protected Button Controls_MagPlus { get; }
	protected Button Controls_MagMinus { get; }
	protected Button Controls_OneToOne { get; }

	public float Zoom { get; set; } = 1;
	public event Action<float>? ZoomLevelChanged;

	public ControlViewToolbar() {
		AddChild(Controls_Grid = new Button {
			Icon = IconTextures.GridIcon,
			TooltipText = "Toggle Grid"
		});
		Controls_Grid.Pressed += Controls_Grid_OnPressed;

		AddChild(Controls_MagPlus = new Button {
			Icon = IconTextures.MagnifyPlusIcon,
			TooltipText = "Zoom In"
		});
		Controls_MagPlus.Pressed += Controls_MagPlus_OnPressed;

		AddChild(Controls_MagMinus = new Button {
			Icon = IconTextures.MagnifyMinusIcon,
			TooltipText = "Zoom Out"
		});
		Controls_MagMinus.Pressed += Controls_MagMinus_OnPressed;

		AddChild(Controls_OneToOne = new Button {
			Icon = IconTextures.OneToOneIcon,
			TooltipText = "Reset Zoom"
		});
		Controls_OneToOne.Pressed += Controls_OneToOne_OnPressed;
	}

	private void Controls_Grid_OnPressed() {
		ShowGrid = !ShowGrid;
	}

	private void Controls_MagPlus_OnPressed() {
		Zoom *= 2;
		ZoomLevelChanged?.Invoke(Zoom);
	}

	private void Controls_MagMinus_OnPressed() {
		Zoom /= 2;
		ZoomLevelChanged?.Invoke(Zoom);
	}

	private void Controls_OneToOne_OnPressed() {
		Zoom = 1;
		ZoomLevelChanged?.Invoke(Zoom);
	}
}