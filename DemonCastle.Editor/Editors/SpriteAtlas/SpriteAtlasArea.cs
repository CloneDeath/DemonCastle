using System;
using DemonCastle.Game;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using Godot;

namespace DemonCastle.Editor.Editors.SpriteAtlas;

public partial class SpriteAtlasArea : Components.Outline {
	private readonly SpriteAtlasDataInfo _info;
	private bool _isSelected;

	public event Action<SpriteAtlasArea>? Selected;

	private readonly Label SpriteName;
	private readonly DragData _drag = new();

	private static readonly Color Color_NotSelected = new(Colors.Gray, 0.5f);
	private static readonly Color Color_Selected = Colors.White;

	public bool IsSelected {
		get => _isSelected;
		set {
			_isSelected = value;
			Color = value ? Color_Selected : Color_NotSelected;
		}
	}

	public SpriteAtlasArea(SpriteAtlasDataInfo info) {
		_info = info;
		IsSelected = false;

		AddChild(SpriteName = new Label {
			Text = info.Name,
			HorizontalAlignment = HorizontalAlignment.Center,
			VerticalAlignment = VerticalAlignment.Top,
			Size = new Vector2(0, 0)
		});
	}

	public override void _Process(double delta) {
		base._Process(delta);

		Position = _info.Position;
		Size = _info.Size;
		SpriteName.Text = _info.Name;
		SpriteName.Position = new Vector2(Size.X / 2, Size.Y) - new Vector2(SpriteName.Size.X / 2, 0);
		SpriteName.Modulate = Color;

		if (IsSelected) {
			MouseDefaultCursorShape = CursorShape.Drag;

			if (Input.IsActionJustReleased(InputActions.EditorClick)) {
				_drag.Dragging = false;
			}

			if (!_drag.Dragging) return;
			_drag.MouseCurrent = (Vector2I)(GetGlobalMousePosition() / GetGlobalTransform().Scale);
			Position = _drag.NewPosition;
			_info.Position = _drag.NewPosition;
		}
		else {
			MouseDefaultCursorShape = CursorShape.Arrow;
		}
	}

	public override void _GuiInput(InputEvent @event) {
		base._GuiInput(@event);

		if (!@event.IsActionPressed(InputActions.EditorClick)) return;
		if (IsSelected) {
			_drag.Dragging = true;
			_drag.MouseStart = (Vector2I)(GetGlobalMousePosition() / GetGlobalTransform().Scale);
			_drag.PositionStart = (Vector2I)Position;
		} else {
			IsSelected = true;
			Selected?.Invoke(this);
		}
	}
}