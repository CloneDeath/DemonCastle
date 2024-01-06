using System;
using DemonCastle.Files.BaseEntity;
using DemonCastle.ProjectFiles.Projects.Data.States.Events;
using DemonCastle.ProjectFiles.Projects.Data.States.Transitions;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.States;

public class EntityStateInfo : BaseInfo<EntityStateData>, IListableInfo {
	public EntityStateInfo(IFileNavigator file, IEntityStateInfoRetriever states, EntityStateData state) : base(file, state) {
		OnEnter = new EntityActionInfoCollection(file, state.OnEnter);
		OnUpdate = new EntityActionInfoCollection(file, state.OnUpdate);
		OnExit = new EntityActionInfoCollection(file, state.OnExit);
		Transitions = new EntityStateTransitionInfoCollection(file, states, state.Transitions);
	}

	public Guid Id => Data.Id;

	public string ListLabel => Name;

	public string Name {
		get => Data.Name;
		set {
			Data.Name = value;
			Save();
			OnPropertyChanged();
			OnPropertyChanged(ListLabel);
		}
	}

	public Guid Animation {
		get => Data.Animation;
		set {
			Data.Animation = value;
			Save();
			OnPropertyChanged();
		}
	}

	public EntityActionInfoCollection OnEnter { get; }
	public EntityActionInfoCollection OnUpdate { get; }
	public EntityActionInfoCollection OnExit { get; }
	public EntityStateTransitionInfoCollection Transitions { get; }
}