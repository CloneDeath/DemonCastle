using DemonCastle.ProjectFiles.Projects;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.SetupScreen {
	public partial class GameSetup {
		protected Label CharactersLabel { get; }
		protected InfoItemList<CharacterInfo> CharacterInfoList { get; }
		
		protected Label LevelsLabel { get; }
		protected InfoItemList<LevelInfo> LevelInfoList { get; }
		
		protected Button LaunchButton { get; }
		
		public GameSetup(ProjectInfo project) {
			AddChild(CharactersLabel = new Label {
				Text = "Characters:",
				RectPosition = new Vector2(10, 10)
			});
			AddChild(CharacterInfoList = new InfoItemList<CharacterInfo> {
				RectPosition = CharactersLabel.RectPosition + new Vector2(0, 20)
			});
			CharacterInfoList.Load(project.Characters);
			
			AddChild(LevelsLabel = new Label {
				Text = "Levels:",
				RectPosition = CharactersLabel.RectPosition + new Vector2(310, 0)
			});
			AddChild(LevelInfoList = new InfoItemList<LevelInfo> {
				RectPosition = LevelsLabel.RectPosition + new Vector2(0, 20)
			});
			LevelInfoList.Load(project.Levels);
			
			AddChild(LaunchButton = new Button {
				Text = "Start Chapter",
				RectPosition = CharacterInfoList.RectPosition + new Vector2(0, 310)
			});
			LaunchButton.Connect("pressed", this, nameof(OnLaunchButtonClicked));
		}
	}
}