using System;
using System.Collections;
using System.Collections.Generic;
using Godot;

namespace DemonCastle.Editor.Editors.Scene.Events.Editor.Conditions;

public partial class ChoiceTree : HFlowContainer, IEnumerable<ChoiceTreeOption> {
	private readonly List<ChoiceTreeOption> _options = new();
	private readonly OptionButton _choice;
	private readonly HFlowContainer _overflow;

	public ChoiceTree() {
		Name = nameof(ChoiceTree);
		SizeFlagsHorizontal = SizeFlags.ExpandFill;

		AddChild(_choice = new OptionButton());
		_choice.ItemSelected += Choice_OnItemSelected;

		AddChild(_overflow = new HFlowContainer {
			SizeFlagsHorizontal = SizeFlags.ExpandFill
		});
	}

	private void Choice_OnItemSelected(long index) {
		foreach (var child in _overflow.GetChildren()) {
			child.QueueFree();
		}
		if (index < 0 || index >= _options.Count) return;

		var option = _options[(int)index];
		option.OnSelect(_overflow);
	}

	public void Add(string text, bool selected, Action<Control> onSelect) {
		_options.Add(new ChoiceTreeOption(onSelect));
		_choice.AddItem(text);
		if (!selected) return;

		_choice.Selected = _options.Count - 1;
		onSelect(_overflow);
	}

	#region IEnumerable
	public IEnumerator<ChoiceTreeOption> GetEnumerator() => _options.GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	#endregion
}

public record ChoiceTreeOption(Action<Control> OnSelect);