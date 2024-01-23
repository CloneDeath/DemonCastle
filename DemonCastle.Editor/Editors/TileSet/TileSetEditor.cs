using DemonCastle.Editor;
using DemonCastle.Editor.Editors;
using Godot;

public partial class TileSetEditor : BaseEditor {
	public override Texture2D TabIcon => EditorFileType.TileSet.Icon;
	public override string TabText { get; }

	public TileSetEditor(TileSetInfo tileSet) {

	}
}