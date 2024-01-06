using System;
using DemonCastle.Files.BaseEntity;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.States.Transitions;

public class EntityStateTransitionInfo : BaseInfo<EntityStateTransitionData>, IListableInfo {
	public EntityStateTransitionInfo(IFileNavigator file, EntityStateTransitionData data) : base(file, data) {
		When = new WhenInfo(file, data.When);
	}

	public Guid Id => Data.Id;
	public string ListLabel => $"[{Name}]: -> {TargetState}";

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