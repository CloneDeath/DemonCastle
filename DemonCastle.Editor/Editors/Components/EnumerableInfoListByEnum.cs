using System;
using System.ComponentModel;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Components;

public partial class EnumerableInfoListByEnum<TInfo, TEnum> : EnumerableInfoList<TInfo>
	where TInfo : class, IListableInfo, INotifyPropertyChanged
	where TEnum : struct, Enum {

	private readonly IEnumerableInfoByEnum<TInfo, TEnum> _data;

	private MenuButton AddButton { get; }

	public EnumerableInfoListByEnum(IEnumerableInfoByEnum<TInfo, TEnum> data) : base(data) {
		_data = data;

		Name = nameof(EnumerableInfoListByEnum<TInfo, TEnum>);

		AddButton = new MenuButton {
			Flat = false,
			Text = "Add..."
		};
		AddButton.GetPopup().IdPressed += AddButton_OnIdPressed;
		var types = Enum.GetValues<TEnum>();
		for (var index = 0; index < types.Length; index++) {
			var type = types[index];
			AddButton.GetPopup().AddItem(Enum.GetName(type), index);
		}
	}

	protected override void AppendAddButton() {
		AddChild(AddButton);
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
}