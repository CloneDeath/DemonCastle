using DemonCastle.Editor.Editors.Components;
using DemonCastle.Editor.Icons;
using DemonCastle.Navigation;
using Godot;

namespace DemonCastle.Editor.Editors;

public partial class ImageEditor : BaseEditor {
	public override Texture2D TabIcon => IconTextures.File.TextureIcon;
	public override string TabText { get; }

	protected TextureView TextureView { get; }

	public ImageEditor(FileNavigator file, Texture2D texture) {
		Name = nameof(ImageEditor);
		TabText = file.FileName;

		AddChild(TextureView = new TextureView {
			Texture = texture
		});
		TextureView.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, LayoutPresetMode.Minsize, 5);
	}
}