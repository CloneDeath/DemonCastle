using DemonCastle.Editor.Windows.Properties;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using Godot;

namespace DemonCastle.Editor.Windows.SpriteGrid {
	public class SpriteGridDataPanel : PanelContainer {
		protected PropertyCollection Properties { get; }
		public SpriteGridDataPanel(SpriteGridDataInfo spriteData) {
			AddChild(Properties = new PropertyCollection());
			Properties.AddString("Name", spriteData, x => x.Name);
			Properties.AddInteger("X", spriteData, x => x.X);
			Properties.AddInteger("Y", spriteData, x => x.Y);
			Properties.AddBoolean("Flip Horizontal", spriteData, x => x.FlipHorizontal);
		}
	}
}