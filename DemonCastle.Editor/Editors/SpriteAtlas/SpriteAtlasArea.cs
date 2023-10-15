using System;
using System.Linq;
using DemonCastle.Editor.Editors.Components;
using DemonCastle.Editor.Editors.SpriteAtlas.Dragging;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using Godot;

namespace DemonCastle.Editor.Editors.SpriteAtlas;

public partial class SpriteAtlasArea : Control {
	private const int EdgeMargin = 3;
	private static readonly Color Color_NotSelected = new(Colors.Gray, 0.5f);
	private static readonly Color Color_Selected = Colors.White;

	private readonly Label SpriteName;
	private readonly Outline Outline;
	private readonly DraggableRegion[] _draggableRegion = new DraggableRegion[5];

	private readonly SpriteAtlasDataInfo _info;

	public bool IsSelected { get; private set; }
	public event Action<SpriteAtlasArea>? Selected;

	public SpriteAtlasArea(SpriteAtlasDataInfo info) {
		_info = info;

		AddChild(Outline = new Outline {
			MouseFilter = MouseFilterEnum.Ignore
		});
		Outline.SetAnchorsPreset(LayoutPreset.FullRect, true);

		// Center
		AddChild(_draggableRegion[0] = new DraggableRegion());
		_draggableRegion[0].SetAnchorsPreset(LayoutPreset.FullRect);
		_draggableRegion[0].Selected += DraggableRegion_OnSelected;

		// Top
		AddChild(_draggableRegion[1] = new DraggableRegion {
			OffsetTop = -EdgeMargin,
			CursorShape = CursorShape.Vsize
		});
		_draggableRegion[1].SetAnchorsPreset(LayoutPreset.TopWide, true);
		_draggableRegion[1].Selected += DraggableRegion_OnSelected;

		// Left
		AddChild(_draggableRegion[2] = new DraggableRegion {
			OffsetLeft = -EdgeMargin,
			CursorShape = CursorShape.Hsize
		});
		_draggableRegion[2].SetAnchorsPreset(LayoutPreset.LeftWide, true);
		_draggableRegion[2].Selected += DraggableRegion_OnSelected;

		// Right
		AddChild(_draggableRegion[3] = new DraggableRegion {
			OffsetRight = EdgeMargin,
			CursorShape = CursorShape.Hsize
		});
		_draggableRegion[3].SetAnchorsPreset(LayoutPreset.RightWide, true);
		_draggableRegion[3].Selected += DraggableRegion_OnSelected;

		// Bottom
		AddChild(_draggableRegion[4] = new DraggableRegion {
			OffsetBottom = EdgeMargin,
			CursorShape = CursorShape.Vsize
		});
		_draggableRegion[4].SetAnchorsPreset(LayoutPreset.BottomWide, true);
		_draggableRegion[4].Selected += DraggableRegion_OnSelected;

		AddChild(SpriteName = new Label {
			Text = info.Name,
			HorizontalAlignment = HorizontalAlignment.Center,
			VerticalAlignment = VerticalAlignment.Top,
			Size = new Vector2(0, 0)
		});
	}

	private void DraggableRegion_OnSelected(DraggableRegion obj) {
		foreach (var draggableRegion in _draggableRegion.Where(d => d != obj)) {
			draggableRegion.IsSelected = true;
		}
		IsSelected = true;
		Selected?.Invoke(this);
	}

	public override void _Process(double delta) {
		base._Process(delta);

		Position = _info.Position;
		Size = _info.Size;
		SpriteName.Text = _info.Name;
		SpriteName.Position = new Vector2(Size.X / 2, Size.Y) - new Vector2(SpriteName.Size.X / 2, 0);
		SpriteName.Modulate = IsSelected ? Color_Selected : Color_NotSelected;
		Outline.Color = IsSelected ? Color_Selected : Color_NotSelected;
	}

	public void Deselect() {
		IsSelected = false;
		foreach (var draggableRegion in _draggableRegion) {
			draggableRegion.IsSelected = false;
		}
	}
}