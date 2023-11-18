using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using DemonCastle.Editor.Properties;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Properties;

public partial class AnimationNameProperty : BaseProperty {
	private readonly IEnumerable<AnimationInfo> _options;

	private List<AnimationInfo> Options => _options.ToList();

	protected IPropertyBinding<string> Binding { get; }
	protected OptionButton OptionButton { get; }

	public string PropertyValue {
		get => OptionButton.Selected < 0 ? string.Empty : Options[OptionButton.Selected].Name;
		set {
			var option = _options.FirstOrDefault(o => o.Name == value);
			OptionButton.Selected = option == null ? -1 : Options.IndexOf(option);
		}
	}

	public AnimationNameProperty(IPropertyBinding<string> binding, IEnumerable<AnimationInfo> options) {
		_options = options;
		Name = nameof(AnimationNameProperty);
		Binding = binding;

		if (options is INotifyCollectionChanged collectionChanged) {
			collectionChanged.CollectionChanged += Options_OnCollectionChanged;
		}

		AddChild(OptionButton = new OptionButton {
			CustomMinimumSize = new Vector2(20, 20),
			SizeFlagsHorizontal = SizeFlags.ExpandFill
		});
		ReloadOptions();
		PropertyValue = binding.Get();

		OptionButton.ItemSelected += OnItemSelected;
	}

	private void ReloadOptions() {
		OptionButton.Clear();
		for (var i = 0; i < Options.Count; i++) {
			var option = Options[i];
			OptionButton.AddItem(option.Name, i);
		}
	}

	private void Options_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		var selected = OptionButton.Selected;
		ReloadOptions();
		OptionButton.Selected = selected;
	}

	public override void _EnterTree() {
		base._EnterTree();
		Binding.Changed += Binding_OnChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		Binding.Changed -= Binding_OnChanged;
	}

	private void Binding_OnChanged(string value) {
		PropertyValue = value;
	}

	private void OnItemSelected(long index) {
		Binding.Set(index < 0 ? string.Empty : Options[(int)index].Name);
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