using System.IO;
using DemonCastle.Editor;
using DemonCastle.Editor.FileInfo;
using DemonCastle.Game;
using DemonCastle.Game.SetupScreen;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Resources;
using DemonCastle.ProjectSelection;
using Godot;

namespace DemonCastle;

public partial class Main : Control {
    protected GameSetup? GameSetup { get; set; }
    protected EditorSpace? EditorSpace { get; set; }

    public override void _Ready() {
        base._Ready();

        LoadProjectMenuView();

        InputActions.RegisterActions();

        var arguments = new CliArguments();
        var projectPath = arguments.ProjectPath;
        if (projectPath == null) return;

        var projectResources = new ProjectResources(Path.GetDirectoryName(projectPath) ?? throw new DirectoryNotFoundException());
        var project = projectResources.GetProject(projectPath);
        LoadPlayProjectView(projectResources, project);
    }

    private void LoadProjectMenuView() {
        ClearChildren();

        GetWindow().Mode = Window.ModeEnum.Windowed;
        GetWindow().Title = "DemonCastle";
        ProjectSelectionMenu projectSelectionMenu;
        AddChild(projectSelectionMenu = new ProjectSelectionMenu());
        projectSelectionMenu.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, margin: 5);
        projectSelectionMenu.ProjectRun += LoadPlayProjectView;
        projectSelectionMenu.ProjectEdit += LoadEditProjectView;
    }

    protected void LoadPlayProjectView(ProjectResources resources, ProjectInfo project) {
        ClearChildren();

        GetWindow().Mode = Window.ModeEnum.Maximized;
        GetWindow().Title = $"DemonCastle - {project.Name}";
        GameRunner gameRunner;
        AddChild(gameRunner = new GameRunner(resources, project));
        gameRunner.SetAnchorsPreset(LayoutPreset.FullRect);
    }

    private void LoadEditProjectView(ProjectResources resources, ProjectInfo project) {
        ClearChildren();

        GetWindow().Mode = Window.ModeEnum.Maximized;
        GetWindow().Title = $"DemonCastle - {project.Name}";
        var projectPreferences = LoadProjectPreferences(resources);
        AddChild(EditorSpace = new EditorSpace(resources, project, projectPreferences));
        EditorSpace.GoToProjectMenu += LoadProjectMenuView;
    }

    private static ProjectPreferencesInfo LoadProjectPreferences(ProjectResources resources) {
        var root = resources.GetRoot();
        var preferencesFile = root.GetFile(".demoncastle/preferences.json");
        if (!preferencesFile.FileExists()) {
            preferencesFile.CreateFile("{}");
        }
        return ProjectPreferencesInfo.Load(preferencesFile, resources);
    }

    private void ClearChildren() {
        foreach (var child in GetChildren()) {
            child.QueueFree();
        }
    }
}