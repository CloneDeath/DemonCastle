using DemonCastle.Editor.Editors.Properties;
using DemonCastle.Editor.Editors.SpriteGrid.Details;
using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using Godot;

namespace DemonCastle.Editor.Editors.SpriteGrid;

public partial class SpriteGridEditor : BaseEditor {
	public override Texture2D TabIcon => IconTextures.SpriteGridIcon;
	public override string TabText { get; }

	protected HSplitContainer SplitContainer { get; }
	protected VBoxContainer LeftContainer { get; }
	protected PropertyCollection SpriteGridDetails { get; }
	protected SpriteGridDefinitionCollection SpriteCollection { get; }
	protected View.SpriteGridTextureView TextureView { get; }

	public SpriteGridEditor(SpriteGridInfo spriteGrid) {
		Name = nameof(SpriteGridEditor);
		TabText = spriteGrid.FileName;
		CustomMinimumSize = new Vector2I(500, 350);

		AddChild(SplitContainer = new HSplitContainer());
		SplitContainer.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, margin: 5);

		SplitContainer.AddChild(LeftContainer = new VBoxContainer());
		LeftContainer.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, margin: 5);
		{
			LeftContainer.AddChild(SpriteGridDetails = new SpriteGridDetails(spriteGrid) {
				CustomMinimumSize = new Vector2(410, 0)
			});
			LeftContainer.AddChild(SpriteCollection = new SpriteGridDefinitionCollection(spriteGrid));
		}

		SplitContainer.AddChild(TextureView = new View.SpriteGridTextureView(spriteGrid));
	}
}