using DemonCastle.ProjectFiles.Projects.Data.States;
using Godot;

namespace DemonCastle.Editor.Editors.Monster.States.Editor.Transitions;

public partial class TransitionsEditor : VBoxContainer {
	private readonly StateInfoProxy _proxy = new();

	public StateInfo? State {
		get => _proxy.Proxy;
		set => _proxy.Proxy = value;
	}

	private ItemList Transitions { get; }
	private ItemList When { get; }

	public TransitionsEditor() {
		Name = nameof(TransitionsEditor);

		AddChild(Transitions = new ItemList {
			SizeFlagsVertical = SizeFlags.ExpandFill
		});
		Transitions.AddItem("-> x");
		Transitions.AddItem("-> y");
		Transitions.AddItem("-> z");

		AddChild(When = new ItemList {
			SizeFlagsVertical = SizeFlags.ExpandFill
		});
		When.AddItem("animation.isComplete");
	}
}