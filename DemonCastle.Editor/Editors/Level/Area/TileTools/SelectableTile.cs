using DemonCastle.Editor.Editors.Components;
using DemonCastle.ProjectFiles.Projects.Data.Levels;

namespace DemonCastle.Editor.Editors.Level.Area.TileTools;

public partial class SelectableTile : SelectableControl {
	protected Outline Outline;

	public TileInfo Tile { get; }

	public SelectableTile(TileInfo tile) {
		Name = nameof(SelectableTile);
		Tile = tile;

		AddChild(new SpriteDefinitionView(tile.Sprite));
		AddChild(Outline = new Outline {
			Visible = false
		});
		Outline.SetAnchorsPreset(LayoutPreset.FullRect);
	}

	public override void _Process(double delta) {
		base._Process(delta);
		Outline.Visible = IsSelected;
		CustomMinimumSize = Tile.Region.Size;
	}

	protected override void OnSelected() {
		base.OnSelected();
		DeselectSiblings();
	}
}