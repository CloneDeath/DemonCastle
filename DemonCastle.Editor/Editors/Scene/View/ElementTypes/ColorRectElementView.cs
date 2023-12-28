using System.ComponentModel;
using DemonCastle.ProjectFiles.Projects.Data.Elements.Types;
using Godot;

namespace DemonCastle.Editor.Editors.Scene.View.ElementTypes;

public partial class ColorRectElementView : ColorRect {
	private readonly ColorRectElementInfo _element;

	public ColorRectElementView(ColorRectElementInfo element) {
		_element = element;
		Name = nameof(ColorRectElementView);

		Refresh();
	}

	public override void _EnterTree() {
		base._EnterTree();
		_element.PropertyChanged += Element_OnPropertyChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		_element.PropertyChanged -= Element_OnPropertyChanged;
	}

	private void Element_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		Refresh();
	}

	private void Refresh() {
		Position = _element.Region.Position;
		Size = _element.Region.Size;
		Color = _element.Color;
	}
}