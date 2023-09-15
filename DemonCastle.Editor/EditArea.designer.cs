using System.Collections.Generic;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor; 

public partial class EditArea {
	protected AcceptDialog ErrorWindow { get; }

	public EditArea() {
		Name = nameof(EditArea);
		AddToGroup(nameof(EditArea));

		AddChild(ErrorWindow = new AcceptDialog {
			Exclusive = true
		}, false, InternalMode.Back);
	}
}