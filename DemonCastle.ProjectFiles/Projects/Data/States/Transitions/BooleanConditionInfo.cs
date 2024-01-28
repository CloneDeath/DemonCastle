using System;
using DemonCastle.Files.Actions.Values;
using DemonCastle.Files.Conditions;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.States.Transitions;

public class BooleanConditionInfo : BaseInfo<EntityStateTransitionEvent>{
	private readonly WhenInfo _when;

	public BooleanConditionInfo(IFileNavigator file, EntityStateTransitionEvent data, WhenInfo when) : base(file, data) {
		_when = when;
	}

	public bool IsSet {
		get => Data.Condition != null;
		set {
			if (value) ClearOthers();
			Data.Condition = value ? Data.Condition ?? new BooleanConditionData() : null;
			Save();
			NotifyAllValuesChanges();
		}
	}

	public bool? Value {
		get => Data.Condition?.Value?.Value;
		set {
			ClearOthers();
			Data.Condition = new BooleanConditionData {
				Value = new BooleanValueData {
					Value = value
				}
			};
			Save();
			NotifyAllValuesChanges();
		}
	}

	public Guid? Variable {
		get => Data.Condition?.Value?.Variable;
		set {
			ClearOthers();
			Data.Condition = new BooleanConditionData {
				Value = new BooleanValueData {
					Variable = value
				}
			};
			Save();
			NotifyAllValuesChanges();
		}
	}

	private void ClearOthers() => _when.ClearAllExcept(nameof(Data.Condition));

	private void NotifyAllValuesChanges() {
		OnPropertyChanged(nameof(IsSet));
		OnPropertyChanged(nameof(Value));
		OnPropertyChanged(nameof(Variable));
	}
}