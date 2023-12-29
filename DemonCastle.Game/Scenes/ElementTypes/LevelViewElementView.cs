using System.ComponentModel;
using DemonCastle.ProjectFiles.Projects.Data.Elements.Types;
using Godot;

namespace DemonCastle.Game.Scenes.ElementTypes;

public partial class LevelViewElementView : ColorRect {
	private readonly LevelViewElementInfo _element;

	public LevelViewElementView(LevelViewElementInfo element) {
		_element = element;
		Name = nameof(LevelViewElementView);
		Color = Colors.Silver;

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
	}
}