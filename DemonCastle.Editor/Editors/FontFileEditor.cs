using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.Editors;

public partial class FontFileEditor : BaseEditor {
	public override Texture2D TabIcon => IconTextures.TextureIcon;
	public override string TabText { get; }

	protected VBoxContainer Lines { get; }

	public FontFileEditor(FileNavigator font) {
		Name = nameof(ImageEditor);
		TabText = font.FileName;

		AddChild(Lines = new VBoxContainer());
		Lines.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, margin: 5);

		const string previewText = "The Quick Brown Fox Jumped Over the Lazy Dog";

		Lines.AddChild(new Label {
			Text = previewText,
			LabelSettings = new LabelSettings {
				Font = font.ToFont(),
				FontSize = 72
			}
		});
		Lines.AddChild(new Label {
			Text = previewText.ToUpper(),
			LabelSettings = new LabelSettings {
				Font = font.ToFont(),
				FontSize = 72
			}
		});
		Lines.AddChild(new Label {
			Text = previewText.ToLower(),
			LabelSettings = new LabelSettings {
				Font = font.ToFont(),
				FontSize = 72
			}
		});
	}
}