using System;
using System.Collections.Specialized;
using DemonCastle.Editor.Editors.Components;
using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using Godot;

namespace DemonCastle.Editor.Editors.Weapon.Animations.Editor.Frames;

public partial class WeaponFrameListEditor : VBoxContainer {
	private WeaponAnimationInfo? _current;

	private Button AddFrameButton { get; }
	private HFlowContainer FrameContainer { get; }

	public event Action<WeaponFrameInfo>? FrameSelected;

	public WeaponFrameListEditor() {
		AddChild(FrameContainer = new HFlowContainer {
			SizeFlagsVertical = SizeFlags.ExpandFill
		});

		FrameContainer.AddChild(AddFrameButton = new Button {
			Icon = IconTextures.AddIcon,
			IconAlignment = HorizontalAlignment.Center,
			TooltipText = "Add Frame",
			CustomMinimumSize = new Vector2(60, 60),
			Disabled = true
		}, @internal: InternalMode.Back);
		AddFrameButton.Pressed += AddFrameButton_OnPressed;
	}

	public override void _ExitTree() {
		base._ExitTree();
		if (_current != null) {
			_current.Frames.CollectionChanged -= Frames_OnCollectionChanged;
		}
	}

	public void Load(WeaponAnimationInfo animation) {
		if (_current != null) {
			_current.Frames.CollectionChanged -= Frames_OnCollectionChanged;
		}
		_current = animation;
		if (_current != null) {
			_current.Frames.CollectionChanged += Frames_OnCollectionChanged;
		}

		AddFrameButton.Disabled = _current == null;
		ReloadFrames();
	}

	private void Frames_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		ReloadFrames();
	}

	private void AddFrameButton_OnPressed() {
		_current?.AddFrame();
	}

	private void ReloadFrames() {
		foreach (var child in FrameContainer.GetChildren()) {
			child.QueueFree();
		}

		if (_current == null) return;
		foreach (var frame in _current.Frames) {
			var weaponFrameItem = new WeaponFrameItem(frame);
			weaponFrameItem.Selected += WeaponFrameItem_OnSelected;
			FrameContainer.AddChild(weaponFrameItem);
		}
	}

	private void WeaponFrameItem_OnSelected(SelectableControl obj) {
		if (obj is not WeaponFrameItem wfi) return;
		FrameSelected?.Invoke(wfi.Frame);
	}
}