using System;
using DemonCastle.Files.Actions.Values;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.States.Events;

public class Vector2IValueInfo : BaseInfo<Vector2IValueData>, IListableInfo {
	public Vector2IValueInfo(IFileNavigator file, Vector2IValueData data) : base(file, data) { }

	public string ListLabel => Value != null ? $"Value: {Value}"
							   : Variable != null ? $"Variable: {Variable}"
							   : "<Empty>";

	public Vector2I? Value {
		get => Data.Value;
		set {
			Data.Clear();
			SaveField(ref Data.Value, value);
		}
	}

	public Guid? Variable {
		get => Data.Variable;
		set {
			Data.Clear();
			SaveField(ref Data.Variable, value);
		}
	}
}