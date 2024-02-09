using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Components;

public partial class InfoCollectionEditor<TInfo> : VBoxContainer
	where TInfo : class, INotifyPropertyChanged, IListableInfo {
	public event Action<TInfo?>? ItemSelected;

	private readonly List<TInfo> _subscribed = new();
	private IEnumerableInfo<TInfo>? _data;
	private bool _enabled = true;

	private HBoxContainer TopButtons { get; }
	private Button AddButton { get; }
	private Button MoveUpButton { get; }
	private Button MoveDownButton { get; }
	protected ItemList ItemList { get; }
	private Button RemoveButton { get; }

	public IEnumerableInfo<TInfo>? Items {
		get => _data;
		set => Load(value);
	}

	public InfoCollectionEditor(IEnumerableInfo<TInfo> data) : this() {
		_data = data;
	}

	public InfoCollectionEditor() {
		Name = nameof(InfoCollectionEditor<TInfo>);

		TopButtons = new HBoxContainer();
		{
			AddButton = new Button {
				Text = "Add",
				SizeFlagsHorizontal = SizeFlags.ExpandFill
			};
			AddButton.Pressed += AddButton_OnPressed;

			MoveUpButton = new Button { Icon = IconTextures.UpIcon };
			MoveUpButton.Pressed += MoveUp_ButtonOnPressed;

			MoveDownButton = new Button { Icon = IconTextures.DownIcon };
			MoveDownButton.Pressed += MoveDownButton_OnPressed;
		}

		ItemList = new ItemList {
			SizeFlagsVertical = SizeFlags.ExpandFill
		};
		ItemList.ItemSelected += ItemListOnItemListSelected;

		RemoveButton = new Button { Text = "Remove" };
		RemoveButton.Pressed += RemoveButton_OnPressed;
	}

	private int LastItemIndex => (_data?.Count() ?? 0) - 1;

	public override void _Ready() {
		base._Ready();

		AddChild(TopButtons);
		AppendAddButton(TopButtons);
		TopButtons.AddChild(MoveUpButton);
		TopButtons.AddChild(MoveDownButton);
		AddChild(ItemList);
		AddChild(RemoveButton);

		ReloadItems();
	}

	public override void _Process(double delta) {
		base._Process(delta);
		var anythingSelected = SelectedIndex.HasValue;

		MoveUpButton.Disabled = !anythingSelected || SelectedIndex <= 0;
		MoveDownButton.Disabled = !anythingSelected || SelectedIndex >= LastItemIndex;
		RemoveButton.Disabled = !anythingSelected;
	}

	public int? SelectedIndex {
		get {
			var selected = ItemList.GetSelectedItems();
			return selected.Any() ? selected[0] : null;
		}
		set {
			if (value is null) {
				ItemList.DeselectAll();
			} else {
				ItemList.Select(value.Value);
			}
		}
	}

	public TInfo? SelectedItem {
		get => SelectedIndex == null ? null : _data?[SelectedIndex.Value];
		set => SelectedIndex = value == null ? null : _data?.IndexOf(value);
	}

	protected virtual void AppendAddButton(Control parent) => parent.AddChild(AddButton);

	public virtual bool Enabled {
		get => _enabled;
		set {
			_enabled = value;
			AddButton.Disabled = !value;
			RemoveButton.Disabled = !value;
		}
	}

	private void ItemListOnItemListSelected(long index) {
		if (_data == null) return;

		var item = _data[(int)index];
		OnItemSelected(item);
	}

	public override void _EnterTree() {
		base._EnterTree();
		if (_data != null) _data.CollectionChanged += Data_OnCollectionChanged;
		ReloadItems();
	}

	public override void _ExitTree() {
		base._ExitTree();
		if (_data != null) _data.CollectionChanged -= Data_OnCollectionChanged;

		foreach (var item in _subscribed) {
			item.PropertyChanged -= InfoItem_OnPropertyChanged;
		}
		_subscribed.Clear();
	}

	private void AddButton_OnPressed() {
		if (_data == null) return;

		var item = _data.AppendNew();
		ItemList.Select(LastItemIndex);
		OnItemSelected(item);
	}

	private void RemoveButton_OnPressed() {
		if (_data == null) return;

		var selected = ItemList.GetSelectedItems();
		if (!selected.Any()) return;

		_data.RemoveAt(selected[0]);
		OnItemSelected(null);
	}

	private void MoveUp_ButtonOnPressed() {
		if (_data == null) return;

		var index = SelectedIndex;
		if (index is null or < 1) return;
		_data.Move(index.Value, index.Value - 1);
		SelectedIndex = index - 1;
	}

	private void MoveDownButton_OnPressed() {
		if (_data == null) return;

		var index = SelectedIndex;
		if (index is null || index >= LastItemIndex) return;
		_data.Move(index.Value, index.Value + 1);
		SelectedIndex = index + 1;
	}

	private void Data_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		ReloadItems();
	}

	public virtual void Load(IEnumerableInfo<TInfo>? data) {
		if (_data != null) _data.CollectionChanged -= Data_OnCollectionChanged;
		_data = data;
		if (_data != null) _data.CollectionChanged += Data_OnCollectionChanged;
		ReloadItems();
	}

	protected virtual void ReloadItems() {
		foreach (var item in _subscribed) {
			item.PropertyChanged -= InfoItem_OnPropertyChanged;
		}
		_subscribed.Clear();
		ItemList.Clear();
		if (_data == null) return;

		foreach (var item in _data) {
			_subscribed.Add(item);
			item.PropertyChanged += InfoItem_OnPropertyChanged;
			AddItemListItem(item);
		}
	}

	protected virtual int AddItemListItem(TInfo item) {
		return ItemList.AddItem(item.ListLabel);
	}

	private void InfoItem_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		if (e.PropertyName != nameof(IListableInfo.ListLabel)) return;

		if (sender is not TInfo item) return;

		var index = _subscribed.IndexOf(item);
		ItemList.SetItemText(index, item.ListLabel);
	}

	protected void OnItemSelected(TInfo? item) => ItemSelected?.Invoke(item);
	public void ClearSelection() => ItemList.DeselectAll();
}