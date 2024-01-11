using System.ComponentModel;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using Godot;

namespace DemonCastle.Editor.Editors.Components;

public partial class SpriteDefinitionView : CenterContainer {
	private ISpriteDefinition? _definition;
	private TextureRect Rect { get; set; }

	public SpriteDefinitionView() {
		AddChild(Rect = new TextureRect {
			StretchMode = TextureRect.StretchModeEnum.KeepCentered
		});
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

		Rect.Texture = _definition != null
						   ? new AtlasTexture {
							   Atlas = _definition.Texture,
							   Region = _definition.Region,
							   FilterClip = true
						   }
						   : null;
		Rect.TextureFilter = TextureFilterEnum.Nearest;
		Rect.FlipH = _definition?.FlipHorizontal ?? false;
		Rect.Material = new TransparentColorSpriteShader {
			TransparentColor = definition?.TransparentColor ?? Colors.Magenta,
			Threshold = definition?.TransparentColorThreshold ?? 0.01f
		};
	}
}