using System;
using DemonCastle.Editor.Icons;
using DemonCastle.Navigation;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.FileTreeView;

public partial class ExplorerPanel : VBoxContainer {
	protected HFlowContainer Controls { get; }
	protected Button RefreshButton { get; }
	protected Button CollapseButton { get; }
	protected Button ExpandButton { get; }
	protected FileTree FileTree { get; }

	public event Action<FileNavigator> FileActivated {
		add => FileTree.OnFileActivated += value;
		remove => FileTree.OnFileActivated -= value;
	}

	public event Action TreeReloaded {
		add => FileTree.TreeReloaded += value;
		remove => FileTree.TreeReloaded -= value;
	}

	public ExplorerPanel(ProjectResources resources) {
		Name = nameof(ExplorerPanel);

		AddChild(Controls = new HFlowContainer {
			Alignment = FlowContainer.AlignmentMode.End
		});
		{
			Controls.AddChild(RefreshButton = new Button {
				Icon = IconTextures.RefreshIcon,
				TooltipText = "Refresh"
			});
			RefreshButton.Pressed += RefreshButton_OnPressed;

			Controls.AddChild(CollapseButton = new Button {
				Icon = IconTextures.CollapseIcon,
				TooltipText = "Collapse"
			});
			CollapseButton.Pressed += CollapseButton_OnPressed;

			Controls.AddChild(ExpandButton = new Button {
				Icon = IconTextures.ExpandIcon,
				TooltipText = "Expand"
			});
			ExpandButton.Pressed += ExpandButton_OnPressed;
		}
		AddChild(FileTree = new FileTree(resources) {
			SizeFlagsVertical = SizeFlags.ExpandFill
		});
	}

	private void RefreshButton_OnPressed() {
		FileTree.Refresh();
	}

	private void CollapseButton_OnPressed() {
		FileTree.Collapse();
	}

	private void ExpandButton_OnPressed() {
		FileTree.Expand();
	}
}