using DemonCastle.Editor.Windows.Properties;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using Godot;

namespace DemonCastle.Editor.Windows.SpriteGrid {
	public class SpriteGridDataPanel : PanelContainer {
		protected SpriteGridDataInfo SpriteData { get; }
		protected PropertyCollection Properties { get; }
		protected TextureRect Preview { get; }
		protected AtlasTexture PreviewTexture { get; }
		public SpriteGridDataPanel(SpriteGridDataInfo spriteData) {
			SpriteData = spriteData;
			
			AddChild(Properties = new PropertyCollection());
			Properties.AddString("Name", spriteData, x => x.Name);
			Properties.AddInteger("X", spriteData, x => x.X);
			Properties.AddInteger("Y", spriteData, x => x.Y);
			Properties.AddBoolean("Flip Horizontal", spriteData, x => x.FlipHorizontal);
			Properties.AddChild(Preview = new TextureRect {
				Texture = PreviewTexture = new AtlasTexture {
					Atlas = spriteData.Texture,
					Region = spriteData.Region
				},
				FlipH = spriteData.FlipHorizontal
			});
		}

		public override void _Process(float delta) {
			base._Process(delta);

			Preview.FlipH = SpriteData.FlipHorizontal;
			PreviewTexture.Atlas = SpriteData.Texture;
			PreviewTexture.Region = SpriteData.Region;
		}
	}
}