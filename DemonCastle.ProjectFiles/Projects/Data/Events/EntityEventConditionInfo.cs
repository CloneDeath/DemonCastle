using DemonCastle.Files.Conditions;
using DemonCastle.ProjectFiles.Exceptions;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.Events;

public class EntityEventConditionInfo : BaseInfo<EntityEventConditionData> {
	public EntityEventConditionInfo(IFileNavigator file, EntityEventConditionData data) : base(file, data) { }

	public PlayerPositionTransition? OnPlayer {
		get => Data.OnPlayer;
		set {
			Data.Clear();
			Data.OnPlayer = value;
			Save();
			OnPropertyChanged();
		}
	}

	public bool IsConditionMet(EventDetails details) {
		if (OnPlayer.HasValue) {
			return OnPlayer.Value switch {
				PlayerPositionTransition.Enter => details.OnPlayerEnter,
				PlayerPositionTransition.Exit => details.OnPlayerExit,
				_ => throw new InvalidEnumValueException<PlayerPositionTransition>(OnPlayer.Value)
			};
		}

		throw new IncompleteDataException(File.FilePath);
	}
}