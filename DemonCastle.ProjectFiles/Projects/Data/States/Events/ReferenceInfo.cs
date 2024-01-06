using System;
using DemonCastle.Files.Actions.Values;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.States.Events;

public class ReferenceInfo : BaseInfo<ReferenceData>, IListableInfo {
	public ReferenceInfo(IFileNavigator file, ReferenceData data) : base(file, data) { }

	public string ListLabel => Id != null ? $"Id {Id}"
							   : Variable != null ? $"Variable {Variable}"
							   : "<Empty>";

	public Guid? Id {
		get => Data.Id;
		set => SaveField(ref Data.Id, value);
	}

	public Guid? Variable {
		get => Data.Variable;
		set => SaveField(ref Data.Variable, value);
	}
}