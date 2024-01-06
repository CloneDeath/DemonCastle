using System;
using DemonCastle.Files;

namespace DemonCastle.ProjectFiles.Converters;

[AttributeUsage(AttributeTargets.Class)]
public class ElementTypeAttribute : Attribute {
	public ElementType ElementType { get; }

	public ElementTypeAttribute(ElementType elementType) {
		ElementType = elementType;
	}
}