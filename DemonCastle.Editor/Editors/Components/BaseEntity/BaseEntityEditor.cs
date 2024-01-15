using DemonCastle.Editor.Editors.Components.Animations;
using DemonCastle.Editor.Editors.Components.States;
using DemonCastle.Files.BaseEntity;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;
using VariableDeclarationsEditor = DemonCastle.Editor.Editors.Components.VariableDeclarations.VariableDeclarationsEditor;

namespace DemonCastle.Editor.Editors.Components.BaseEntity;

public abstract partial class BaseEntityEditor<TInfo, TFile> : BaseEditor
	where TInfo : BaseEntityInfo<TFile>
	where TFile : BaseEntityFile {

	public override string TabText { get; }
	protected HSplitContainer SplitContainer { get; }
	protected TabContainer RightArea { get; }

	protected BaseEntityEditor(ProjectInfo project, IFileInfo file, TInfo entity, Control detailsPanel) {
		TabText = file.FileName;

		AddChild(SplitContainer = new HSplitContainer());
		SplitContainer.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, margin: 5);

		SplitContainer.AddChild(detailsPanel);
		detailsPanel.CustomMinimumSize = new Vector2(300, 300);
		SplitContainer.AddChild(RightArea = new TabContainer {
			CustomMinimumSize = new Vector2(300, 300)
		});

		RightArea.AddChild(new VariableDeclarationsEditor(project, entity.Variables));
		RightArea.SetTabTitle(0, "Variables");
		RightArea.AddChild(new AnimationsEditor(file, entity.Animations));
		RightArea.SetTabTitle(1, "Animations");
		RightArea.AddChild(new StatesEditor(project, entity));
		RightArea.SetTabTitle(2, "States");
	}
}