using System;
using System.Collections.Generic;
using System.Linq;
using DemonCastle.Editor.Editors.Components.Properties.Reference;
using DemonCastle.Editor.Properties;
using Godot;

namespace DemonCastle.Editor.Editors.Components.Properties;

public partial class EnumProperty<TEnum> : BaseProperty where TEnum : struct, Enum {
	private List<TEnum> _options = new();

	protected IPropertyBinding<TEnum> Binding { get; }
	protected OptionButton OptionButton { get; }

	public TEnum PropertyValue {
		get => OptionButton.Selected < 0 ? _options[0] : _options[OptionButton.Selected];
		set => OptionButton.Selected = _options.IndexOf(value);
	}

	public EnumProperty(IPropertyBinding<TEnum> binding) {
		Name = nameof(MonsterReferenceProperty);
		Binding = binding;

		AddChild(OptionButton = new OptionButton {
			CustomMinimumSize = new Vector2(20, 20),
			SizeFlagsHorizontal = SizeFlags.ExpandFill
		});
		LoadOptions(Enum.GetValues<TEnum>());
		PropertyValue = binding.Get();

		OptionButton.ItemSelected += OnItemSelected;
	}

	public override void _EnterTree() {
		base._EnterTree();
		Binding.Changed += Binding_OnChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		Binding.Changed -= Binding_OnChanged;
	}

	public void LoadOptions(IEnumerable<TEnum> options) {
		_options = options.ToList();
		OptionButton.Clear();

		for (var i = 0; i < _options.Count; i++) {
			var option = _options[i];
			var name = option.ToString();
			OptionButton.AddItem(name, i);
		}
	}

	private void Binding_OnChanged(TEnum value) {
		PropertyValue = value;
	}

	private void OnItemSelected(long index) {
		Binding.Set(index < 0 ? _options[0] : _options[(int)index]);
	}

	public override void Enable() {
		base.Enable();
		OptionButton.Disabled = false;
	}

	public override void Disable() {
		base.Disable();
		OptionButton.Disabled = true;
	}
}