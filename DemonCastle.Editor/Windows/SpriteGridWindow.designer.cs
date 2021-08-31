using DemonCastle.Editor.Windows.Properties;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using Godot;

namespace DemonCastle.Editor.Windows {
	public partial class SpriteGridWindow {
		protected PropertyCollection PropertyCollection { get; }
		protected ScrollContainer ScrollContainer { get; }
		protected TextureRect TextureRect { get; }

		public SpriteGridWindow(SpriteGridInfo spriteGridInfo) {
			Name = nameof(SpriteGridWindow);
			
			WindowTitle = spriteGridInfo.FileName;
			RectSize = new Vector2(600, 300);
			
			AddChild(PropertyCollection = new PropertyCollection {
				MarginLeft = 5,
				MarginTop = 5,
				MarginBottom = 5,
				MarginRight = 205,
				AnchorBottom = 1
			});
			PropertyCollection.AddChild(new StringProperty {
				PropertyName = "File",
				PropertyValue = spriteGridInfo.SpriteFile
			});
			PropertyCollection.AddChild(new IntegerProperty {
				PropertyName = "Width",
				PropertyValue = spriteGridInfo.Width
			});
			PropertyCollection.AddChild(new IntegerProperty {
				PropertyName = "Height",
				PropertyValue = spriteGridInfo.Height
			});
			PropertyCollection.AddChild(new IntegerProperty {
				PropertyName = "X Offset",
				PropertyValue = spriteGridInfo.XOffset
			});
			PropertyCollection.AddChild(new IntegerProperty {
				PropertyName = "Y Offset",
				PropertyValue = spriteGridInfo.YOffset
			});
			PropertyCollection.AddChild(new IntegerProperty {
				PropertyName = "X Separation",
				PropertyValue = spriteGridInfo.XSeparation
			});
			PropertyCollection.AddChild(new IntegerProperty {
				PropertyName = "Y Separation",
				PropertyValue = spriteGridInfo.YSeparation
			});
			
			AddChild(new VSeparator {
				Name = nameof(VSeparator),
				RectPosition = new Vector2(210, 5),
				MarginBottom = -5,
				AnchorBottom = 1
			});
			AddChild(ScrollContainer = new ScrollContainer {
				Name = nameof(ScrollContainer),
				RectPosition = new Vector2(215, 5),
				MarginRight = -5,
				MarginBottom = -5,
				AnchorRight = 1,
				AnchorBottom = 1
			});
			ScrollContainer.AddChild(TextureRect = new TextureRect {
				Name = nameof(TextureRect),
				Texture = spriteGridInfo.Texture,
			});
		}
	}
}