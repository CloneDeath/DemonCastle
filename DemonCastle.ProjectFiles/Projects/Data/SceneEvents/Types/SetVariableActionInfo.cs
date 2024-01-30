using System;
using DemonCastle.Files.Actions;
using DemonCastle.Files.Variables;
using DemonCastle.Files.Variables.VariableTypes.Boolean;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.SceneEvents.Types;

public class SetVariableActionInfo : BaseInfo<SceneActionData> {
	public SetVariableActionInfo(IFileNavigator file, SceneActionData data) : base(file, data) { }

	public bool IsSet {
		get => Data.SetGlobalVariable != null;
		set {
			if (IsSet == value) return;
			var data = Data.SetGlobalVariable ?? new SetBooleanVariableActionData();
			if (value) Data.Clear();
			Data.SetGlobalVariable = value ? data : null;
			Save();
			OnPropertyChanged();
		}
	}

	public Guid VariableId {
		get => Data.SetGlobalVariable?.VariableId ?? Guid.Empty;
		set {
			if (Data.SetGlobalVariable?.VariableId == value) return;
			var data = Data.SetGlobalVariable ?? new SetBooleanVariableActionData();
			data.VariableId = value;
			Data.Clear();
			Data.SetGlobalVariable = data;
			Save();
			OnPropertyChanged();
		}
	}

	public VariableType Type {
		get => Data.SetGlobalVariable?.Type ?? VariableType.Boolean;
		set {
			if (Data.SetGlobalVariable?.Type == value) return;
			var variableId = Data.SetGlobalVariable?.VariableId;
			Data.Clear();
			Data.SetGlobalVariable = InfoFactory.CreateSetVariableActionData(value);
			if (variableId.HasValue) Data.SetGlobalVariable.VariableId = variableId.Value;
			Save();
			OnPropertyChanged();
		}
	}
}