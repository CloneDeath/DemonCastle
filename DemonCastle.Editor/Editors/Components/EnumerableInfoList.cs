using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Components;

public partial class EnumerableInfoList<TInfo> : VBoxContainer
	where TInfo : class, INotifyPropertyChanged, IListableInfo {
	public event Action<TInfo?>? ItemSelected;

	private readonly List<TInfo> _subscribed = new();
	private readonly IEnumerableInfo<TInfo> _data;
	private bool _enabled = true;

	private Button? AddButton { get; set; }
	private ItemList Items { get; }
	private Button RemoveButton { get; }

	public EnumerableInfoList(IEnumerableInfo<TInfo> data) {
		_data = data;
		Name = nameof(EnumerableInfoList<TInfo>);

		AddButton = new Button { Text = "Add" };
		AddButton.Pressed += AddButton_OnPressed;

		Items = new ItemList {
			SizeFlagsVertical = SizeFlags.ExpandFill
		};
		Items.ItemSelected += Items_OnItemSelected;

		RemoveButton = new Button { Text = "Remove" };
		RemoveButton.Pressed += RemoveButton_OnPressed;
	}

	public override void _Ready() {
		base._Ready();

		AppendAddButton();
		AddChild(Items);
		AddChild(RemoveButton);

		ReloadItems();
	}

	protected virtual void AppendAddButton() {
		AddChild(AddButton);
	}

	public virtual bool Enabled {
		get => _enabled;
		set {
			_enabled = value;
			if (AddButton != null) AddButton.Disabled = !value;
			RemoveButton.Disabled = !value;
		}
	}

	private void Items_OnItemSelected(long index) {
		var item = _data[(int)index];
		OnItemSelected(item);
	}

	public override void _EnterTree() {
		base._EnterTree();
		_data.CollectionChanged += Data_OnCollectionChanged;

		ReloadItems();
	}

	public override void _ExitTree() {
		base._ExitTree();
		_data.CollectionChanged -= Data_OnCollectionChanged;

		foreach (var item in _subscribed) {
			item.PropertyChanged -= InfoItem_OnPropertyChanged;
		}
		_subscribed.Clear();
	}

	private void AddButton_OnPressed() {
		var item = _data.AppendNew();
		OnItemSelected(item);
	}

	private void RemoveButton_OnPressed() {
		var selected = Items.GetSelectedItems();
		if (!selected.Any()) return;

		_data.RemoveAt(selected[0]);
		OnItemSelected(null);
	}

	private void Data_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		ReloadItems();
	}

	private void ReloadItems() {
		foreach (var item in _subscribed) {
			item.PropertyChanged -= InfoItem_OnPropertyChanged;
		}
		_subscribed.Clear();
		Items.Clear();

		foreach (var item in _data) {
			_subscribed.Add(item);
			item.PropertyChanged += InfoItem_OnPropertyChanged;
			Items.AddItem(item.ListLabel);
		}
	}

	private void InfoItem_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		if (e.PropertyName != nameof(IListableInfo.ListLabel)) return;

		if (sender is not TInfo item) return;

		var index = _subscribed.IndexOf(item);
		Items.SetItemText(index, item.ListLabel);
	}

	protected void OnItemSelected(TInfo? item) {
		ItemSelected?.Invoke(item);
	}

	public override void _Process(double delta) {
		base._Process(delta);

		RemoveButton.Disabled = !Items.IsAnythingSelected();
	}
}