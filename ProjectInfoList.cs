using System.Collections.Generic;
using DemonCastle.Projects;
using Godot;

namespace DemonCastle {
	public partial class ProjectInfoList : ItemList {
		public void Load(IEnumerable<ProjectInfo> projects) {
			Items.Clear();
			foreach (var project in projects) {
				AddItem(project.Name);
			}
		}
	}
}