using System.ComponentModel;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using Godot;

namespace DemonCastle.Editor.Editors.Components;

public partial class SpriteDefinitionView : CenterContainer {
	private ISpriteDefinition? _definition;
	private TextureRect Rect { get; set; }

	public SpriteDefinitionView(ISpriteDefinition definition) {
		AddChild(Rect = new TextureRect {
			StretchMode = TextureRect.StretchModeEnum.KeepCentered
		});
		Load(definition);
	}

	public override void _ExitTree() {
		base._ExitTree();
		if (_definition is INotifyPropertyChanged definitionNotify) {
			definitionNotify.PropertyChanged -= DefinitionNotify_OnPropertyChanged;
		}
	}

	private void DefinitionNotify_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		Reload();
	}

	public void Reload() {
		if (_definition != null) Load(_definition);
	}

	public void Load(ISpriteDefinition definition) {
		if (_definition is INotifyPropertyChanged oldDefinition) {
			oldDefinition.PropertyChanged -= DefinitionNotify_OnPropertyChanged;
		}
		_definition = definition;
		if (_definition is INotifyPropertyChanged newDefinition) {
			newDefinition.PropertyChanged += DefinitionNotify_OnPropertyChanged;
		}

		Rect.Texture = new AtlasTexture {
			Atlas = _definition.Texture,
			Region = _definition.Region,
			FilterClip = true
		};
		Rect.TextureFilter = TextureFilterEnum.Nearest;
		Rect.FlipH = _definition.FlipHorizontal;
	}
}