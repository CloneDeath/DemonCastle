using System;
using DemonCastle.Projects.Data;
using Godot;

namespace DemonCastle.SetupScreen {
	public partial class GameSetup : Container {
		public event Action<LevelInfo, CharacterInfo> GameStart;

		public override void _Process(float delta) {
			base._Process(delta);

			LaunchButton.Disabled = LevelInfoList.NoItemSelected || CharacterInfoList.NoItemSelected;
		}

		protected void OnLaunchButtonClicked() {
			GameStart?.Invoke(LevelInfoList.SelectedItem, CharacterInfoList.SelectedItem);
		}
	}
}