using DemonCastle.Editor;
using DemonCastle.ProjectFiles.Projects;
using Godot;

namespace DemonCastle {
	public partial class Main {
		protected ProjectSelectionMenu ProjectSelectionMenu { get; }

		public Main() {
			AddChild(ProjectSelectionMenu = new ProjectSelectionMenu());
			ProjectSelectionMenu.ProjectLoaded += OnProjectLoaded;
			ProjectSelectionMenu.ProjectEdit += OnProjectEdit;
		}
	}
}