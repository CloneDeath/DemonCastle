using System;

namespace DemonCastle.ProjectFiles.Exceptions;

public class InvalidEnumValueException<TEnum> : Exception {
	public TEnum Value { get; }

	public InvalidEnumValueException(TEnum value)
		: base($"Invalid {typeof(TEnum).Namespace} value encountered: {value}") {
		Value = value;
	}
}