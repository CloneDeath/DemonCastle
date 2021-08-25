using System.Collections.Generic;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor {
	public partial class WindowContainer {
		protected Dictionary<FileNavigator, WindowDialog> WindowFileMap { get; } =
			new Dictionary<FileNavigator, WindowDialog>();


	}
}