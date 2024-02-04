using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using DemonCastle.Editor.Properties;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Components.Properties.Reference;

public abstract partial class BaseReferenceProperty<T> : BaseProperty where T : INotifyPropertyChanged {
	protected abstract Texture2D? GetTexture(T option);
	protected abstract Guid GetGuid(T option);
	protected abstract string GetName(T option);

	private IEnumerableInfo<T>? _options;
	private List<T> _subscribed = new();

	protected IPropertyBinding<Guid> Binding { get; }
	protected OptionButton OptionButton { get; }

	public event Action<T>? ItemSelected;

	public Guid PropertyValue {
		get => _options == null || OptionButton.Selected < 0 ? Guid.Empty : GetGuid(_options[OptionButton.Selected]);
		set {
			if (_options == null) return;
			var option = _options.FirstOrDefault(o => GetGuid(o) == value);
			OptionButton.Selected = option == null ? -1 : _options.IndexOf(option);
		}
	}

	protected BaseReferenceProperty(IPropertyBinding<Guid> binding, IEnumerableInfo<T> options) {
		Name = nameof(BaseReferenceProperty<T>);
		Binding = binding;

		AddChild(OptionButton = new OptionButton {
			CustomMinimumSize = new Vector2(20, 20),
			SizeFlagsHorizontal = SizeFlags.ExpandFill
		});
		LoadOptions(options);

		OptionButton.ItemSelected += OnItemSelected;
	}

	public override void _EnterTree() {
		base._EnterTree();
		Binding.Changed += Binding_OnChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		Binding.Changed -= Binding_OnChanged;
		if (_options != null) _options.CollectionChanged -= Options_OnCollectionChanged;
		_options = null;
		foreach (var item in _subscribed) {
			item.PropertyChanged -= Item_OnPropertyChanged;
		}
		_subscribed.Clear();
	}

	public void LoadOptions(IEnumerableInfo<T> options) {
		if (_options != null) _options.CollectionChanged -= Options_OnCollectionChanged;
		_options = options;
		if (_options != null) _options.CollectionChanged += Options_OnCollectionChanged;

		ReloadOptions();
	}

	private void Item_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		ReloadOptions();
	}

	private void Options_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		ReloadOptions();
	}

	private void ReloadOptions() {
		foreach (var item in _subscribed) {
			item.PropertyChanged -= Item_OnPropertyChanged;
		}
		_subscribed.Clear();
		OptionButton.Clear();

		if (_options == null) return;

		for (var i = 0; i < _options.Count(); i++) {
			var option = _options[i];
			var texture = GetTexture(option);
			var name = GetName(option);
			if (texture != null) {
				OptionButton.AddIconItem(texture, name, i);
			}
			else {
				OptionButton.AddItem(name, i);
			}
			option.PropertyChanged += Item_OnPropertyChanged;
			_subscribed.Add(option);
		}

		PropertyValue = Binding.Get();
	}

	private void Binding_OnChanged(Guid value) {
		PropertyValue = value;
	}

	private void OnItemSelected(long index) {
		if (_options == null) return;
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