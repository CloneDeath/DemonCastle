using System;
using DemonCastle.Files.BaseEntity;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.States.Transitions;

public class EntityStateTransitionInfo : BaseInfo<EntityStateTransitionData>, IListableInfo {
	private readonly IEntityStateInfoRetriever _states;

	public EntityStateTransitionInfo(IFileNavigator file, IEntityStateInfoRetriever states, EntityStateTransitionData data) : base(file, data) {
		_states = states;
		When = new WhenInfo(file, data.When);
	}

	public Guid Id => Data.Id;
	public string ListLabel => $"[{Name}] -> {TargetStateInfo?.ListLabel ?? "None"}";

	public string Name {
		get => Data.Name;
		set {
			Data.Name = value;
			Save();
			OnPropertyChanged();
			OnPropertyChanged(nameof(ListLabel));
		}
	}

	public Guid TargetState {
		get => Data.TargetState;
		set {
			Data.TargetState = value;
			Save();
			OnPropertyChanged();
			OnPropertyChanged(nameof(ListLabel));
			OnPropertyChanged(nameof(TargetStateInfo));
		}
	}

	public WhenInfo When { get; }

	public EntityStateInfo? TargetStateInfo => _states.RetrieveEntityStateInfo(TargetState);
}