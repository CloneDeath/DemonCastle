using DemonCastle.Editor.Editors.Components.Properties;
using DemonCastle.Editor.Editors.TileSet.Tiles;
using DemonCastle.Editor.Editors.TileSet.Tiles.Collision;
using DemonCastle.Editor.Editors.TileSet.Tiles.Stairs;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Tiles;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinitions;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.Tools.TileTools;

public partial class LevelTileDetails : PropertyCollection {
	private readonly TileProxy _tileProxy = new();

	public TileInfo? Proxy {
		get => _tileProxy.Proxy;
		set {
			_tileProxy.Proxy = value;
			if (value == null) Disable();
			else Enable();
		}
	}

	public LevelTileDetails(string levelDirectory) {
		Name = nameof(LevelTileDetails);
		CustomMinimumSize = new Vector2I(160, 100);

		AddString("Name", _tileProxy, x => x.Name);
		var spriteReference = AddSpriteDefinition(_tileProxy, levelDirectory,
			e => e.SourceFile,
			e => e.SpriteId,
			t => t.SpriteOptions);
		spriteReference.ItemSelected += SpriteIdProperty_OnItemSelected;
		AddVector2I("Span", _tileProxy, x => x.Size);
		AddChild(new TileCollisionView(_tileProxy));

		Disable();
	}

	private void SpriteIdProperty_OnItemSelected(ISpriteDefinition obj) {
		if (string.IsNullOrEmpty(_tileProxy.Name)) {
			_tileProxy.Name = obj.Name;
		}
	}
}