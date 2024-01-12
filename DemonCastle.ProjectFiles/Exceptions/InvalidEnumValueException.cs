using System;

namespace DemonCastle.ProjectFiles.Exceptions;

public class InvalidEnumValueException<TEnum> : Exception {
	public TEnum Value { get; }

	public InvalidEnumValueException(TEnum value)
		: base($"Invalid {typeof(TEnum).Name} value encountered: {value}") {
		Value = value;
	}
}