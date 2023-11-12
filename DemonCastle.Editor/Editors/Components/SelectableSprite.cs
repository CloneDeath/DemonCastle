using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;

namespace DemonCastle.Editor.Editors.Components;

public partial class SelectableSprite : SelectableControl {
	protected SpriteDefinitionView SpriteDefinitionView { get; }
	protected Outline Outline { get; }

	public ISpriteDefinition SpriteDefinition { get; }

	public SelectableSprite(ISpriteDefinition spriteDefinition) {
		Name = nameof(SelectableSprite);
		SpriteDefinition = spriteDefinition;

		DefaultCursorShape = CursorShape.PointingHand;
		SelectedCursorShape = CursorShape.Arrow;

		AddChild(SpriteDefinitionView = new SpriteDefinitionView(spriteDefinition));
		AddChild(Outline = new Outline {
			Visible = false
		});
		Outline.SetAnchorsPreset(LayoutPreset.FullRect);
	}

	public override void _Process(double delta) {
		base._Process(delta);
		Outline.Visible = IsSelected;
		CustomMinimumSize = SpriteDefinition.Region.Size;
	}

	protected override void OnSelected() {
		base.OnSelected();
		DeselectSiblings();
	}
}