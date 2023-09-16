using DemonCastle.Editor.Editors.Properties;
using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using Godot;

namespace DemonCastle.Editor.Editors.SpriteAtlas; 

public partial class SpriteAtlasEditor : BaseEditor {
    private readonly SpriteAtlasInfo _spriteAtlasInfo;
    
    public override Texture2D TabIcon => IconTextures.SpriteAtlasIcon;
    public override string TabText { get; }
    
    protected HSplitContainer SplitContainer { get; }
    protected PropertyCollection PropertyCollection { get; }
    protected Button AddSpriteButton { get; }
    protected SpriteAtlasTextureEditor TextureEditor { get; }

    protected SpriteAtlasDataCollection DataCollection { get; }

    public SpriteAtlasEditor(SpriteAtlasInfo spriteAtlasInfo) {
        Name = nameof(SpriteAtlasEditor);
        TabText = spriteAtlasInfo.FileName;
        CustomMinimumSize = new Vector2I(600, 300);
        
        _spriteAtlasInfo = spriteAtlasInfo;

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
            CustomMinimumSize = new Vector2(410, 0)
        });
        PropertyCollection.AddFile("File", spriteAtlasInfo, spriteAtlasInfo.Directory, x => x.SpriteFile);
        PropertyCollection.AddColor("Transparent Color", spriteAtlasInfo, x => x.TransparentColor);
        
        PropertyCollection.AddChild(AddSpriteButton = new Button {
            Text = "Add Sprite"
        });
        AddSpriteButton.Pressed += AddSpriteButton_OnPressed;
        
        PropertyCollection.AddChild(DataCollection = new SpriteAtlasDataCollection(spriteAtlasInfo.SpriteData) {
            AnchorRight = 1,
            AnchorBottom = 1,
            OffsetBottom = 0,
            SizeFlagsVertical = SizeFlags.ExpandFill,
            CustomMinimumSize = new Vector2(100, 100)
        });

        SplitContainer.AddChild(TextureEditor = new SpriteAtlasTextureEditor(spriteAtlasInfo));
    }

    private void AddSpriteButton_OnPressed() {
        _spriteAtlasInfo.CreateSprite();
        DataCollection.Reload();
    }
}