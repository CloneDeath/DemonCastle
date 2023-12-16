using System;
using DemonCastle.ProjectFiles.Extensions;
using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Levels.Monsters;

public class MonsterDataInfo : BaseInfo<MonsterData> {
	public MonsterDataInfo(IFileNavigator file, MonsterData data) : base(file, data) { }

	public Guid MonsterId {
		get => Data.MonsterId;
		set {
			Data.MonsterId = value;
			Save();
			OnPropertyChanged();
		}
	}

	public Vector2 Position {
		get => Data.Position.ToVector2();
		set {
			Data.Position = value.ToPosition2D();
			Save();
			OnPropertyChanged();
		}
	}
}