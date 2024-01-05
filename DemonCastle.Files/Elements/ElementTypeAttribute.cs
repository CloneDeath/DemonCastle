using System;

namespace DemonCastle.Files.Elements;

[AttributeUsage(AttributeTargets.Class)]
public class ElementTypeAttribute : Attribute {
	public ElementTypeAttribute(ElementType elementType) {
	}
}