using System;
using System.Collections.Specialized;
using System.Linq;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Components;

public partial class EnumerableInfoList<TInfo> : VBoxContainer
	where TInfo : IListableInfo {
	public event Action<TInfo>? ItemSelected;

	private readonly IEnumerableInfo<TInfo> _data;
	private bool _enabled = true;

	private ItemList Items { get; }
	private Button AddButton { get; }
	private Button RemoveButton { get; }

	public EnumerableInfoList(IEnumerableInfo<TInfo> data) {
		_data = data;
		Name = nameof(EnumerableInfoList<TInfo>);

		AddChild(AddButton = new Button { Text = "Add" });
		AddButton.Pressed += AddButton_OnPressed;
		AddChild(Items = new ItemList {
			SizeFlagsVertical = SizeFlags.ExpandFill
		});
		Items.ItemSelected += Items_OnItemSelected;
		AddChild(RemoveButton = new Button { Text = "Remove" });
		RemoveButton.Pressed += RemoveButton_OnPressed;

		ReloadItems();
	}

	public bool Enabled {
		get => _enabled;
		set {
			_enabled = value;
			AddButton.Disabled = !value;
			RemoveButton.Disabled = !value;
		}
	}

	private void Items_OnItemSelected(long index) {
		var animation = _data[(int)index];
		ItemSelected?.Invoke(animation);
	}

	public override void _EnterTree() {
		base._EnterTree();
		_data.CollectionChanged += Data_OnCollectionChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		_data.CollectionChanged -= Data_OnCollectionChanged;
	}

	private void AddButton_OnPressed() {
		_data.AppendNew();
	}

	private void RemoveButton_OnPressed() {
		var selected = Items.GetSelectedItems();
		if (!selected.Any()) return;

		_data.RemoveAt(selected[0]);
	}

	private void Data_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		ReloadItems();
	}

	private void ReloadItems() {
		Items.Clear();

		foreach (var item in _data) {
			Items.AddItem(GetName(item));
		}
	}

	protected virtual string GetName(TInfo item) => item.Name;
}