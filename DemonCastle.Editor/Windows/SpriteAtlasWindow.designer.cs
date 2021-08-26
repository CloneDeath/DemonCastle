using DemonCastle.Editor.Windows.Properties;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using Godot;

namespace DemonCastle.Editor.Windows {
	public partial class SpriteAtlasWindow {
		protected PropertyCollection PropertyCollection { get; }
		public SpriteAtlasWindow(SpriteAtlasInfo spriteAtlasInfo) {
			WindowTitle = spriteAtlasInfo.FileName;
			RectSize = new Vector2(600, 300);

			AddChild(PropertyCollection = new PropertyCollection {
				MarginTop = 5,
				MarginRight = -5,
				MarginBottom = -5,
				MarginLeft = 5,
				AnchorBottom = 1,
				AnchorRight = 1
			});
			PropertyCollection.AddChild(new StringProperty {
				PropertyName = "File",
				PropertyValue = spriteAtlasInfo.SpriteFile
			});
			PropertyCollection.AddChild(new ColorProperty {
				PropertyName = "Transparent Color",
				PropertyValue = spriteAtlasInfo.TransparentColor
			});
		}
	}
}