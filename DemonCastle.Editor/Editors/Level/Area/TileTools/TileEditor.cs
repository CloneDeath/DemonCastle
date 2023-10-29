using DemonCastle.Editor.Editors.Properties;
using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.TileTools;

public partial class TileEditor : BaseEditor {
	public override Texture2D TabIcon => IconTextures.LevelIcon;
	public override string TabText { get; }

	protected PropertyCollection Properties { get; }

	public TileEditor(TileInfo tileInfo) {
		Name = nameof(TileEditor);
		TabText = "Tile Window";
		CustomMinimumSize = new Vector2I(80, 100);

		AddChild(Properties = new PropertyCollection());
		Properties.AddString("Name", tileInfo, x => x.Name);
		Properties.AddFile("Source", tileInfo, tileInfo.Directory,  t => t.SourceFile, FileType.SpriteSources);
		Properties.AddSpriteName("Sprite", tileInfo, x => x.SpriteName, tileInfo.SpriteOptions);
	}
}