using System;
using System.Collections.Generic;
using DemonCastle.Files.Actions;
using DemonCastle.Files.Common;
using Godot;

namespace DemonCastle.Files.Elements;

[ElementType(ElementType.OptionList)]
public class OptionListElementData : ElementData {
	public OptionListElementData() {
		Name = "Option List";
		Type = ElementType.OptionList;
	}

	public string? FontFile { get; set; }
	public int FontSize { get; set; } = 8;
	public ColorData Color { get; set; } = Colors.White.ToColorData();
	public TextTransform TextTransform { get; set; } = TextTransform.None;

	public List<OptionData> Options { get; set; } = new();
}

public class OptionData {
	public Guid Id { get; set; } = Guid.NewGuid();
	public string Text = "Option";
	public List<SceneActionData> OnSelect { get; set; } = new();
}