using DemonCastle.Editor.Editors.Components;
using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.Editors;

public partial class ImageEditor : BaseEditor {
	public override Texture2D TabIcon => IconTextures.TextureIcon;
	public override string TabText { get; }

	protected TextureView TextureView { get; }

	public ImageEditor(FileNavigator texture) {
		Name = nameof(ImageEditor);
		TabText = texture.FileName;

		AddChild(TextureView = new TextureView());
		TextureView.SetAnchorsPreset(LayoutPreset.FullRect);
		TextureView.Texture = texture.ToTexture();
	}
}