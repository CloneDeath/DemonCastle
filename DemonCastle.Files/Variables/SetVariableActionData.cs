using System;

namespace DemonCastle.Files.Variables;

public class SetVariableActionData {
	public Guid VariableId { get; set; }
	public VariableType Type { get; protected set; }
}