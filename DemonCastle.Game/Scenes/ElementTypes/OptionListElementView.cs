using System.Collections.Specialized;
using System.ComponentModel;
using DemonCastle.ProjectFiles.Projects.Data.Elements.Types;
using Godot;

namespace DemonCastle.Game.Scenes.ElementTypes;

public partial class OptionListElementView : VBoxContainer {
	private readonly OptionListElementInfo _element;

	public OptionListElementView(OptionListElementInfo element) {
		_element = element;
		Name = nameof(LabelElementView);
		Refresh();
	}

	public override void _EnterTree() {
		base._EnterTree();
		_element.PropertyChanged += Element_OnPropertyChanged;
		_element.Options.CollectionChanged += Options_OnCollectionChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		_element.PropertyChanged -= Element_OnPropertyChanged;
		_element.Options.CollectionChanged -= Options_OnCollectionChanged;
	}

	private void Element_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		Refresh();
	}

	private void Options_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		Refresh();
	}

	private void Refresh() {
		Position = _element.Region.Position;
		Size = _element.Region.Size;

		foreach (var child in GetChildren()) {
			child.QueueFree();
		}

		foreach (var option in _element.Options) {
			var label = new Label {
				Text = option.Text
			};
			AddChild(label);
		}
	}
}