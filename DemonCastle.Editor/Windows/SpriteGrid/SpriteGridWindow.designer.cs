using DemonCastle.Editor.Windows.Properties;
using DemonCastle.Editor.Windows.Textures;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using Godot;

namespace DemonCastle.Editor.Windows.SpriteGrid {
	public partial class SpriteGridWindow {
		protected PropertyCollection PropertyCollection { get; }
		protected SpriteGridTextureView TextureView { get; }
		protected SpriteGridDataCollection DataCollection { get; }

		public SpriteGridWindow(SpriteGridInfo spriteGridInfo) {
			Name = nameof(SpriteGridWindow);

			WindowTitle = $"Sprite Grid - {spriteGridInfo.FileName}";
			RectSize = new Vector2(600, 350);
			
			AddChild(PropertyCollection = new PropertyCollection {
				MarginLeft = 5,
				MarginTop = 5,
				MarginBottom = -5,
				MarginRight = 205,
				AnchorBottom = 1
			});
			PropertyCollection.AddFile("File", spriteGridInfo, spriteGridInfo.Directory, x => x.SpriteFile);
			PropertyCollection.AddInteger("Width", spriteGridInfo, x => x.Width);
			PropertyCollection.AddInteger("Height", spriteGridInfo, x => x.Height);
			PropertyCollection.AddInteger("X Offset", spriteGridInfo, x => x.XOffset);
			PropertyCollection.AddInteger("Y Offset", spriteGridInfo, x => x.YOffset);
			PropertyCollection.AddInteger("X Separation", spriteGridInfo, x => x.XSeparation);
			PropertyCollection.AddInteger("Y Separation", spriteGridInfo, x => x.YSeparation);
			PropertyCollection.AddChild(DataCollection = new SpriteGridDataCollection(spriteGridInfo) {
				AnchorRight = 1,
				AnchorBottom = 1,
				MarginBottom = 0,
				RectMinSize = new Vector2(100, 100)
			});
			
			AddChild(new VSeparator {
				Name = nameof(VSeparator),
				RectPosition = new Vector2(210, 5),
				MarginBottom = -5,
				AnchorBottom = 1
			});
			AddChild(TextureView = new SpriteGridTextureView(spriteGridInfo) {
				RectPosition = new Vector2(215, 5),
				MarginRight = -5,
				MarginBottom = -5,
				AnchorRight = 1,
				AnchorBottom = 1
			});
		}
	}
}