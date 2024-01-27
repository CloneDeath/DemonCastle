using System;
using System.Collections.Generic;
using System.ComponentModel;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Components;

public partial class InfoCollectionEditorByEnum<TInfo, TEnum> : InfoCollectionEditor<TInfo>
	where TInfo : class, IListableInfo, INotifyPropertyChanged
	where TEnum : struct, Enum {

	private IEnumerableInfoByEnum<TInfo, TEnum>? _data;
	private readonly IReadOnlyDictionary<TEnum, Texture2D>? _iconMap;
	private readonly Func<TInfo, TEnum>? _getEnum;

	private MenuButton AddButton { get; }

	public InfoCollectionEditorByEnum(IEnumerableInfoByEnum<TInfo, TEnum> data,
									  IReadOnlyDictionary<TEnum, Texture2D>? iconMap = null,
									  Func<TInfo, TEnum>? getEnum = null) : base(data) {
		_data = data;
		_iconMap = iconMap;
		_getEnum = getEnum;

		Name = nameof(InfoCollectionEditorByEnum<TInfo, TEnum>);

		AddButton = CreateAddButton();
	}

	public InfoCollectionEditorByEnum(IReadOnlyDictionary<TEnum, Texture2D>? iconMap = null, Func<TInfo, TEnum>? getEnum = null) {
		_iconMap = iconMap;
		_getEnum = getEnum;

		Name = nameof(InfoCollectionEditorByEnum<TInfo, TEnum>);

		AddButton = CreateAddButton();
	}

	private MenuButton CreateAddButton() {
		var addButton = new MenuButton {
			Flat = false,
			Text = "Add...",
			SizeFlagsHorizontal = SizeFlags.ExpandFill
		};
		addButton.GetPopup().IdPressed += AddButton_OnIdPressed;
		var types = Enum.GetValues<TEnum>();
		for (var index = 0; index < types.Length; index++) {
			var type = types[index];
			addButton.GetPopup().AddItem(Enum.GetName(type), index);
			var icon = _iconMap?.GetValueOrDefault(type);
			if (icon != null) addButton.GetPopup().SetItemIcon(index, icon);
		}
		return addButton;
	}

	protected override void AppendAddButton(Control parent) {
		parent.AddChild(AddButton);
	}

	private void AddButton_OnIdPressed(long id) {
		if (_data == null) return;

		var values = Enum.GetValues<TEnum>();
		var type = values[(int)id];
		var element = _data.AppendNew(type);
		OnItemSelected(element);
	}

	public override bool Enabled {
		get => base.Enabled;
		set {
			base.Enabled = value;
			AddButton.Disabled = !value;
		}
	}

	protected override int AddItemListItem(TInfo item) {
		if (_getEnum == null || _iconMap == null) return ItemList.AddItem(item.ListLabel);

		var value = _getEnum(item);
		var icon = _iconMap.GetValueOrDefault(value);
		return ItemList.AddItem(item.ListLabel, icon);
	}

	public void Load(IEnumerableInfoByEnum<TInfo, TEnum>? data) {
		base.Load(data);
		_data = data;
	}
}