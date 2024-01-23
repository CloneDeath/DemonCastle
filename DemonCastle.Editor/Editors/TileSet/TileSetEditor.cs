using DemonCastle.ProjectFiles.Projects.Data.TileSets;
using Godot;

namespace DemonCastle.Editor.Editors.TileSet;

public partial class TileSetEditor : BaseEditor {
	public override Texture2D TabIcon => EditorFileType.TileSet.Icon;
	public override string TabText { get; }

	public TileSetEditor(TileSetInfo tileSet) {
		TabText = tileSet.FileName;
	}
}