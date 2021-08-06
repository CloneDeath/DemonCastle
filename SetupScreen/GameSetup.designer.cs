using DemonCastle.Projects;
using DemonCastle.Projects.Data;
using Godot;

namespace DemonCastle.SetupScreen {
	public partial class GameSetup {
		protected InfoItemList<CharacterInfo> CharacterInfoList { get; }
		
		public GameSetup(ProjectInfo project) {
			AddChild(CharacterInfoList = new InfoItemList<CharacterInfo> {
				RectPosition = new Vector2(10, 10)
			});
			CharacterInfoList.Load(project.Characters);
		}
	}
}