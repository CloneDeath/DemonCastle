using DemonCastle.Editor.Editors.Components;
using Godot;

namespace DemonCastle.Editor.Editors;

public partial class ImageEditor : BaseEditor {
	protected TextureView TextureView { get; }

	public ImageEditor(Texture2D texture) {
		Name = nameof(ImageEditor);

		AddChild(TextureView = new TextureView {
			Texture = texture
		});
		TextureView.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, LayoutPresetMode.Minsize, 5);
	}
}