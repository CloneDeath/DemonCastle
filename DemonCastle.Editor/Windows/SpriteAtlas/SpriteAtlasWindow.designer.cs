using DemonCastle.Editor.Windows.Properties;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using Godot;

namespace DemonCastle.Editor.Windows.SpriteAtlas {
	public partial class SpriteAtlasWindow {
		protected HSplitContainer SplitContainer { get; }
		protected PropertyCollection PropertyCollection { get; }
		protected ScrollContainer ScrollContainer { get; }
		protected TextureRect TextureRect { get; }
		
		protected SpriteAtlasDataCollection DataCollection { get; }

		public SpriteAtlasWindow(SpriteAtlasInfo spriteAtlasInfo) {
			WindowTitle = $"Sprite2D Atlas - {spriteAtlasInfo.FileName}";
			Size = new Vector2(600, 300);
			CustomMinimumSize = Size;

			AddChild(SplitContainer = new HSplitContainer {
				AnchorRight = 1,
				AnchorBottom = 1,
				OffsetTop = 5,
				OffsetBottom = -5,
				OffsetLeft = 5,
				OffsetRight = -5
			});
			
			SplitContainer.AddChild(PropertyCollection = new PropertyCollection {
				OffsetTop = 5,
				OffsetRight = -5,
				OffsetBottom = -5,
				OffsetLeft = 5,
				AnchorBottom = 1,
				AnchorRight = 1,
				CustomMinimumSize = new Vector2(200, 0)
			});
			PropertyCollection.AddString("File", spriteAtlasInfo, x => x.SpriteFile);
			PropertyCollection.AddChild(new ColorProperty {
				PropertyName = "Transparent Color",
				PropertyValue = spriteAtlasInfo.TransparentColor
			});
			PropertyCollection.AddChild(DataCollection = new SpriteAtlasDataCollection(spriteAtlasInfo.SpriteData) {
				AnchorRight = 1,
				AnchorBottom = 1,
				OffsetBottom = 0,
				CustomMinimumSize = new Vector2(100, 100)
			});

			SplitContainer.AddChild(ScrollContainer = new ScrollContainer());
			ScrollContainer.AddChild(TextureRect = new TextureRect {
				Texture2D = spriteAtlasInfo.Texture2D
			});
		}
	}
}