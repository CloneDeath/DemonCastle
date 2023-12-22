using DemonCastle.Editor;
using DemonCastle.Game;
using DemonCastle.Game.DebugNodes;
using DemonCastle.Game.SetupScreen;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle;

public partial class Main : Control {
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
        GameSetup.GameStart += (level, character, debug) => OnGameStart(project, level, character, debug);
    }

    private void OnGameStart(ProjectInfo project, LevelInfo level, CharacterInfo character, DebugState debug) {
        GameSetup.QueueFree();

        var gameRunner = new GameRunner(project, level, character, debug);
        AddChild(gameRunner);
        gameRunner.SetAnchorsPreset(LayoutPreset.FullRect);
    }

    private void OnProjectEdit(ProjectInfo project) {
        ProjectSelectionMenu.QueueFree();

        GetWindow().Title = $"DemonCastle - {project.Name}";
        AddChild(EditorSpace = new EditorSpace(project));
    }
}