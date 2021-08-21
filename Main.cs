using DemonCastle.Game;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.SetupScreen;
using Godot;

namespace DemonCastle {
    public partial class Main : Node2D
    {
        protected GameSetup GameSetup { get; set; }

        public override void _Ready() {
            base._Ready();
            
            InputActions.RegisterActions();
        }

        protected void OnProjectLoaded(ProjectInfo project) {
            ProjectSelectionMenu.QueueFree();

            AddChild(GameSetup = new GameSetup(project));
            GameSetup.GameStart += OnGameStart;
        }

        private void OnGameStart(LevelInfo level, CharacterInfo character) {
            GameSetup.QueueFree();

            AddChild(new GameRunner(level, character));
        }
    }
}
