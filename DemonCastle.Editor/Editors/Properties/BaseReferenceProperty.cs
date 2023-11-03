using System;
using System.Collections.Generic;
using System.Linq;
using DemonCastle.Editor.Properties;
using Godot;

namespace DemonCastle.Editor.Editors.Properties;

public abstract partial class BaseReferenceProperty<T> : BaseProperty {
	protected abstract Texture2D? GetTexture(T option);
	protected abstract Guid GetGuid(T option);
	protected abstract string GetName(T option);

	private List<T> _options = new();

	protected IPropertyBinding<Guid> Binding { get; }
	protected OptionButton OptionButton { get; }

	public event Action<T>? ItemSelected;

	public Guid PropertyValue {
		get => OptionButton.Selected < 0 ? Guid.Empty : GetGuid(_options[OptionButton.Selected]);
		set {
			var option = _options.FirstOrDefault(o => GetGuid(o) == value);
			OptionButton.Selected = option == null ? -1 : _options.IndexOf(option);
		}
	}

	protected BaseReferenceProperty(IPropertyBinding<Guid> binding, IEnumerable<T> options) {
		Name = nameof(BaseReferenceProperty<T>);
		Binding = binding;

		AddChild(OptionButton = new OptionButton {
			CustomMinimumSize = new Vector2(20, 20),
			SizeFlagsHorizontal = SizeFlags.ExpandFill
		});
		LoadOptions(options);
		PropertyValue = binding.Get();
		binding.Changed += Binding_OnChanged;

		OptionButton.ItemSelected += OnItemSelected;
	}

	public void LoadOptions(IEnumerable<T> options) {
		_options = options.ToList();
		OptionButton.Clear();

		for (var i = 0; i < _options.Count; i++) {
			var option = _options[i];
			var texture = GetTexture(option);
			var name = GetName(option);
			if (texture != null) {
				OptionButton.AddIconItem(texture, name, i);
			}
			else {
				OptionButton.AddItem(name, i);
			}
		}
	}

	private void Binding_OnChanged(Guid value) {
		PropertyValue = value;
	}

	private void OnItemSelected(long index) {
		Binding.Set(index < 0 ? Guid.Empty : GetGuid(_options[(int)index]));
		if (index > 0) {
			ItemSelected?.Invoke(_options[(int)index]);
		}
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