using DemonCastle.Editor.Editors.Properties;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.TileTools;

public partial class TileDetails : PropertyCollection {
	private readonly TileProxy TileProxy = new();

	public TileInfo? Proxy {
		get => TileProxy.Proxy;
		set {
			TileProxy.Proxy = value;
			if (value == null) {
				DisableProperties();
			} else {
				EnableProperties();
			}
		}
	}

	public TileDetails() {
		Name = nameof(TileDetails);
		CustomMinimumSize = new Vector2I(160, 100);

		AddString("Name", TileProxy, x => x.Name);
		AddFile("Source", TileProxy, TileProxy.Directory,  t => t.SourceFile, FileType.SpriteSources);
		AddSpriteReference("Sprite", TileProxy, x => x.SpriteId, TileProxy.SpriteOptions);

		DisableProperties();
	}
}