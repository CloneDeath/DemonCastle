using DemonCastle.Projects;
using DemonCastle.SetupScreen;
using Godot;

namespace DemonCastle {
    public partial class Main : Node2D
    {
        protected void OnProjectLoaded(ProjectInfo project) {
            RemoveChild(ProjectSelectionMenu);
            AddChild(new GameSetup(project));
        }
    }
}
