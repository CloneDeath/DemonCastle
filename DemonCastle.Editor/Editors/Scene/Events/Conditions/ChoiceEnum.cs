using System;

namespace DemonCastle.Editor.Editors.Scene.Events.Conditions;

public partial class ChoiceEnum<TEnum> : ChoiceTree where TEnum : struct, Enum {
	public ChoiceEnum(TEnum? current, Action<TEnum> selected) {
		Name = nameof(ChoiceEnum<TEnum>);

		var values = Enum.GetValues<TEnum>();
		foreach (var value in values) {
			Add(value.ToString(),
				value.Equals(current),
				_ => {
					selected(value);
				}
			);
		}
	}
}