using System;

namespace DemonCastle.Files.Actions.Values;

public class BooleanValueData {
	public bool? Value;
	public Guid? Variable;

	public void Clear() {
		Value = null;
		Variable = null;
	}
}