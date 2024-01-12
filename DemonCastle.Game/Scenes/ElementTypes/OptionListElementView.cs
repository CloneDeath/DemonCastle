using System.Collections.Specialized;
using System.ComponentModel;
using DemonCastle.ProjectFiles.Projects.Data.Elements.Types;
using DemonCastle.ProjectFiles.State;
using Godot;

namespace DemonCastle.Game.Scenes.ElementTypes;

public partial class OptionListElementView : VBoxContainer {
	private readonly OptionListElementInfo _element;
	private readonly IGameState _game;

	public OptionListElementView(OptionListElementInfo element, IGameState game) {
		_element = element;
		_game = game;
		Name = nameof(OptionListElementView);
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
			AddChild(new OptionView(_element, option, _game));
		}
	}
}

public partial class OptionView : Label {
	private readonly OptionListElementInfo _optionList;
	private readonly OptionInfo _option;
	private readonly IGameState _game;

	public OptionView(OptionListElementInfo optionList, OptionInfo option, IGameState game) {
		_optionList = optionList;
		_option = option;
		_game = game;

		Name = nameof(OptionView);

		LabelSettings = new LabelSettings();

		Refresh();
	}

	public override void _EnterTree() {
		base._EnterTree();
		_optionList.PropertyChanged += OptionList_OnPropertyChanged;
		_option.PropertyChanged += Option_OnPropertyChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		_optionList.PropertyChanged -= OptionList_OnPropertyChanged;
		_option.PropertyChanged -= Option_OnPropertyChanged;
	}

	private void OptionList_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		Refresh();
	}

	private void Option_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		Refresh();
	}

	public void Refresh() {
		Text = new TextFinalizer(_game, _optionList.TextTransform).Finalize(_option.Text);

		LabelSettings.Font = _optionList.Font;
		LabelSettings.FontSize = _optionList.FontSize;
		LabelSettings.FontColor = _optionList.Color;
	}
}