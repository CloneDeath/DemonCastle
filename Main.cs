using System.IO;
using DemonCastle.Editor;
using DemonCastle.Game;
using DemonCastle.Game.SetupScreen;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle;

public partial class Main : Control {
    protected GameSetup? GameSetup { get; set; }
    protected EditorSpace? EditorSpace { get; set; }

    public override void _Ready() {
        base._Ready();

        LoadProjectMenuView();

        InputActions.RegisterActions();
        GetWindow().Mode = Window.ModeEnum.Maximized;

        var arguments = new CliArguments();
        var projectPath = arguments.ProjectPath;
        if (projectPath == null) return;

        var projectResources = new ProjectResources(Path.GetDirectoryName(projectPath) ?? throw new DirectoryNotFoundException());
        var project = projectResources.GetProject(projectPath);
        LoadPlayProjectView(project);
    }

    private void LoadProjectMenuView() {
        ClearChildren();

        GetWindow().Title = $"DemonCastle";
        ProjectSelectionMenu projectSelectionMenu;
        AddChild(projectSelectionMenu = new ProjectSelectionMenu());
        projectSelectionMenu.ProjectLoaded += LoadPlayProjectView;
        projectSelectionMenu.ProjectEdit += LoadEditProjectView;
    }

    protected void LoadPlayProjectView(ProjectInfo project) {
        ClearChildren();

        GetWindow().Title = $"DemonCastle - {project.Name}";
        GameRunner gameRunner;
        AddChild(gameRunner = new GameRunner(project));
        gameRunner.SetAnchorsPreset(LayoutPreset.FullRect);
    }

    private void LoadEditProjectView(ProjectInfo project) {
        ClearChildren();

        GetWindow().Title = $"DemonCastle - {project.Name}";
        AddChild(EditorSpace = new EditorSpace(project));
        EditorSpace.GoToProjectMenu += LoadProjectMenuView;
    }

    private void ClearChildren() {
        foreach (var child in GetChildren()) {
            child.QueueFree();
        }
    }
}