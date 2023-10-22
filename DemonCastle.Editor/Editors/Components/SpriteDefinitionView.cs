using System.ComponentModel;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using Godot;

namespace DemonCastle.Editor.Editors.Components;

public partial class SpriteDefinitionView : CenterContainer {
	private readonly ISpriteDefinition _definition;
	private TextureRect Rect { get; set; }

	public SpriteDefinitionView(ISpriteDefinition definition) {
		_definition = definition;

		AddChild(Rect = new TextureRect {
			StretchMode = TextureRect.StretchModeEnum.KeepCentered
		});
		Reload();

		if (definition is INotifyPropertyChanged definitionNotify) {
			definitionNotify.PropertyChanged += DefinitionNotify_OnPropertyChanged;
		}
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
		Rect.Texture = new AtlasTexture {
			Atlas = _definition.Texture,
			Region = _definition.Region,
			FilterClip = true
		};
		Rect.FlipH = _definition.FlipHorizontal;
	}
}