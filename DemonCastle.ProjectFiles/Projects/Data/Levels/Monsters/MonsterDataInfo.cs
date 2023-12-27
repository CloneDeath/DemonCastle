using System;
using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.Locations;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Levels.Monsters;

public class MonsterDataInfo : BaseInfo<MonsterData> {
	private readonly AreaInfo _area;

	public MonsterDataInfo(IFileNavigator file, AreaInfo area, MonsterData data) : base(file, data) {
		_area = area;
	}

	public Guid MonsterId {
		get => Data.MonsterId;
		set {
			Data.MonsterId = value;
			Save();
			OnPropertyChanged();
		}
	}

	public Vector2 Position {
		get => Data.Position;
		set {
			Data.Position = value;
			Save();
			OnPropertyChanged();
			OnPropertyChanged(nameof(MonsterPosition));
		}
	}

	public MonsterPosition MonsterPosition => new(Data.Position, _area.PositionOfArea, _area.TileSize);
}