using DemonCastle.Editor.Editors.SpriteAtlas.Details;
using DemonCastle.Editor.Editors.SpriteAtlas.View;
using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using Godot;
using DemonCastle.Editor.Editors.Components.Properties;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinitions;

namespace DemonCastle.Editor.Editors.SpriteAtlas;

public partial class SpriteAtlasEditor : BaseEditor {
    public override Texture2D TabIcon => IconTextures.File.SpriteAtlasIcon;

    protected HSplitContainer SplitContainer { get; }
    protected VBoxContainer LeftContainer { get; }
    protected PropertyCollection SpriteAtlasDetails { get; }
    protected SpriteAtlasDefinitionCollection SpriteCollection { get; }
    protected SpriteAtlasTextureView TextureView { get; }

    public SpriteAtlasEditor(SpriteAtlasInfo spriteAtlas) {
        Name = nameof(SpriteAtlasEditor);
        CustomMinimumSize = new Vector2I(600, 300);

        AddChild(SplitContainer = new HSplitContainer());
        SplitContainer.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, margin: 5);

        SplitContainer.AddChild(LeftContainer = new VBoxContainer());
        LeftContainer.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, margin: 5);
        {
            LeftContainer.AddChild(SpriteAtlasDetails = new SpriteAtlasDetails(spriteAtlas) {
                CustomMinimumSize = new Vector2(410, 0)
            });
            LeftContainer.AddChild(SpriteCollection = new SpriteAtlasDefinitionCollection(spriteAtlas));
            SpriteCollection.SpriteSelected += SpriteCollection_OnSpriteSelected;
        }

        SplitContainer.AddChild(TextureView = new SpriteAtlasTextureView(spriteAtlas));
        TextureView.SpriteSelected += TextureView_SpriteSelected;
    }

    private void SpriteCollection_OnSpriteSelected(SpriteAtlasDataInfo? sprite) {
        TextureView.SelectSprite(sprite);
    }

    private void TextureView_SpriteSelected(SpriteAtlasDataInfo sprite) {
        SpriteCollection.SelectSprite(sprite);
    }
}