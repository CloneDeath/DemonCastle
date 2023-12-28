using System;
using System.Collections.Specialized;
using System.Linq;
using DemonCastle.ProjectFiles.Projects.Data.SceneEvents;
using Godot;

namespace DemonCastle.Editor.Editors.Scene.Events;

public partial class SceneEventsList : VBoxContainer {
	public event Action<SceneEventInfo?>? SceneEventSelected;

	private readonly SceneEventInfoCollection _events;

	private Button AddButton { get; }
	private ItemList Events { get; }
	private Button RemoveButton { get; }

	public SceneEventsList(SceneEventInfoCollection events) {
		_events = events;

		Name = nameof(SceneEventsList);

		AddChild(AddButton = new Button { Text = "Add" });
		AddButton.Pressed += AddButton_OnPressed;

		AddChild(Events = new ItemList {
			SizeFlagsVertical = SizeFlags.ExpandFill
		});
		Events.ItemSelected += Events_OnItemSelected;

		AddChild(RemoveButton = new Button { Text = "Remove" });
		RemoveButton.Pressed += RemoveButton_OnPressed;

		ReloadEvents();
	}

	public override void _EnterTree() {
		base._EnterTree();
		_events.CollectionChanged += Events_OnCollectionChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		_events.CollectionChanged -= Events_OnCollectionChanged;
	}

	private void AddButton_OnPressed() {
		var element = _events.AppendNew();
		SceneEventSelected?.Invoke(element);
	}

	private void Events_OnItemSelected(long index) {
		var element = _events[(int)index];
		SceneEventSelected?.Invoke(element);
	}

	private void RemoveButton_OnPressed() {
		var selected = Events.GetSelectedItems();
		if (!selected.Any()) return;

		_events.RemoveAt(selected[0]);
		SceneEventSelected?.Invoke(null);
	}

	private void Events_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		ReloadEvents();
	}

	private void ReloadEvents() {
		Events.Clear();

		foreach (var @event in _events) {
			Events.AddItem(@event.Name);
		}
	}

	public override void _Process(double delta) {
		base._Process(delta);

		RemoveButton.Disabled = !Events.IsAnythingSelected();
	}
}