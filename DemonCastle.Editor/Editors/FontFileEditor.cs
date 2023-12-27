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

		Lines.AddChild(new Label {
			Text = "The Quick Brown Fox Jumped Over the Lazy Dog",
			LabelSettings = new LabelSettings {
				Font = font.ToFont(),
				FontSize = 72
			}
		});
	}
}