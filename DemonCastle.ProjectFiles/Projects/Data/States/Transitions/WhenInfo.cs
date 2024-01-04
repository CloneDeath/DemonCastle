using DemonCastle.ProjectFiles.Files.Conditions;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.States.Transitions;

public class WhenInfo : BaseInfo<EntityStateTransitionEvent> {
	public WhenInfo(IFileNavigator file, EntityStateTransitionEvent data) : base(file, data) {
		RandomTimerExpires = new RandomTimerExpiresInfo(File, data);
	}

	public SelfEvent? Self {
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