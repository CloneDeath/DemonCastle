using System;
using DemonCastle.ProjectFiles.Files.BaseEntity;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.States.Transitions;

public class RandomTimerExpiresInfo : BaseInfo<MonsterStateTransitionEvent> {
	public RandomTimerExpiresInfo(IFileNavigator file, MonsterStateTransitionEvent data) : base(file, data) { }

	public bool IsSet {
		get => Data.RandomTimerExpires != null;
		set {
			Data.RandomTimerExpires = value ? Data.RandomTimerExpires ?? new RandomTimerExpires() : null;
			Save();
			OnPropertyChanged();
		}
	}

	public TimeSpan Start {
		get => TimeSpan.FromSeconds(Data.RandomTimerExpires?.Start.Seconds ?? 1);
		set {
			if (Data.RandomTimerExpires == null) return;
			Data.RandomTimerExpires.Start.Seconds = (float)value.TotalSeconds;
			Save();
			OnPropertyChanged();
		}
	}

	public TimeSpan End {
		get => TimeSpan.FromSeconds(Data.RandomTimerExpires?.End.Seconds ?? 2);
		set {
			if (Data.RandomTimerExpires == null) return;
			Data.RandomTimerExpires.End.Seconds = (float)value.TotalSeconds;
			Save();
			OnPropertyChanged();
		}
	}
}