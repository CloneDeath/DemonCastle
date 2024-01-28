using System;
using DemonCastle.Files.Conditions;
using DemonCastle.Files.Conditions.Events;
using DemonCastle.ProjectFiles.Projects.Resources;
using DemonCastle.ProjectFiles.State;

namespace DemonCastle.ProjectFiles.Projects.Data.States.Transitions;

public class WhenInfo : BaseInfo<EntityStateTransitionEvent>, IClearParent {
	public WhenInfo(IFileNavigator file, EntityStateTransitionEvent data) : base(file, data) {
		RandomTimerExpires = new RandomTimerExpiresInfo(File, data, this);
		Condition = new BooleanConditionInfo(File, () => data.Condition, v => data.Condition = v, nameof(data.Condition), this);
	}

	public SelfEvent? Self {
		get => Data.Self;
		set => SaveField(ref Data.Self, value);
	}

	public AnimationEvent? Animation {
		get => Data.Animation;
		set => SaveField(ref Data.Animation, value);
	}

	public RandomTimerExpiresInfo RandomTimerExpires { get; }

	public BooleanConditionInfo Condition { get; }


	public void ClearAllExcept(string dataName) {
		if (dataName != nameof(Data.Self)) Self = null;
		if (dataName != nameof(Data.Animation)) Animation = null;
		if (dataName != nameof(Data.RandomTimerExpires)) RandomTimerExpires.IsSet = false;
		if (dataName != nameof(Data.Condition)) Condition.IsSet = false;
	}

	public bool IsConditionMet(IEntityState entity) {
		if (Self != null) {
			return entity.WasKilled;
		}
		if (Animation != null) {
			throw new NotImplementedException();
		}
		if (RandomTimerExpires != null) {
			throw new NotImplementedException();
		}
		if (Condition != null) {
			throw new NotImplementedException();
		}
		throw new NotSupportedException("No condition was set!");
	}
}