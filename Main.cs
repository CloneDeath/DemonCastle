using DemonCastle.Editor;
using DemonCastle.Game;
using DemonCastle.Game.SetupScreen;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle;

public partial class Main : Control {
    protected ProjectSelectionMenu ProjectSelectionMenu { get; }

    protected GameSetup? GameSetup { get; set; }
    protected EditorSpace? EditorSpace { get; set; }

    public Main() {
        AddChild(ProjectSelectionMenu = new ProjectSelectionMenu());
        ProjectSelectionMenu.ProjectLoaded += OnProjectLoaded;
        ProjectSelectionMenu.ProjectEdit += OnProjectEdit;
    }


    public override void _Ready() {
        base._Ready();

        InputActions.RegisterActions();
        GetWindow().Mode = Window.ModeEnum.Maximized;
    }

    protected void OnProjectLoaded(ProjectInfo project) {
        ProjectSelectionMenu.QueueFree();

        var gameRunner = new GameRunner(project);
        AddChild(gameRunner);
        gameRunner.SetAnchorsPreset(LayoutPreset.FullRect);
    }

    private void OnProjectEdit(ProjectInfo project) {
        ProjectSelectionMenu.QueueFree();

        GetWindow().Title = $"DemonCastle - {project.Name}";
        AddChild(EditorSpace = new EditorSpace(project));
    }
}