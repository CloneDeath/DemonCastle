using DemonCastle.Projects;
using Godot;

namespace DemonCastle {
	public partial class Main {
		protected ProjectSelectionMenu ProjectSelectionMenu { get; }

		public Main() {
			AddChild(ProjectSelectionMenu = new ProjectSelectionMenu());
		}
	}
}