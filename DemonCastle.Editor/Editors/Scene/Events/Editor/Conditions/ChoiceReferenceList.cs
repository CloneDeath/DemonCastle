using System;
using System.Collections.Generic;
using DemonCastle.ProjectFiles.Projects.Data;

namespace DemonCastle.Editor.Editors.Scene.Events.Editor.Conditions;

public partial class ChoiceReferenceList<T> : ChoiceTree
	where T : IListableInfo {
	public ChoiceReferenceList(IEnumerable<T> options, Func<T, bool> isCurrent, Action<T> selected) {
		Name = nameof(ChoiceReferenceList<T>);

		foreach (var value in options) {
			Add(value.ListLabel,
				isCurrent(value),
				_ => {
					selected(value);
				}
			);
		}
	}
}