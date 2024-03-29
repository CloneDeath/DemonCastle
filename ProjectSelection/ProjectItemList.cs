using System;
using System.Collections.Generic;
using System.Linq;
using DemonCastle.ProjectFiles.Projects;
using Godot;

namespace DemonCastle.ProjectSelection;

public partial class ProjectItemList : ItemList {
	private List<ProjectWithResources> _projectItems;

	public ProjectItemList() : this(Array.Empty<ProjectWithResources>()) { }

	public ProjectItemList(IEnumerable<ProjectWithResources> projectItems) {
		Name = nameof(ProjectItemList);

		_projectItems = projectItems.ToList();
		foreach (var project in _projectItems) {
			AddItem(project.ListLabel);
		}
	}

	public void Load(IEnumerable<ProjectWithResources> infoItems) {
		Clear();
		_projectItems = infoItems.ToList();
		foreach (var project in _projectItems) {
			AddItem(project.ListLabel);
		}
	}

	public bool IsItemSelected => IsAnythingSelected();
	public bool NoItemSelected => !IsAnythingSelected();
	public ProjectWithResources SelectedItem => _projectItems[GetSelectedItems()[0]];
}