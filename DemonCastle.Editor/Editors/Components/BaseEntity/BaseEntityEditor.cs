using DemonCastle.Editor.Editors.Components.Animations;
using DemonCastle.Editor.Editors.Components.States;
using DemonCastle.Files.BaseEntity;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Components.BaseEntity;

public abstract partial class BaseEntityEditor<TInfo, TFile> : BaseEditor
	where TInfo : BaseEntityInfo<TFile>
	where TFile : BaseEntityFile {

	public override string TabText { get; }
	protected HSplitContainer SplitContainer { get; }
	protected TabContainer RightArea { get; }

	protected BaseEntityEditor(TInfo entity, Control detailsPanel) {
		TabText = entity.FileName;

		AddChild(SplitContainer = new HSplitContainer());
		SplitContainer.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, margin: 5);

		SplitContainer.AddChild(detailsPanel);
		detailsPanel.CustomMinimumSize = new Vector2(300, 300);
		SplitContainer.AddChild(RightArea = new TabContainer {
			CustomMinimumSize = new Vector2(300, 300)
		});

		//RightArea.AddChild(new VariableEditor());
		//RightArea.SetTabTitle(0, "Variables");
		RightArea.AddChild(new AnimationsEditor(entity, entity.Animations));
		RightArea.SetTabTitle(0, "Animations");
		RightArea.AddChild(new StatesEditor(entity.States, entity.Animations));
		RightArea.SetTabTitle(1, "States");
	}
}