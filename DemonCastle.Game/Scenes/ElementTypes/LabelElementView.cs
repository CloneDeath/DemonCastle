using System;
using System.ComponentModel;
using DemonCastle.ProjectFiles.Files.Elements.Types;
using DemonCastle.ProjectFiles.Projects.Data.Elements.Types;
using Godot;

namespace DemonCastle.Game.Scenes.ElementTypes;

public partial class LabelElementView : Label {
	private readonly LabelElementInfo _element;

	public LabelElementView(LabelElementInfo element) {
		_element = element;
		Name = nameof(LabelElementView);
		LabelSettings = new LabelSettings();
		Refresh();
	}

	public override void _EnterTree() {
		base._EnterTree();
		_element.PropertyChanged += Element_OnPropertyChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		_element.PropertyChanged -= Element_OnPropertyChanged;
	}

	private void Element_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		Refresh();
	}

	private void Refresh() {
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