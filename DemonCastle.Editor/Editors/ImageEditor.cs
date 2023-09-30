using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.Editors; 

public partial class ImageEditor : BaseEditor {
	public override Texture2D TabIcon => IconTextures.TextureIcon;
	public override string TabText { get; }
	
	protected ScrollContainer ScrollContainer { get; }
	protected TextureRect TextureRect { get; }
		
	public ImageEditor(FileNavigator texture) {
		Name = nameof(ImageEditor);
		TabText = texture.FileName;
		CustomMinimumSize = new Vector2I(300, 300);
			
		AddChild(ScrollContainer = new ScrollContainer {
			AnchorRight = 1,
			AnchorBottom = 1,
			OffsetLeft = 5,
			OffsetTop = 5,
			OffsetBottom = -5,
			OffsetRight = -5
		});
		ScrollContainer.AddChild(TextureRect = new TextureRect {
			Texture = texture.ToTexture()
		});
	}
}