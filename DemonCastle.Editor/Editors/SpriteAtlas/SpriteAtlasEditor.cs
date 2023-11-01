using DemonCastle.Editor.Editors.Properties;
using DemonCastle.Editor.Editors.SpriteAtlas.Details;
using DemonCastle.Editor.Editors.SpriteAtlas.View;
using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using Godot;

namespace DemonCastle.Editor.Editors.SpriteAtlas;

public partial class SpriteAtlasEditor : BaseEditor {
    private readonly SpriteAtlasInfo _spriteAtlas;

    public override Texture2D TabIcon => IconTextures.SpriteAtlasIcon;
    public override string TabText { get; }

    protected HSplitContainer SplitContainer { get; }
    protected VBoxContainer LeftContainer { get; }
    protected PropertyCollection SpriteAtlasDetails { get; }
    protected SpriteAtlasTextureView TextureView { get; }

    public SpriteAtlasEditor(SpriteAtlasInfo spriteAtlas) {
        Name = nameof(SpriteAtlasEditor);
        TabText = spriteAtlas.FileName;
        CustomMinimumSize = new Vector2I(600, 300);

        _spriteAtlas = spriteAtlas;

        AddChild(SplitContainer = new HSplitContainer());
        SplitContainer.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, margin: 5);

        SplitContainer.AddChild(LeftContainer = new VBoxContainer());
        LeftContainer.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, margin: 5);
        {
            LeftContainer.AddChild(SpriteAtlasDetails = new SpriteAtlasDetails(spriteAtlas) {
                CustomMinimumSize = new Vector2(410, 0)
            });
            LeftContainer.AddChild(new SpriteAtlasDefinitionCollection(spriteAtlas));
        }

        SplitContainer.AddChild(TextureView = new View.SpriteAtlasTextureView(spriteAtlas));
    }
}