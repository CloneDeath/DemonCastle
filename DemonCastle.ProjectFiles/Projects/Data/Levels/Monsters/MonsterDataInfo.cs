using System;
using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.Levels.Monsters;

public class MonsterDataInfo : BaseInfo<MonsterData> {
	public MonsterDataInfo(IFileNavigator file, MonsterData data) : base(file, data) { }

	public Guid Id => Data.MonsterId;

	public Position2D Position {
		get => Data.Position;
		set {
			Data.Position = value;
			Save();
			OnPropertyChanged();
		}
	}
}