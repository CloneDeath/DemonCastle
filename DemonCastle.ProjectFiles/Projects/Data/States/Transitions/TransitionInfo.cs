using System;
using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.States.Transitions;

public class TransitionInfo : BaseInfo<MonsterStateTransitionData> {
	public TransitionInfo(IFileNavigator file, MonsterStateTransitionData data) : base(file, data) { }

	public Guid Id {
		get => Data.Id;
		set {
			Data.Id = value;
			Save();
			OnPropertyChanged();
		}
	}

	public string Name {
		get => Data.Name;
		set {
			Data.Name = value;
			Save();
			OnPropertyChanged();
		}
	}

	public Guid TargetState {
		get => Data.TargetState;
		set {
			Data.TargetState = value;
			Save();
			OnPropertyChanged();
		}
	}
}