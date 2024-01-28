using DemonCastle.Files.Conditions;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.States.Transitions;

public class BooleanConditionInfo : BaseInfo<EntityStateTransitionEvent>{
	public BooleanConditionInfo(IFileNavigator file, EntityStateTransitionEvent data) : base(file, data) { }

	public bool IsSet {
		get => Data.RandomTimerExpires != null;
		set {
			Data.RandomTimerExpires = value ? Data.RandomTimerExpires ?? new RandomTimerExpires() : null;
			Save();
			OnPropertyChanged();
		}
	}
}