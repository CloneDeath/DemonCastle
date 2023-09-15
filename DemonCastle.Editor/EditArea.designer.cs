using System.Collections.Generic;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor; 

public partial class EditArea {
	protected AcceptDialog ErrorWindow { get; }

	public EditArea() {
		Name = nameof(EditArea);
		DragToRearrangeEnabled = true;
		AddToGroup(nameof(EditArea));
		
		GetChild<TabBar>(0, true).TabCloseDisplayPolicy = TabBar.CloseButtonDisplayPolicy.ShowActiveOnly;
		GetChild<TabBar>(0, true).TabClosePressed += OnTabButtonPressed;

		AddChild(ErrorWindow = new AcceptDialog {
			Exclusive = true
		}, false, InternalMode.Back);
	}
}