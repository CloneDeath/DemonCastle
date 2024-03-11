using DemonCastle.Files.Conditions;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.Events;

public class EntityEventConditionInfo : BaseInfo<EntityEventConditionData> {
	public EntityEventConditionInfo(IFileNavigator file, EntityEventConditionData data) : base(file, data) { }

	public PlayerPositionTransition? AnyInput {
		get => Data.OnPlayer;
		set {
			Data.Clear();
			Data.OnPlayer = value;
			Save();
			OnPropertyChanged();
		}
	}
}