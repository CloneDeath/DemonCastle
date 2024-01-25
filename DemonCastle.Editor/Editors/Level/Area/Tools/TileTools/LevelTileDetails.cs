using DemonCastle.Editor.Editors.Components.Properties;
using DemonCastle.Editor.Editors.TileSet.Tiles;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Tiles;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinitions;
using Godot;
using TileCollisionView = DemonCastle.Editor.Editors.TileSet.Tiles.Collision.TileCollisionView;
using TileStairView = DemonCastle.Editor.Editors.TileSet.Tiles.Stairs.TileStairView;

namespace DemonCastle.Editor.Editors.Level.Area.Tools.TileTools;

public partial class LevelTileDetails : PropertyCollection {
	private readonly TileProxy TileProxy = new();

	public TileInfo? Proxy {
		get => TileProxy.Proxy;
		set {
			TileProxy.Proxy = value;
			if (value == null) Disable();
			else Enable();
		}
	}

	public LevelTileDetails(string levelDirectory) {
		Name = nameof(LevelTileDetails);
		CustomMinimumSize = new Vector2I(160, 100);

		AddString("Name", TileProxy, x => x.Name);
		var spriteReference = AddSpriteDefinition(TileProxy, levelDirectory,
			e => e.SourceFile,
			e => e.SpriteId,
			t => t.SpriteOptions);
		spriteReference.ItemSelected += SpriteIdProperty_OnItemSelected;
		AddVector2I("Span", TileProxy, x => x.Size);
		AddChild(new TileCollisionView(TileProxy));
		AddChild(new TileStairView(TileProxy));

		Disable();
	}

	private void SpriteIdProperty_OnItemSelected(ISpriteDefinition obj) {
		if (string.IsNullOrEmpty(TileProxy.Name)) {
			TileProxy.Name = obj.Name;
		}
	}
}