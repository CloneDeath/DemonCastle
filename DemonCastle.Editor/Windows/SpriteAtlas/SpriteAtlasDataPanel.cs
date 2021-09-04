using DemonCastle.Editor.Windows.Properties;
using DemonCastle.ProjectFiles;
using Godot;

namespace DemonCastle.Editor.Windows.SpriteAtlas {
	public class SpriteAtlasDataPanel : PanelContainer {
		protected PropertyCollection Properties { get; }
		public SpriteAtlasDataPanel(SpriteAtlasData spriteData) {
			AddChild(Properties = new PropertyCollection());
			Properties.AddString("Name", spriteData, x => x.Name);
			Properties.AddInteger("X", spriteData, x => x.X);
			Properties.AddInteger("Y", spriteData, x => x.Y);
			Properties.AddInteger("Width", spriteData, x => x.Width);
			Properties.AddInteger("Height", spriteData, x => x.Height);
		}
	}
}