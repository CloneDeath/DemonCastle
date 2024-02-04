using DemonCastle.Editor.Editors.Components;
using DemonCastle.Editor.Icons;
using DemonCastle.Navigation;
using Godot;

namespace DemonCastle.Editor.Editors;

public partial class ImageEditor : BaseEditor {
	public override Texture2D TabIcon => IconTextures.File.TextureIcon;
	public override string TabText { get; }

	protected TextureView TextureView { get; }

	public ImageEditor(FileNavigator texture) {
		Name = nameof(ImageEditor);
		TabText = texture.FileName;

		AddChild(TextureView = new TextureView {
			Texture = texture.ToTexture()
		});
		TextureView.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, LayoutPresetMode.Minsize, 5);
	}
}