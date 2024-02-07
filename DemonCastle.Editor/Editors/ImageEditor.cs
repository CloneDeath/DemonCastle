using DemonCastle.Editor.Editors.Components;
using DemonCastle.Editor.Icons;
using Godot;

namespace DemonCastle.Editor.Editors;

public partial class ImageEditor : BaseEditor {
	public override Texture2D TabIcon => IconTextures.File.TextureIcon;

	protected TextureView TextureView { get; }

	public ImageEditor(Texture2D texture) {
		Name = nameof(ImageEditor);

		AddChild(TextureView = new TextureView {
			Texture = texture
		});
		TextureView.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, LayoutPresetMode.Minsize, 5);
	}
}