using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.Editor.Editors.Components.Properties;

namespace DemonCastle.Editor.Editors.Project;

public partial class ProjectDetails : PropertyCollection {
	public ProjectDetails(ProjectInfo project) {
		Name = nameof(ProjectDetails);

		AddString("Name", project, p => p.Name);
		AddString("Version", project, p => p.Version);
		AddFile("Start Scene", project, project.Directory, p => p.StartSceneFile, new[] { FileType.Scene });
	}
}