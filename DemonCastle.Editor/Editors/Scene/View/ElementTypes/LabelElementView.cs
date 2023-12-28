using System;
using DemonCastle.ProjectFiles.Files.Elements.Types;
using DemonCastle.ProjectFiles.Projects.Data.Elements.Types;
using Godot;

namespace DemonCastle.Editor.Editors.Scene.View.ElementTypes;

public partial class LabelElementView : Label {
	private readonly LabelElementInfo _element;

	public LabelElementView(LabelElementInfo element) {
		_element = element;
		Name = nameof(LabelElementView);
		LabelSettings = new LabelSettings();
	}

	public override void _Process(double delta) {
		base._Process(delta);

		Position = _element.Region.Position;
		Size = _element.Region.Size;
		Text = _element.TextTransform switch {
			TextTransform.None => _element.Text,
			TextTransform.Lowercase => _element.Text.ToLower(),
			TextTransform.Uppercase => _element.Text.ToUpper(),
			_ => throw new NotImplementedException()
		};

		LabelSettings.Font = _element.Font;
		LabelSettings.FontSize = _element.FontSize;
		LabelSettings.FontColor = _element.Color;
	}
}