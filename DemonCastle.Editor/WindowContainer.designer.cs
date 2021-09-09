using System.Collections.Generic;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor {
	public partial class WindowContainer {
		protected Dictionary<FileNavigator, WindowDialog> WindowFileMap { get; } =
			new Dictionary<FileNavigator, WindowDialog>();
		
		protected AcceptDialog ErrorWindow { get; }

		public WindowContainer() {
			Name = nameof(WindowContainer);
			
			AddChild(ErrorWindow = new AcceptDialog {
				PopupExclusive = true
			}); 
		}
	}
}