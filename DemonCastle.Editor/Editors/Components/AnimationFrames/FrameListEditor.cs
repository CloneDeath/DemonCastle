using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using Godot;

namespace DemonCastle.Editor.Editors.Components.AnimationFrames;

public partial class FrameListEditor : VBoxContainer {
	private IAnimationInfo? _current;

	private Dictionary<IFrameInfo, FrameItem> _frames = new();

	private Button AddFrameButton { get; }
	private HFlowContainer FrameContainer { get; }

	public event Action<IFrameInfo?>? FrameSelected;

	public FrameListEditor() {
		AddChild(FrameContainer = new HFlowContainer {
			SizeFlagsVertical = SizeFlags.ExpandFill
		});

		FrameContainer.AddChild(AddFrameButton = new Button {
			Icon = IconTextures.AddFrameIcon,
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

	public void Load(IAnimationInfo? animation) {
		if (_current != null) {
			_current.Frames.CollectionChanged -= Frames_OnCollectionChanged;
		}
		_current = animation;
		if (_current != null) {
			_current.Frames.CollectionChanged += Frames_OnCollectionChanged;
		}

		AddFrameButton.Disabled = _current == null;
		ReloadFrames();
		FrameSelected?.Invoke(null);
	}

	private void Frames_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		ReloadFrames();
	}

	private void AddFrameButton_OnPressed() {
		if (_current == null) return;
		var frame = _current.Frames.AppendNew();
		FrameSelected?.Invoke(frame);
		_frames[frame].IsSelected = true;
	}

	private void ReloadFrames() {
		foreach (var child in FrameContainer.GetChildren()) {
			child.QueueFree();
		}
		_frames.Clear();

		if (_current == null) return;
		foreach (var frame in _current.Frames) {
			var frameItem = new FrameItem(frame);
			frameItem.Selected += FrameItem_OnSelected;
			FrameContainer.AddChild(frameItem);
			_frames[frame] = frameItem;
		}
	}

	private void FrameItem_OnSelected(SelectableControl obj) {
		if (obj is not FrameItem wfi) return;
		FrameSelected?.Invoke(wfi.Frame);
	}
}