using DemonCastle.Editor;
using DemonCastle.Game;
using DemonCastle.Game.SetupScreen;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle; 

public partial class Main : Node2D {
    protected GameSetup GameSetup { get; set; }
    protected EditorSpace EditorSpace { get; set; }

    public override void _Ready() {
        base._Ready();
            
        InputActions.RegisterActions();
        GetWindow().Mode = Window.ModeEnum.Maximized;
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

    private void OnProjectEdit(ProjectInfo project) {
        ProjectSelectionMenu.QueueFree();

        GetWindow().Title = $"DemonCastle - {project.Name}";
        AddChild(EditorSpace = new EditorSpace(project));
    }
}