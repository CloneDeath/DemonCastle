using DemonCastle.Editor.Editors.SpriteGrid.Details;
using DemonCastle.Editor.Editors.SpriteGrid.View;
using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using Godot;
using DemonCastle.Editor.Editors.Components.Properties;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;

namespace DemonCastle.Editor.Editors.SpriteGrid;

public partial class SpriteGridEditor : BaseEditor {
	public override Texture2D TabIcon => IconTextures.SpriteGridIcon;
	public override string TabText { get; }

	protected HSplitContainer SplitContainer { get; }
	protected VBoxContainer LeftContainer { get; }
	protected PropertyCollection SpriteGridDetails { get; }
	protected SpriteGridDefinitionCollection SpriteCollection { get; }
	protected SpriteDetails SpriteDetails { get; }

	protected SpriteGridTextureView TextureView { get; }

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
			SpriteCollection.SpriteSelected += SpriteCollection_OnSpriteSelected;

			LeftContainer.AddChild(SpriteDetails = new SpriteDetails());
			SpriteDetails.Disable();
		}

		SplitContainer.AddChild(TextureView = new SpriteGridTextureView(spriteGrid));
	}

	private void SpriteCollection_OnSpriteSelected(SpriteGridDataInfo? data) {
		SpriteDetails.Proxy = data;
	}
}