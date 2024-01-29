using System;

namespace DemonCastle.ProjectFiles.Exceptions;

public class VariableNotFoundException : Exception {
	public Guid VariableId { get; }

	public VariableNotFoundException(Guid id) : base($"Variable with ID {id} was not found!") {
		VariableId = id;
	}
}