using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.Windows {
	public partial class ImageWindow {
		protected ScrollContainer ScrollContainer { get; }
		protected TextureRect TextureRect { get; }
		
		public ImageWindow(FileNavigator texture) {
			WindowTitle = $"Image - {texture.FileName}";
			Size = new Vector2(300, 300);
			CustomMinimumSize = Size;
			
			AddChild(ScrollContainer = new ScrollContainer {
				AnchorRight = 1,
				AnchorBottom = 1,
				OffsetLeft = 5,
				OffsetTop = 5,
				OffsetBottom = -5,
				OffsetRight = -5,
			});
			ScrollContainer.AddChild(TextureRect = new TextureRect {
				Texture2D = texture.ToTexture()
			});
		}
	}
}