using System;
using System.Collections.Generic;
using System.Linq;
using DemonCastle.Editor.Editors.Components.Properties;
using DemonCastle.Editor.Editors.Scene.Events.Conditions;
using DemonCastle.Editor.Properties;
using DemonCastle.Files.Variables;
using DemonCastle.ProjectFiles.Projects.Data.States.Transitions;
using DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations;

namespace DemonCastle.Editor.Editors.Components.States.Editor.Transitions.Editor;

public partial class BooleanConditionTree : ChoiceTree {
	public BooleanConditionTree(BooleanConditionInfo condition, IEnumerable<VariableDeclarationInfo> variables) {
		Add(
			"Value",
			condition.Value != null,
			i => {
				condition.Value ??= true;
				var binding = new CallbackBinding<bool>(
					() => condition.Value ?? false,
					(b) => condition.Value = b);
				i.AddChild(new BooleanProperty(binding));
			}
		);
		Add(
			"Variable",
			condition.Variable != null,
			i => {
				condition.Variable ??= Guid.Empty;

				i.AddChild(new ChoiceReferenceList<VariableDeclarationInfo>(
					variables.Where(v => v.Type == VariableType.Boolean),
					v => condition.Variable == v.Id,
					v => condition.Variable = v.Id));
			}
		);
		Add(
			"Not",
			condition.Not.IsSet,
			i => {
				condition.Not.IsSet = true;
				i.AddChild(new BooleanConditionTree(condition.Not, variables));
			}
		);
	}
}