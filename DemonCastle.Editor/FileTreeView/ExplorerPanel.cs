using System;
using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.FileTreeView;

public partial class ExplorerPanel : Container {
	protected FlowContainer Controls { get; }
	protected Button RefreshButton { get; }
	protected Button CollapseButton { get; }
	protected Button ExpandButton { get; }
	protected FileTree FileTree { get; }

	public event Action<FileNavigator> FileActivated {
		add => FileTree.OnFileActivated += value;
		remove => FileTree.OnFileActivated -= value;
	}

	public ExplorerPanel(DirectoryNavigator directoryNavigator) {
		Name = nameof(ExplorerPanel);

		AddChild(Controls = new FlowContainer {
			AnchorRight = 1,
			OffsetRight = -5,
			OffsetTop = 2,
			OffsetBottom = 25
		});
		{
			Controls.AddChild(RefreshButton = new Button {
				Icon = IconTextures.RefreshIcon,
				TooltipText = "Refresh"
			});
			RefreshButton.Pressed += RefreshButton_OnPressed;

			Controls.AddChild(CollapseButton = new Button {
				Text = "-",
				TooltipText = "Collapse"
			});
			CollapseButton.Pressed += CollapseButton_OnPressed;

			Controls.AddChild(ExpandButton = new Button {
				Text = "+",
				TooltipText = "Expand"
			});
			ExpandButton.Pressed += ExpandButton_OnPressed;
		}
		AddChild(FileTree = new FileTree(directoryNavigator) {
			AnchorRight = 1,
			AnchorBottom = 1,
			OffsetTop = 35,
			OffsetRight = -5,
			OffsetBottom = -5
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