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
	private readonly DraggableRegion[] _draggableRegion = new DraggableRegion[9];

	private readonly SpriteAtlasDataInfo _info;

	public bool IsSelected { get; private set; }
	public event Action<SpriteAtlasArea>? Selected;

	public SpriteAtlasArea(SpriteAtlasDataInfo info) {
		_info = info;

		AddChild(Outline = new Outline {
			MouseFilter = MouseFilterEnum.Ignore
		});
		Outline.SetAnchorsPreset(LayoutPreset.FullRect, true);

		// TopLeft
		AddChild(_draggableRegion[0] = new DraggableRegion {
			OffsetTop = -EdgeMargin,
			OffsetLeft = -EdgeMargin,
			CursorShape = CursorShape.Fdiagsize
		});
		_draggableRegion[0].SetAnchorsPreset(LayoutPreset.TopLeft, true);
		_draggableRegion[0].Selected += DraggableRegion_OnSelected;
		_draggableRegion[0].DragUpdate += DraggableRegion_TopLeft_OnDragUpdate;

		// Top
		AddChild(_draggableRegion[1] = new DraggableRegion {
			OffsetTop = -EdgeMargin,
			CursorShape = CursorShape.Vsize
		});
		_draggableRegion[1].SetAnchorsPreset(LayoutPreset.TopWide, true);
		_draggableRegion[1].Selected += DraggableRegion_OnSelected;
		_draggableRegion[1].DragUpdate += DraggableRegion_Top_OnDragUpdate;

		// TopRight
		AddChild(_draggableRegion[2] = new DraggableRegion {
			OffsetTop = -EdgeMargin,
			OffsetRight = EdgeMargin,
			CursorShape = CursorShape.Bdiagsize
		});
		_draggableRegion[2].SetAnchorsPreset(LayoutPreset.TopRight, true);
		_draggableRegion[2].Selected += DraggableRegion_OnSelected;
		_draggableRegion[2].DragUpdate += DraggableRegion_TopRight_OnDragUpdate;

		// Left
		AddChild(_draggableRegion[3] = new DraggableRegion {
			OffsetLeft = -EdgeMargin,
			CursorShape = CursorShape.Hsize
		});
		_draggableRegion[3].SetAnchorsPreset(LayoutPreset.LeftWide, true);
		_draggableRegion[3].Selected += DraggableRegion_OnSelected;
		_draggableRegion[3].DragUpdate += DraggableRegion_Left_OnDragUpdate;

		// Center
		AddChild(_draggableRegion[4] = new DraggableRegion());
		_draggableRegion[4].SetAnchorsPreset(LayoutPreset.FullRect);
		_draggableRegion[4].Selected += DraggableRegion_OnSelected;
		_draggableRegion[4].DragUpdate += DraggableRegion_Center_OnDragUpdate;

		// Right
		AddChild(_draggableRegion[5] = new DraggableRegion {
			OffsetRight = EdgeMargin,
			CursorShape = CursorShape.Hsize
		});
		_draggableRegion[5].SetAnchorsPreset(LayoutPreset.RightWide, true);
		_draggableRegion[5].Selected += DraggableRegion_OnSelected;
		_draggableRegion[5].DragUpdate += DraggableRegion_Right_OnDragUpdate;

		// BottomLeft
		AddChild(_draggableRegion[6] = new DraggableRegion {
			OffsetLeft = -EdgeMargin,
			OffsetBottom = EdgeMargin,
			CursorShape = CursorShape.Bdiagsize
		});
		_draggableRegion[6].SetAnchorsPreset(LayoutPreset.BottomLeft, true);
		_draggableRegion[6].Selected += DraggableRegion_OnSelected;
		_draggableRegion[6].DragUpdate += DraggableRegion_BottomLeft_OnDragUpdate;

		// Bottom
		AddChild(_draggableRegion[7] = new DraggableRegion {
			OffsetBottom = EdgeMargin,
			CursorShape = CursorShape.Vsize
		});
		_draggableRegion[7].SetAnchorsPreset(LayoutPreset.BottomWide, true);
		_draggableRegion[7].Selected += DraggableRegion_OnSelected;
		_draggableRegion[7].DragUpdate += DraggableRegion_Bottom_OnDragUpdate;

		// BottomRight
		AddChild(_draggableRegion[8] = new DraggableRegion {
			OffsetRight = EdgeMargin,
			OffsetBottom = EdgeMargin,
			CursorShape = CursorShape.Fdiagsize
		});
		_draggableRegion[8].SetAnchorsPreset(LayoutPreset.BottomRight, true);
		_draggableRegion[8].Selected += DraggableRegion_OnSelected;
		_draggableRegion[8].DragUpdate += DraggableRegion_BottomRight_OnDragUpdate;

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

	private void DraggableRegion_TopLeft_OnDragUpdate(DragData obj) {
		_info.Region = _info.Region.GrowIndividual(-obj.Delta.X, -obj.Delta.Y, 0, 0);
	}

	private void DraggableRegion_Top_OnDragUpdate(DragData obj) {
		_info.Region = _info.Region.GrowIndividual(0, -obj.Delta.Y, 0, 0);
	}

	private void DraggableRegion_TopRight_OnDragUpdate(DragData obj) {
		_info.Region = _info.Region.GrowIndividual(0, -obj.Delta.Y, obj.Delta.X, 0);

	}

	private void DraggableRegion_Left_OnDragUpdate(DragData obj) {
		_info.Region = _info.Region.GrowIndividual(-obj.Delta.X, 0, 0, 0);
	}

	private void DraggableRegion_Center_OnDragUpdate(DragData obj) {
		_info.Position += obj.Delta;
	}

	private void DraggableRegion_Right_OnDragUpdate(DragData obj) {
		_info.Region = _info.Region.GrowIndividual(0, 0, obj.Delta.X, 0);
	}

	private void DraggableRegion_BottomLeft_OnDragUpdate(DragData obj) {
		_info.Region = _info.Region.GrowIndividual(-obj.Delta.X, 0, 0, obj.Delta.Y);
	}

	private void DraggableRegion_Bottom_OnDragUpdate(DragData obj) {
		_info.Region = _info.Region.GrowIndividual(0, 0, 0, obj.Delta.Y);
	}

	private void DraggableRegion_BottomRight_OnDragUpdate(DragData obj) {
		_info.Region = _info.Region.GrowIndividual(0, 0, obj.Delta.X, obj.Delta.Y);
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