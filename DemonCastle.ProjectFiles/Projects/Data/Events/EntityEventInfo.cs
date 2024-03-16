using DemonCastle.Files.Events;
using DemonCastle.ProjectFiles.Projects.Data.States.Events;
using DemonCastle.ProjectFiles.Projects.Resources;
using DemonCastle.ProjectFiles.State;

namespace DemonCastle.ProjectFiles.Projects.Data.Events;

public class EntityEventInfo : BaseInfo<EntityEventData>, IListableInfo {
	public EntityEventInfo(IFileNavigator file, EntityEventData data) : base(file, data) {
		When = new EntityEventConditionInfo(file, data.When);
		Then = new EntityActionInfoCollection(file, data.Then);
	}

	public string ListLabel => Name;

	public string Name {
		get => Data.Name;
		set {
			Data.Name = value;
			Save();
			OnPropertyChanged();
			OnPropertyChanged(nameof(ListLabel));
		}
	}

	public EntityEventConditionInfo When { get; }
	public EntityActionInfoCollection Then { get; }

	public void CheckAndTriggerEvent(IGameState game, IEntityState entity, EventDetails details) {
		if (When.IsConditionMet(details)) {
			Then.Execute(game, entity);
		}
	}
}