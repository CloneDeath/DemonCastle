using DemonCastle.Files.Conditions;

namespace DemonCastle.Files.Variables.VariableTypes.Boolean;

[VariableType(VariableType.Boolean)]
public class SetBooleanVariableActionData : SetVariableActionData {
	public SetBooleanVariableActionData() {
		Type = VariableType.Boolean;
	}

	public BooleanConditionData Value = new();
}