using System.ComponentModel;
using DemonCastle.Game.Animations.Generic;
using DemonCastle.ProjectFiles.Projects.Data.Elements.Types;
using Godot;
using Container = Godot.Container;

namespace DemonCastle.Game.Scenes.ElementTypes;

public partial class HealthBarElementView : Container {
	private readonly HealthBarElementInfo _element;

	private int ElementWidth => _element.SpriteDefinition.Region.Size.X;
	private int NumberOfElements => _element.Region.Size.X / ElementWidth;

	public HealthBarElementView(HealthBarElementInfo element) {
		_element = element;
		Name = nameof(HealthBarElementView);
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

		foreach (var child in GetChildren()) {
			child.QueueFree();
		}

		for (var x = 0; x < NumberOfElements; x++) {
			AddChild(new SpriteDefinitionNode(_element.SpriteDefinition, Vector2I.Zero) {
				Position = new Vector2(x * ElementWidth, 0)
			});
		}
	}
}