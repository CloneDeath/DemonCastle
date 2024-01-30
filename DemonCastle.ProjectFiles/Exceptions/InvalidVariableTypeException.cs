using System;
using DemonCastle.Files.Variables;

namespace DemonCastle.ProjectFiles.Exceptions;

public class InvalidVariableTypeException : Exception {
	public VariableType? ActualType { get; }
	public VariableType ExpectedType { get; }

	public InvalidVariableTypeException(VariableType? actualType, VariableType expectedType)
		: base($"Invalid variable type! Expected {expectedType}, but got {actualType}.") {
		ActualType = actualType;
		ExpectedType = expectedType;
	}
}