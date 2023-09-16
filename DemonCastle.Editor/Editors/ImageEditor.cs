using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.Editors; 

public partial class ImageEditor : Control {
	protected ScrollContainer ScrollContainer { get; }
	protected TextureRect TextureRect { get; }
		
	public ImageEditor(FileNavigator texture) {
		Name = $"Image - {texture.FileName}";
		CustomMinimumSize = new Vector2I(300, 300);
			
		AddChild(ScrollContainer = new ScrollContainer {
			AnchorRight = 1,
			AnchorBottom = 1,
			OffsetLeft = 5,
			OffsetTop = 5,
			OffsetBottom = -5,
			OffsetRight = -5,
		});
		ScrollContainer.AddChild(TextureRect = new TextureRect {
			Texture = texture.ToTexture()
		});
	}
}