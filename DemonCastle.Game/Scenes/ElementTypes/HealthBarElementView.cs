using System.ComponentModel;
using DemonCastle.Game.Animations.Generic;
using DemonCastle.ProjectFiles.Projects.Data.Elements.Types;
using Godot;

namespace DemonCastle.Game.Scenes.ElementTypes;

public partial class HealthBarElementView : SpriteDefinitionNode {
	private readonly HealthBarElementInfo _element;

	public HealthBarElementView(HealthBarElementInfo element) : base(element.SpriteDefinition, Vector2I.Zero){
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
		Load(_element.SpriteDefinition);
	}
}