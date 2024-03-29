using System.Collections.Generic;
using DemonCastle.Editor.Editors.Components.Properties;
using DemonCastle.Editor.Editors.Components.Properties.Reference;
using DemonCastle.Editor.Editors.TileSet.Tiles.Collision;
using DemonCastle.Editor.Editors.TileSet.Tiles.Stairs;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Tiles;
using DemonCastle.ProjectFiles.Projects.Data.States;
using Godot;

namespace DemonCastle.Editor.Editors.TileSet.Tiles;

public partial class TileDetails : PropertyCollection {
	private readonly TileProxy _tile = new();
	private readonly StateReferenceProperty _stateReference;

	public TileInfo? Tile {
		get => _tile.Proxy;
		set {
			_tile.Proxy = value;
			_stateReference.LoadOptions(value == null ? new EnumerableInfoWrapper<EntityStateInfo>(new List<EntityStateInfo>()) : value.States);
			if (value == null) Disable();
			else Enable();
		}
	}

	public TileDetails() {
		Name = nameof(TileDetails);

		AddString("Name", _tile, m => m.Name, InternalMode.Front);
		AddVector2I("Size", _tile, p => p.Size);
		AddChild(new TileCollisionView(_tile));
		_stateReference = AddStateReference("Initial State", _tile, m => m.InitialState, new EnumerableInfoWrapper<EntityStateInfo>(new List<EntityStateInfo>()));
		AddChild(new HSeparator());
		AddChild(new TileStairView(_tile));
	}
}