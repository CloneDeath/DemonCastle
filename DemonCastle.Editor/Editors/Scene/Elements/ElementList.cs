using System;
using System.Collections.Specialized;
using System.Linq;
using DemonCastle.Files;
using DemonCastle.ProjectFiles.Projects.Data.Elements;
using Godot;

namespace DemonCastle.Editor.Editors.Scene.Elements;

public partial class ElementList : VBoxContainer {
	public event Action<IElementInfo?>? ElementSelected;

	private readonly ElementInfoCollection _elements;

	private MenuButton AddButton { get; }
	private ItemList Elements { get; }
	private Button RemoveButton { get; }

	public ElementList(ElementInfoCollection elements) {
		_elements = elements;

		Name = nameof(ElementList);

		AddChild(AddButton = new MenuButton {
			Flat = false,
			Text = "Add..."
		});
		AddButton.GetPopup().IdPressed += AddButton_OnIdPressed;
		foreach (var type in Enum.GetValues<ElementType>()) {
			AddButton.GetPopup().AddItem(Enum.GetName(type), (int)type);
		}

		AddChild(Elements = new ItemList {
			SizeFlagsVertical = SizeFlags.ExpandFill
		});
		Elements.ItemSelected += Elements_OnItemSelected;

		AddChild(RemoveButton = new Button { Text = "Remove" });
		RemoveButton.Pressed += RemoveButton_OnPressed;

		ReloadElements();
	}

	public override void _EnterTree() {
		base._EnterTree();
		_elements.CollectionChanged += Elements_OnCollectionChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		_elements.CollectionChanged -= Elements_OnCollectionChanged;
	}

	private void AddButton_OnIdPressed(long id) {
		var type = (ElementType)id;
		var element = _elements.AppendNew(type);
		ElementSelected?.Invoke(element);
	}

	private void Elements_OnItemSelected(long index) {
		var element = _elements[(int)index];
		ElementSelected?.Invoke(element);
	}

	private void RemoveButton_OnPressed() {
		var selected = Elements.GetSelectedItems();
		if (!selected.Any()) return;

		_elements.RemoveAt(selected[0]);
		ElementSelected?.Invoke(null);
	}

	private void Elements_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		ReloadElements();
	}

	private void ReloadElements() {
		Elements.Clear();

		foreach (var element in _elements) {
			Elements.AddItem(element.Name);
		}
	}

	public override void _Process(double delta) {
		base._Process(delta);

		RemoveButton.Disabled = !Elements.IsAnythingSelected();
	}
}