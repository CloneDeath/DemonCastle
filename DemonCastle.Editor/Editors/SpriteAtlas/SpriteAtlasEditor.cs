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
    protected SpriteAtlasTextureView TextureView { get; }

    protected SpriteAtlasDataCollection DataCollection { get; }

    public SpriteAtlasEditor(SpriteAtlasInfo spriteAtlasInfo) {
        Name = nameof(SpriteAtlasEditor);
        TabText = spriteAtlasInfo.FileName;
        CustomMinimumSize = new Vector2I(600, 300);

        _spriteAtlasInfo = spriteAtlasInfo;

        AddChild(SplitContainer = new HSplitContainer {
            OffsetTop = 5,
            OffsetBottom = -5,
            OffsetLeft = 5,
            OffsetRight = -5
        });
        SplitContainer.SetAnchorsPreset(LayoutPreset.FullRect, true);

        SplitContainer.AddChild(PropertyCollection = new PropertyCollection {
            OffsetTop = 5,
            OffsetRight = -5,
            OffsetBottom = -5,
            OffsetLeft = 5,
            CustomMinimumSize = new Vector2(410, 0)
        });
        PropertyCollection.SetAnchorsPreset(LayoutPreset.FullRect, true);
        PropertyCollection.AddFile("File", spriteAtlasInfo, spriteAtlasInfo.Directory, x => x.SpriteFile, FileType.RawTextureFiles);
        PropertyCollection.AddColor("Transparent Color", spriteAtlasInfo, x => x.TransparentColor);

        PropertyCollection.AddChild(AddSpriteButton = new Button {
            Text = "Add Sprite"
        });
        AddSpriteButton.Pressed += AddSpriteButton_OnPressed;

        PropertyCollection.AddChild(DataCollection = new SpriteAtlasDataCollection(spriteAtlasInfo.SpriteData) {
            SizeFlagsVertical = SizeFlags.ExpandFill
        });
        DataCollection.SetAnchorsPreset(LayoutPreset.FullRect);

        SplitContainer.AddChild(TextureView = new SpriteAtlasTextureView(spriteAtlasInfo));
    }

    private void AddSpriteButton_OnPressed() {
        _spriteAtlasInfo.CreateSprite();
        DataCollection.Reload();
    }
}