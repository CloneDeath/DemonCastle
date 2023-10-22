using System.Collections.Generic;
using System.Linq;
using DemonCastle.Editor.Properties;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Properties;

public partial class AnimationNameProperty : BaseProperty {
	private readonly List<AnimationInfo> _options;
	protected IPropertyBinding<string> Binding { get; }
	protected OptionButton OptionButton { get; }

	public string PropertyValue {
		get => OptionButton.Selected < 0 ? string.Empty : _options[OptionButton.Selected].Name;
		set {
			var option = _options.FirstOrDefault(o => o.Name == value);
			OptionButton.Selected = option == null ? -1 : _options.IndexOf(option);
		}
	}

	public AnimationNameProperty(IPropertyBinding<string> binding, IEnumerable<AnimationInfo> options) {
		_options = options.ToList();
		Name = nameof(AnimationNameProperty);
		Binding = binding;

		AddChild(OptionButton = new OptionButton {
			CustomMinimumSize = new Vector2(20, 20),
			SizeFlagsHorizontal = SizeFlags.ExpandFill
		});
		for (var i = 0; i < _options.Count; i++) {
			var option = _options[i];
			OptionButton.AddItem(option.Name, i);
		}
		PropertyValue = binding.Get();

		OptionButton.ItemSelected += OnItemSelected;
	}

	private void OnItemSelected(long index) {
		Binding.Set(index < 0 ? string.Empty : _options[(int)index].Name);
	}
}