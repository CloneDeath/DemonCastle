using System.ComponentModel;
using DemonCastle.Files.Elements;
using DemonCastle.Game.Animations.Generic;
using DemonCastle.ProjectFiles.Exceptions;
using DemonCastle.ProjectFiles.Projects.Data.Elements.Types;
using DemonCastle.ProjectFiles.State;
using Godot;
using Container = Godot.Container;

namespace DemonCastle.Game.Scenes.ElementTypes;

public partial class HealthBarElementView : Container {
	private readonly HealthBarElementInfo _element;
	private readonly IGameState _game;

	private int ElementWidth => _element.SpriteDefinition.Region.Size.X;
	private int NumberOfElements => _element.Region.Size.X / ElementWidth;

	public HealthBarElementView(HealthBarElementInfo element, IGameState game) {
		_element = element;
		_game = game;
		Name = nameof(HealthBarElementView);
		Refresh();
	}

	public override void _EnterTree() {
		base._EnterTree();
		_element.PropertyChanged += Element_OnPropertyChanged;
		_game.Player.PropertyChanged += GamePlayer_OnPropertyChanged;
	}

	private void GamePlayer_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		switch (_element.Source) {
			case HealthBarSource.PlayerHP:
				if (e.PropertyName is nameof(_game.Player.HP) or nameof(_game.Player.MaxHP)) {
					Refresh();
				}
				break;
			case HealthBarSource.PlayerMP:
				if (e.PropertyName is nameof(_game.Player.MP) or nameof(_game.Player.MaxMP)) {
					Refresh();
				}
				break;
			case HealthBarSource.PlayerLives:
				if (e.PropertyName is nameof(_game.Player.Lives) or nameof(_game.Player.MaxLives)) {
					Refresh();
				}
				break;
			default:
				throw new InvalidEnumValueException<HealthBarSource>(_element.Source);
		}
	}

	public override void _ExitTree() {
		base._ExitTree();
		_element.PropertyChanged -= Element_OnPropertyChanged;
		_game.Player.PropertyChanged -= GamePlayer_OnPropertyChanged;
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