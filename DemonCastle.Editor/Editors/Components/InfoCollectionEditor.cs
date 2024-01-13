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
	private readonly IEnumerableInfo<TInfo> _data;
	private bool _enabled = true;

	private HBoxContainer TopButtons { get; }
	private Button AddButton { get; }
	private Button MoveUpButton { get; }
	private Button MoveDownButton { get; }
	protected ItemList Items { get; }
	private Button RemoveButton { get; }

	public InfoCollectionEditor(IEnumerableInfo<TInfo> data) {
		_data = data;
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

		Items = new ItemList {
			SizeFlagsVertical = SizeFlags.ExpandFill
		};
		Items.ItemSelected += Items_OnItemSelected;

		RemoveButton = new Button { Text = "Remove" };
		RemoveButton.Pressed += RemoveButton_OnPressed;
	}

	public override void _Ready() {
		base._Ready();

		AddChild(TopButtons);
		AppendAddButton(TopButtons);
		TopButtons.AddChild(MoveUpButton);
		TopButtons.AddChild(MoveDownButton);
		AddChild(Items);
		AddChild(RemoveButton);

		ReloadItems();
	}

	public override void _Process(double delta) {
		base._Process(delta);
		var anythingSelected = SelectedIndex.HasValue;

		MoveUpButton.Disabled = !anythingSelected || SelectedIndex <= 0;
		MoveDownButton.Disabled = !anythingSelected || SelectedIndex >= _data.Count() - 1;
		RemoveButton.Disabled = !anythingSelected;
	}

	protected int? SelectedIndex {
		get {
			var selected = Items.GetSelectedItems();
			return selected.Any() ? selected[0] : null;
		}
		set {
			if (value is null) {
				Items.DeselectAll();
			} else {
				Items.Select(value.Value);
			}
		}
	}

	protected virtual void AppendAddButton(Control parent) {
		parent.AddChild(AddButton);
	}

	private void MoveUp_ButtonOnPressed() {
		var index = SelectedIndex;
		if (index is null or < 1) return;
		_data.Move(index.Value, index.Value - 1);
		SelectedIndex = index - 1;
	}

	private void MoveDownButton_OnPressed() {
		var index = SelectedIndex;
		if (index is null || index >= _data.Count() - 1) return;
		_data.Move(index.Value, index.Value + 1);
		SelectedIndex = index + 1;
	}

	public virtual bool Enabled {
		get => _enabled;
		set {
			_enabled = value;
			AddButton.Disabled = !value;
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
		Items.Select(_data.Count() - 1);
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

	protected virtual void ReloadItems() {
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

	public void ClearSelection() => Items.DeselectAll();
}