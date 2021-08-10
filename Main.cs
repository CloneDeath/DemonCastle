using DemonCastle.Projects.Data;
using DemonCastle.SetupScreen;
using Godot;

namespace DemonCastle {
    public partial class Main : Node2D
    {
        protected GameSetup GameSetup { get; set; }
        
        protected void OnProjectLoaded(ProjectInfo project) {
            ProjectSelectionMenu.QueueFree();

            AddChild(GameSetup = new GameSetup(project));
            GameSetup.GameStart += OnGameStart;
        }

        private void OnGameStart(LevelInfo level, CharacterInfo character) {
            GameSetup.QueueFree();
            
            
        }
    }
}
