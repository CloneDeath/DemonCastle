using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.Windows {
	public partial class ImageWindow {
		protected ScrollContainer ScrollContainer { get; }
		protected TextureRect TextureRect { get; }
		
		public ImageWindow(FileNavigator texture) {
			WindowTitle = texture.FileName;
			RectSize = new Vector2(300, 300);
			AddChild(ScrollContainer = new ScrollContainer {
				AnchorRight = 1,
				AnchorBottom = 1,
				MarginLeft = 5,
				MarginTop = 5,
				MarginBottom = -5,
				MarginRight = -5,
			});
			ScrollContainer.AddChild(TextureRect = new TextureRect {
				Texture = texture.ToTexture()
			});
		}
	}
}