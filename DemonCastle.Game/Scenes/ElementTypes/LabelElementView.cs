using System.ComponentModel;
using DemonCastle.ProjectFiles.Exceptions;
using DemonCastle.ProjectFiles.Projects.Data.Elements.Types;
using DemonCastle.ProjectFiles.State;
using Godot;

namespace DemonCastle.Game.Scenes.ElementTypes;

public partial class LabelElementView : Label {
	private readonly LabelElementInfo _element;
	private readonly IGameState _game;

	public LabelElementView(LabelElementInfo element, IGameState game) {
		_element = element;
		_game = game;
		Name = nameof(LabelElementView);
		LabelSettings = new LabelSettings();
		Refresh();
	}

	public override void _EnterTree() {
		base._EnterTree();
		_element.PropertyChanged += Element_OnPropertyChanged;
		_game.Player.PropertyChanged += Player_OnPropertyChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		_element.PropertyChanged -= Element_OnPropertyChanged;
		_game.Player.PropertyChanged -= Player_OnPropertyChanged;
	}

	private void Player_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		Refresh();
	}

	private void Element_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		Refresh();
	}

	private void Refresh() {
		Position = _element.Region.Position;
		Size = _element.Region.Size;
		Text = new TextFinalizer(_game, _element.TextTransform).Finalize(_element.Text);

		HorizontalAlignment = _element.HorizontalAlignment switch {
			Files.Elements.HorizontalAlignment.Left => HorizontalAlignment.Left,
			Files.Elements.HorizontalAlignment.Center => HorizontalAlignment.Center,
			Files.Elements.HorizontalAlignment.Right => HorizontalAlignment.Right,
			_ => throw new InvalidEnumValueException<Files.Elements.HorizontalAlignment>(_element.HorizontalAlignment)
		};
		VerticalAlignment = _element.VerticalAlignment switch {
			Files.Elements.VerticalAlignment.Top => VerticalAlignment.Top,
			Files.Elements.VerticalAlignment.Center => VerticalAlignment.Center,
			Files.Elements.VerticalAlignment.Bottom => VerticalAlignment.Bottom,
			_ => throw new InvalidEnumValueException<Files.Elements.HorizontalAlignment>(_element.HorizontalAlignment)
		};

		LabelSettings.Font = _element.Font;
		LabelSettings.FontSize = _element.FontSize;
		LabelSettings.FontColor = _element.Color;
	}
}