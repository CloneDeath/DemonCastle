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
			WindowTitle = $"Sprite Atlas - {spriteAtlasInfo.FileName}";
			RectSize = new Vector2(600, 300);

			AddChild(SplitContainer = new HSplitContainer {
				AnchorRight = 1,
				AnchorBottom = 1,
				MarginTop = 5,
				MarginBottom = -5,
				MarginLeft = 5,
				MarginRight = -5
			});
			
			SplitContainer.AddChild(PropertyCollection = new PropertyCollection {
				MarginTop = 5,
				MarginRight = -5,
				MarginBottom = -5,
				MarginLeft = 5,
				AnchorBottom = 1,
				AnchorRight = 1,
				RectMinSize = new Vector2(200, 0)
			});
			PropertyCollection.AddString("File", spriteAtlasInfo, x => x.SpriteFile);
			PropertyCollection.AddChild(new ColorProperty {
				PropertyName = "Transparent Color",
				PropertyValue = spriteAtlasInfo.TransparentColor
			});
			PropertyCollection.AddChild(DataCollection = new SpriteAtlasDataCollection(spriteAtlasInfo.SpriteData) {
				AnchorRight = 1,
				AnchorBottom = 1,
				MarginBottom = 0,
				RectMinSize = new Vector2(100, 100)
			});

			SplitContainer.AddChild(ScrollContainer = new ScrollContainer());
			ScrollContainer.AddChild(TextureRect = new TextureRect {
				Texture = spriteAtlasInfo.Texture
			});
		}
	}
}