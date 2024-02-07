using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects;

public class ProjectWithResources : IListableInfo {
	public ProjectResources Resources { get; }
	public ProjectInfo Project { get; }

	public ProjectWithResources(ProjectResources resources, ProjectInfo project) {
		Resources = resources;
		Project = project;
	}

	public string ListLabel => Project.ListLabel;
}