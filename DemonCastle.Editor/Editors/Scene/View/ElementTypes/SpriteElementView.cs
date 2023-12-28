using System.ComponentModel;
using DemonCastle.Editor.Editors.Components;
using DemonCastle.ProjectFiles.Projects.Data.Elements.Types;

namespace DemonCastle.Editor.Editors.Scene.View.ElementTypes;

public partial class SpriteElementView : SpriteDefinitionView {
	private readonly SpriteElementInfo _element;

	public SpriteElementView(SpriteElementInfo element) {
		_element = element;
		Name = nameof(SpriteElementView);
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
		Load(_element.SpriteDefinition);
	}
}