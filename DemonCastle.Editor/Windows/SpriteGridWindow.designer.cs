using DemonCastle.Editor.Windows.Properties;
using DemonCastle.Editor.Windows.Textures;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using Godot;

namespace DemonCastle.Editor.Windows {
	public partial class SpriteGridWindow {
		protected PropertyCollection PropertyCollection { get; }
		protected SpriteGridTextureView TextureView { get; }

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
			PropertyCollection.AddInteger("Width", spriteGridInfo, x => x.Width);
			PropertyCollection.AddInteger("Height", spriteGridInfo, x => x.Height);
			PropertyCollection.AddInteger("X Offset", spriteGridInfo, x => x.XOffset);
			PropertyCollection.AddInteger("Y Offset", spriteGridInfo, x => x.YOffset);
			PropertyCollection.AddInteger("X Separation", spriteGridInfo, x => x.XSeparation);
			PropertyCollection.AddInteger("Y Separation", spriteGridInfo, x => x.YSeparation);

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