using DemonCastle.Editor.Windows.Properties;
using DemonCastle.ProjectFiles;
using Godot;

namespace DemonCastle.Editor.Windows.SpriteAtlas {
	public class SpriteAtlasDataPanel : PanelContainer {
		protected PropertyCollection Properties { get; }
		public SpriteAtlasDataPanel(SpriteAtlasData spriteData) {
			AddChild(Properties = new PropertyCollection());
			Properties.AddChild(new StringProperty {
				PropertyName = "Name",
				PropertyValue = spriteData.Name
			});
			Properties.AddChild(new IntegerProperty {
				PropertyName = "X",
				PropertyValue = spriteData.X
			});
			Properties.AddChild(new IntegerProperty {
				PropertyName = "Y",
				PropertyValue = spriteData.Y
			});
			Properties.AddChild(new IntegerProperty {
				PropertyName = "Width",
				PropertyValue = spriteData.Width
			});
			Properties.AddChild(new IntegerProperty {
				PropertyName = "Height",
				PropertyValue = spriteData.Height
			});
		}
	}
}