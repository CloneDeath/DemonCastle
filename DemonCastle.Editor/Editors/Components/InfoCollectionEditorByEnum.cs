using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Components;

public partial class InfoCollectionEditorByEnum<TInfo, TEnum> : InfoCollectionEditor<TInfo>
	where TInfo : class, IListableInfo, INotifyPropertyChanged
	where TEnum : struct, Enum {

	private readonly IEnumerableInfoByEnum<TInfo, TEnum> _data;
	private readonly IReadOnlyDictionary<TEnum, Texture2D>? _iconMap;
	private readonly Func<TInfo, TEnum>? _getEnum;

	private MenuButton AddButton { get; }

	public InfoCollectionEditorByEnum(IEnumerableInfoByEnum<TInfo, TEnum> data, IReadOnlyDictionary<TEnum, Texture2D>? iconMap = null, Func<TInfo, TEnum>? getEnum = null) : base(data) {
		_data = data;
		_iconMap = iconMap;
		_getEnum = getEnum;

		Name = nameof(InfoCollectionEditorByEnum<TInfo, TEnum>);

		AddButton = new MenuButton {
			Flat = false,
			Text = "Add...",
			SizeFlagsHorizontal = SizeFlags.ExpandFill
		};
		AddButton.GetPopup().IdPressed += AddButton_OnIdPressed;
		var types = Enum.GetValues<TEnum>();
		for (var index = 0; index < types.Length; index++) {
			var type = types[index];
			AddButton.GetPopup().AddItem(Enum.GetName(type), index);
			var icon = iconMap?.GetValueOrDefault(type);
			if (icon != null) AddButton.GetPopup().SetItemIcon(index, icon);
		}
	}

	protected override void AppendAddButton(Control parent) {
		parent.AddChild(AddButton);
	}

	private void AddButton_OnIdPressed(long id) {
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

	protected override void ReloadItems() {
		base.ReloadItems();
		if (_iconMap == null) return;
		if (_getEnum == null) return;
		for (var i = 0; i < _data.Count(); i++) {
			var item = _data[i];
			var value = _getEnum(item);
			var icon = _iconMap.GetValueOrDefault(value);
			if (icon != null) {
				Items.SetItemIcon(i, icon);
			}
		}
	}
}