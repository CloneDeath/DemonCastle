using System.ComponentModel;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using Godot;

namespace DemonCastle.Editor.Editors.Components.AnimationFrames;

public partial class FrameItem : SelectableControl {
	private static readonly Color SelectedColor = Colors.White;
	private static readonly Color DeselectedColor = new(Colors.White, 0.25f);

	public IFrameInfo Frame { get; }

	private Outline Outline { get; }
	private SpriteDefinitionView SpriteDefinitionView { get; }

	public FrameItem(IFrameInfo frame) {
		Frame = frame;
		Name = nameof(FrameItem);
		CustomMinimumSize = new Vector2(60, 60);

		SelectedCursorShape = CursorShape.Arrow;
		DefaultCursorShape = CursorShape.PointingHand;

		AddChild(Outline = new Outline{ MouseFilter = MouseFilterEnum.Ignore});
		Outline.SetAnchorsPreset(LayoutPreset.FullRect);

		AddChild(SpriteDefinitionView = new SpriteDefinitionView(frame.SpriteDefinition));
		SpriteDefinitionView.SetAnchorsPreset(LayoutPreset.FullRect);
	}

	public override void _EnterTree() {
		base._EnterTree();
		Frame.PropertyChanged += Frame_OnPropertyChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		Frame.PropertyChanged -= Frame_OnPropertyChanged;
	}

	private void Frame_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		SpriteDefinitionView.Load(Frame.SpriteDefinition);
	}

	public override void _Process(double delta) {
		base._Process(delta);
		Outline.Color = IsSelected ? SelectedColor : DeselectedColor;
	}

	protected override void OnSelected() {
		base.OnSelected();
		DeselectSiblings();
	}
}