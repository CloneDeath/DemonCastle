using DemonCastle.Editor;
using DemonCastle.Files;
using DemonCastle.Game;
using DemonCastle.Game.SetupScreen;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Resources;
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

        var arguments = new CliArguments();
        var projectPath = arguments.ProjectPath;
        if (projectPath != null) {
            var fileNavigator = new FileNavigator<ProjectFile>(projectPath);
            var projectInfo = new ProjectInfo(fileNavigator);
            OnProjectLoaded(projectInfo);
        }
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