using DemonCastle.Files.Actions;
using DemonCastle.ProjectFiles.Exceptions;
using DemonCastle.ProjectFiles.Projects.Data.SceneEvents.Types;
using DemonCastle.ProjectFiles.Projects.Resources;
using DemonCastle.ProjectFiles.State;

namespace DemonCastle.ProjectFiles.Projects.Data.SceneEvents;

public class SceneEventActionInfo : BaseInfo<SceneActionData> {
	public SceneEventActionInfo(IFileNavigator file, SceneActionData data) : base(file, data) {
		Scene = new SceneChangeActionInfo(file, data);
	}

	public GameAction? Game {
		get => Data.Game;
		set {
			Data.Clear();
			Data.Game = value;
			Save();
			OnPropertyChanged();
		}
	}

	public SceneChangeActionInfo Scene { get; }

	public string? SetCharacter {
		get => Data.SetCharacter;
		set {
			Data.Clear();
			Data.SetCharacter = value;
			Save();
			OnPropertyChanged();
		}
	}

	public string? SetLevel {
		get => Data.SetLevel;
		set {
			Data.Clear();
			Data.SetLevel = value;
			Save();
			OnPropertyChanged();
		}
	}

	public void TriggerAction(IGameState game) {
		if (Scene.IsSet) Scene.TriggerAction(game);
		else if (SetCharacter != null) game.SetCharacter(File.GetCharacter(SetCharacter));
		else if (SetLevel != null) game.SetLevel(File.GetLevel(SetLevel));
		else if (Game.HasValue) {
			switch (Game.Value) {
				case GameAction.Quit:
					game.Quit();
					break;
				case GameAction.Restart:
					game.Restart();
					break;
				default:
					throw new InvalidEnumValueException<GameAction>(Game.Value);
			}
		}
		else throw new IncompleteDataException(File.FilePath);
	}

	public override string ToString() {
		return SetCharacter != null ? $"Set Character: {SetCharacter}"
			   : SetLevel != null ? $"Set Level: {SetLevel}"
			   : Scene.IsSet ? $"Scene: {Scene}"
				: Game.HasValue ? $"Game: {Game}"
			   : "Invalid Action";
	}
}