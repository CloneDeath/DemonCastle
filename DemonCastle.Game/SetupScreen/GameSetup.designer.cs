using DemonCastle.ProjectFiles.Projects;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Game.SetupScreen {
	public partial class GameSetup {
		protected Label CharactersLabel { get; }
		protected InfoItemList<CharacterInfo> CharacterInfoList { get; }
		
		protected Label LevelsLabel { get; }
		protected InfoItemList<LevelInfo> LevelInfoList { get; }
		
		protected Button LaunchButton { get; }
		
		public GameSetup(ProjectInfo project) {
			AddChild(CharactersLabel = new Label {
				Text = "Characters:",
				Position = new Vector2(10, 10)
			});
			AddChild(CharacterInfoList = new InfoItemList<CharacterInfo> {
				Position = CharactersLabel.Position + new Vector2(0, 20)
			});
			CharacterInfoList.Load(project.Characters);
			
			AddChild(LevelsLabel = new Label {
				Text = "Levels:",
				Position = CharactersLabel.Position + new Vector2(310, 0)
			});
			AddChild(LevelInfoList = new InfoItemList<LevelInfo> {
				Position = LevelsLabel.Position + new Vector2(0, 20)
			});
			LevelInfoList.Load(project.Levels);
			
			AddChild(LaunchButton = new Button {
				Text = "Start Chapter",
				Position = CharacterInfoList.Position + new Vector2(0, 310)
			});
			LaunchButton.Pressed += this.OnLaunchButtonClicked;;
		}
	}
}