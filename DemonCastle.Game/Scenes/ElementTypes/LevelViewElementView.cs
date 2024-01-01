using System.ComponentModel;
using DemonCastle.ProjectFiles.Projects.Data.Elements.Types;
using DemonCastle.ProjectFiles.State;
using Godot;

namespace DemonCastle.Game.Scenes.ElementTypes;

public partial class LevelViewElementView : TextureRect {
	private readonly LevelViewElementInfo _element;

	public LevelViewElementView(LevelViewElementInfo element, IGameState gameState) {
		_element = element;
		Name = nameof(LevelViewElementView);
		Texture = gameState.LevelView;

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