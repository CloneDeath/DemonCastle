using System.Net.Http.Headers;
using DemonCastle.Editor.Editors.Properties;
using DemonCastle.Editor.Editors.Textures;
using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using Godot;

namespace DemonCastle.Editor.Editors.SpriteGrid; 

public partial class SpriteGridEditor : BaseEditor {
	public override Texture2D TabIcon => IconTextures.SpriteGridIcon;	
	public override string TabText { get; }	
	
	protected PropertyCollection PropertyCollection { get; }
	protected SpriteGridTextureView TextureView { get; }
	protected SpriteGridDataCollection DataCollection { get; }

	public SpriteGridEditor(SpriteGridInfo spriteGridInfo) {
		Name = nameof(SpriteGridEditor);
		TabText = $"Sprite Grid - {spriteGridInfo.FileName}";
		CustomMinimumSize = new Vector2I(500, 350);
			
		AddChild(PropertyCollection = new PropertyCollection {
			OffsetLeft = 5,
			OffsetTop = 5,
			OffsetBottom = -5,
			OffsetRight = 205,
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
			OffsetBottom = 0,
			CustomMinimumSize = new Vector2(100, 100)
		});
			
		AddChild(new VSeparator {
			Name = nameof(VSeparator),
			Position = new Vector2(210, 5),
			OffsetBottom = -5,
			AnchorBottom = 1
		});
		AddChild(TextureView = new SpriteGridTextureView(spriteGridInfo) {
			Position = new Vector2(215, 5),
			OffsetRight = -5,
			OffsetBottom = -5,
			AnchorRight = 1,
			AnchorBottom = 1
		});
	}
}