using DemonCastle.Files.Common;
using Godot;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DemonCastle.Files.Elements;

[ElementType(ElementType.Label)]
public class LabelElementData : ElementData {
	public LabelElementData() {
		Name = "Label";
		Type = ElementType.Label;
		Text = "Text";
	}

	public string Text { get; set; }
	public string? FontFile { get; set; }
	public int FontSize { get; set; } = 8;
	public ColorData Color { get; set; } = Colors.White.ToColorData();

	public HorizontalAlignment HorizontalAlignment = HorizontalAlignment.Left;
	public VerticalAlignment VerticalAlignment = VerticalAlignment.Top;

	public TextTransform TextTransform { get; set; } = TextTransform.None;
}

[JsonConverter(typeof(StringEnumConverter))]
public enum HorizontalAlignment {
	Left,
	Center,
	Right
}

[JsonConverter(typeof(StringEnumConverter))]
public enum VerticalAlignment {
	Top,
	Center,
	Bottom
}

[JsonConverter(typeof(StringEnumConverter))]
public enum TextTransform {
	None,
	Uppercase,
	Lowercase
}