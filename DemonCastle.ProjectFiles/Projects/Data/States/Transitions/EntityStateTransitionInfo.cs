using System;
using DemonCastle.ProjectFiles.Files.BaseEntity;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.States.Transitions;

public class EntityStateTransitionInfo : BaseInfo<EntityStateTransitionData> {
	public EntityStateTransitionInfo(IFileNavigator file, EntityStateTransitionData data) : base(file, data) {
		When = new WhenInfo(file, data.When);
	}

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

	public WhenInfo When { get; }
}