using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using DemonCastle.Files.Conditions;
using DemonCastle.ProjectFiles.Projects.Data.Elements.Types;
using DemonCastle.ProjectFiles.State;
using Godot;

namespace DemonCastle.Game.Scenes.ElementTypes;

public partial class OptionListElementView : VBoxContainer {
	private readonly OptionListElementInfo _element;
	private readonly IGameState _game;
	private readonly ISceneState _scene;
	private int _selectedOption;
	private readonly List<OptionView> _options = new();

	public OptionListElementView(OptionListElementInfo element, IGameState game, ISceneState scene) {
		_element = element;
		_game = game;
		_scene = scene;
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
		ClampSelectedOption();
		Position = _element.Region.Position;
		Size = _element.Region.Size;

		foreach (var child in GetChildren()) {
			child.QueueFree();
		}
		_options.Clear();

		foreach (var option in _element.Options) {
			var optionView = new OptionView(_element, option, _game);
			_options.Add(optionView);
			AddChild(optionView);
		}
	}

	public override void _Process(double delta) {
		base._Process(delta);
		if (!_scene.IsActive) return;

		if (_game.Input.InputIsInState(PlayerAction.Down, KeyState.Pressed)) {
			_selectedOption++;
		} else if (_game.Input.InputIsInState(PlayerAction.Up, KeyState.Pressed)) {
			_selectedOption--;
		}

		ClampSelectedOption();
		if (!_element.Options.Any()) return;
		if (_game.Input.InputIsInState(PlayerAction.Accept, KeyState.Pressed)) {
			_element.Options[_selectedOption].OnSelect.TriggerActions(_game);
		}

		for (var i = 0; i < _options.Count; i++) {
			_options[i].IsSelected = i == _selectedOption;
		}
	}

	private void ClampSelectedOption() {
		_selectedOption = _element.Options.Any() ? Math.Clamp(_selectedOption, 0, _element.Options.Count() - 1) : 0;
	}
}

public partial class OptionView : Label {
	private readonly OptionListElementInfo _optionList;
	private readonly OptionInfo _option;
	private readonly IGameState _game;
	private bool _isSelected;

	public OptionView(OptionListElementInfo optionList, OptionInfo option, IGameState game) {
		_optionList = optionList;
		_option = option;
		_game = game;

		Name = nameof(OptionView);

		LabelSettings = new LabelSettings();

		Refresh();
	}

	public bool IsSelected {
		get => _isSelected;
		set {
			_isSelected = value;
			Refresh();
		}
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
		var text = $"{(IsSelected ? "> " : "")}{_option.Text}";
		Text = new TextFinalizer(_game, _optionList.TextTransform).Finalize(text);

		LabelSettings.Font = _optionList.Font;
		LabelSettings.FontSize = _optionList.FontSize;
		LabelSettings.FontColor = _optionList.Color;
	}
}