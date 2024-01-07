using System;
using DemonCastle.Files.Actions.Values;
using DemonCastle.ProjectFiles.Exceptions;
using DemonCastle.ProjectFiles.Projects.Resources;
using DemonCastle.ProjectFiles.State;

namespace DemonCastle.ProjectFiles.Projects.Data.States.Events;

public class ReferenceInfo : BaseInfo<ReferenceData>, IListableInfo {
	public ReferenceInfo(IFileNavigator file, ReferenceData data) : base(file, data) { }

	public string ListLabel => Id != null ? $"Id {Id}"
							   : Variable != null ? $"Variable {Variable}"
							   : "<Empty>";

	public Guid? Id {
		get => Data.Id;
		set {
			Data.Clear();
			SaveField(ref Data.Id, value);
		}
	}

	public Guid? Variable {
		get => Data.Variable;
		set {
			Data.Clear();
			SaveField(ref Data.Variable, value);
		}
	}

	public Guid GetValue(IEntityState entity) {
		if (Id.HasValue) return Id.Value;
		if (Variable.HasValue) {
			return entity.Variables.GetGuid(Variable.Value);
		}
		throw new IncompleteDataException(File.FilePath);
	}
}