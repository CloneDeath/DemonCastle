using System.ComponentModel;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinitions;
using Godot;

namespace DemonCastle.Editor.Editors.Components;

public partial class SpriteDefinitionView : TextureRect {
	private ISpriteDefinition? _definition;

	public SpriteDefinitionView() {
		StretchMode = StretchModeEnum.KeepCentered;
	}

	public SpriteDefinitionView(ISpriteDefinition definition) : this() {
		Load(definition);
	}

	public override void _ExitTree() {
		base._ExitTree();
		if (_definition != null) {
			_definition.PropertyChanged -= DefinitionNotify_OnPropertyChanged;
		}
	}

	private void DefinitionNotify_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		Reload();
	}

	public void Reload() {
		if (_definition != null) Load(_definition);
	}

	public void Load(ISpriteDefinition? definition) {
		if (_definition != null) {
			_definition.PropertyChanged -= DefinitionNotify_OnPropertyChanged;
		}
		_definition = definition;
		if (_definition != null) {
			_definition.PropertyChanged += DefinitionNotify_OnPropertyChanged;
		}

		Texture = _definition?.ToTexture();
		TextureFilter = TextureFilterEnum.Nearest;
		FlipH = _definition?.FlipHorizontal ?? false;
		Material = new TransparentColorSpriteShader {
			TransparentColor = definition?.TransparentColor ?? Colors.Magenta,
			Threshold = definition?.TransparentColorThreshold ?? 0.01f
		};
	}
}