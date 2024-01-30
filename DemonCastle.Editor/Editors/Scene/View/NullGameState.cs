using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DemonCastle.Files.Conditions;
using DemonCastle.ProjectFiles.Locations;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.State;
using Godot;

namespace DemonCastle.Editor.Editors.Scene.View;

public class NullGameState : IGameState {
	public IPlayerState Player { get; } = new NullPlayerState();
	public ICurrentArea? CurrentArea { get; } = new NullAreaState();
	public IVariables Variables { get; } = new NullVariableCollection();

	public void SetCharacter(CharacterInfo character) {	}
	public void SetLevel(LevelInfo level) {	}
	public void PushScene(SceneInfo scene) { }
	public void PopScene(int number) {	}
	public void SetScene(SceneInfo scene) {	}

	public IInputState Input => new NullInputState();
	public Texture2D LevelView => new GradientTexture2D {
		Gradient = new Gradient {
			Colors = new[] {
				Colors.Silver,
				Colors.LightGray
			},
			Offsets = new[] {
				0f, 1f
			}
		}
	};

	public void SpawnItem(Guid itemId, Vector2 position) { }
	public void Restart() { }
	public void Quit() { }
}

public class NullInputState : IInputState {
	public bool AnyInputIsInState(KeyState state) => false;
	public bool InputIsInState(PlayerAction action, KeyState state) => false;
}

public class NullPlayerState : IPlayerState {
	public int HP => 9;
	public int MaxHP => 9;
	public int MP => 9;
	public int MaxMP => 9;
	public int Lives => 3;
	public int? MaxLives => 9;
	public int Score => 42;
	public Vector2 Position => Vector2.Zero;

	#region INotifyPropertyChanged
	public event PropertyChangedEventHandler? PropertyChanged;

	protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null) {
		if (EqualityComparer<T>.Default.Equals(field, value)) return false;
		field = value;
		OnPropertyChanged(propertyName);
		return true;
	}
	#endregion
}

public class NullAreaState : ICurrentArea {
	public AreaPosition Position { get; } = new(Vector2I.Zero, Vector2I.One * 10, Vector2I.One * 16);
}

public class NullVariableCollection : IVariables {
	public Guid GetGuid(Guid variableId) => Guid.NewGuid();

	public Vector2I GetVector2I(Guid variableId) => Vector2I.Zero;

	public bool HasBoolean(Guid variableId) => true;

	public bool GetBoolean(Guid variableId) => true;

	public void SetBoolean(Guid variableId, bool value) {}
}