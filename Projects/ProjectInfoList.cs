using System.Collections.Generic;
using System.Linq;
using DemonCastle.Projects.Data;
using Godot;

namespace DemonCastle.Projects {
	public partial class ProjectInfoList : ItemList {
		private List<ProjectInfo> _projects;
		
		public void Load(IEnumerable<ProjectInfo> projects) {
			Clear();
			_projects = projects.ToList();
			foreach (var project in _projects) {
				AddItem(project.Name);
			}
		}

		public bool IsProjectSelected => IsAnythingSelected();
		public ProjectInfo SelectedProject => _projects[GetSelectedItems()[0]];
	}
}