using System;
using Godot;

namespace DemonCastle.ProjectSelection;

public partial class ProjectActions : VBoxContainer {
	public event Action? EditPressed;
	public event Action? RunPressed;
	public event Action? RemovePressed;

	protected Button EditButton { get; }
	protected Button RunButton { get; }
	protected Button RemoveButton { get; }

	public bool Disabled {
		get => EditButton.Disabled;
		set {
			EditButton.Disabled = value;
			RunButton.Disabled = value;
			RemoveButton.Disabled = value;
		}
	}

	public ProjectActions() {
		Name = nameof(ProjectActions);

		AddChild(EditButton = new Button { Text = "Edit" });
		EditButton.Pressed += () => EditPressed?.Invoke();

		AddChild(RunButton = new Button { Text = "Run" });
		RunButton.Pressed += () => RunPressed?.Invoke();

		AddChild(RemoveButton = new Button { Text = "Remove" });
		RemoveButton.Pressed += () => RemovePressed?.Invoke();
	}
}