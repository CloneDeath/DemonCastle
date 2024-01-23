using DemonCastle.ProjectFiles.Projects.Data.TileSets;
using Godot;

namespace DemonCastle.Editor.Editors.TileSet;

public partial class TileSetEditor : BaseEditor {
	public override Texture2D TabIcon => EditorFileType.TileSet.Icon;
	public override string TabText { get; }

	protected HSplitContainer SplitContainer { get; }

	public TileSetEditor(TileSetInfo tileSet) {
		TabText = tileSet.FileName;

		AddChild(SplitContainer = new HSplitContainer());
		SplitContainer.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, margin: 5);

		SplitContainer.AddChild(new TileSetDetails(tileSet));
	}
}