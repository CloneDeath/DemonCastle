using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.States.Transitions;

public class WhenInfo : BaseInfo<MonsterStateTransitionEvent> {
	public WhenInfo(IFileNavigator file, MonsterStateTransitionEvent data) : base(file, data) {
		RandomTimerExpires = new RandomTimerExpiresInfo(File, data);
	}

	public SelfEvents? Self {
		get => Data.Self;
		set {
			Data.Self = value;
			Save();
			OnPropertyChanged();
		}
	}

	public AnimationEvent? Animation {
		get => Data.Animation;
		set {
			Data.Animation = value;
			Save();
			OnPropertyChanged();
		}
	}

	public RandomTimerExpiresInfo RandomTimerExpires { get; }
}