using System;
using DemonCastle.Files.Conditions;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.States.Transitions;

public class RandomTimerExpiresInfo : BaseInfo<EntityStateTransitionEvent> {
	private readonly WhenInfo _when;

	public RandomTimerExpiresInfo(IFileNavigator file, EntityStateTransitionEvent data, WhenInfo when) : base(file, data) {
		_when = when;
	}

	public bool IsSet {
		get => Data.RandomTimerExpires != null;
		set {
			if (value) ClearOthers();
			Data.RandomTimerExpires = value ? Data.RandomTimerExpires ?? new RandomTimerExpires() : null;
			Save();
			OnPropertyChanged();
			OnPropertyChanged(nameof(Start));
			OnPropertyChanged(nameof(End));
		}
	}

	public TimeSpan Start {
		get => TimeSpan.FromSeconds(Data.RandomTimerExpires?.Start.Seconds ?? 1);
		set {
			ClearOthers();
			Data.RandomTimerExpires ??= new RandomTimerExpires();
			Data.RandomTimerExpires.Start.Seconds = (float)value.TotalSeconds;
			Save();
			OnPropertyChanged();
		}
	}

	public TimeSpan End {
		get => TimeSpan.FromSeconds(Data.RandomTimerExpires?.End.Seconds ?? 2);
		set {
			ClearOthers();
			Data.RandomTimerExpires ??= new RandomTimerExpires();
			Data.RandomTimerExpires.End.Seconds = (float)value.TotalSeconds;
			Save();
			OnPropertyChanged();
		}
	}

	private void ClearOthers() => _when.ClearAllExcept(nameof(Data.RandomTimerExpires));
}