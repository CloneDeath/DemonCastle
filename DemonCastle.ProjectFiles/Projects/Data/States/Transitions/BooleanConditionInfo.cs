using System;
using DemonCastle.Files.Actions.Values;
using DemonCastle.Files.Conditions;
using DemonCastle.ProjectFiles.Exceptions;
using DemonCastle.ProjectFiles.Projects.Resources;
using DemonCastle.ProjectFiles.State;

namespace DemonCastle.ProjectFiles.Projects.Data.States.Transitions;

public interface IClearParent {
	void ClearAllExcept(string dataName);
}

public class BooleanConditionInfo : BaseInfo, IClearParent {
	private readonly IClearParent _parent;
	private readonly Func<BooleanConditionData?> _dataGet;
	private readonly Action<BooleanConditionData?> _dataSet;
	private readonly string _dataName;

	private BooleanConditionData? Data {
		get => _dataGet();
		set => _dataSet(value);
	}

	public BooleanConditionInfo(IFileNavigator file,
								Func<BooleanConditionData?> dataGet,
								Action<BooleanConditionData?> dataSet,
								string dataName,
								IClearParent parent) : base(file) {
		_parent = parent;
		_dataGet = dataGet;
		_dataSet = dataSet;
		_dataName = dataName;
	}

	public bool IsSet {
		get => Data != null;
		set {
			if (value) ClearOthers();
			Data = value ? Data ?? new BooleanConditionData() : null;
			Save();
			NotifyAllValuesChanges();
		}
	}

	public BooleanConditionInfo Not => new(File,
		() => Data?.Not,
		v => {
			Data = new BooleanConditionData {
				Not = v
			};
		}, nameof(Data.Not), this);

	public bool? Value {
		get => Data?.Value?.Value;
		set {
			if (value.HasValue) {
				ClearOthers();
				Data = new BooleanConditionData {
					Value = new BooleanValueData {
						Value = value
					}
				};
			} else {
				if (Data is { Value: not null }) {
					Data.Value.Value = null;
				}
			}

			Save();
			NotifyAllValuesChanges();
		}
	}

	public Guid? Variable {
		get => Data?.Value?.Variable;
		set {
			if (value.HasValue) {
				ClearOthers();
				Data = new BooleanConditionData {
					Value = new BooleanValueData {
						Variable = value
					}
				};
			} else {
				if (Data is { Value: not null }) {
					Data.Value.Variable = null;
				}
			}

			Save();
			NotifyAllValuesChanges();
		}
	}

	private void ClearOthers() => _parent.ClearAllExcept(_dataName);

	private void NotifyAllValuesChanges() {
		OnPropertyChanged(nameof(IsSet));
		OnPropertyChanged(nameof(Not));
		OnPropertyChanged(nameof(Value));
		OnPropertyChanged(nameof(Variable));
	}

	public void ClearAllExcept(string dataName) {
		if (dataName != nameof(Data.Not)) Not.IsSet = false;
		if (dataName != nameof(Data.Value)) Value = null;
	}

	public bool IsConditionMet(IGameState game, IEntityState entity) {
		if (Not.IsSet) {
			return !Not.IsConditionMet(game, entity);
		}
		if (Value.HasValue) {
			return Value.Value;
		}
		if (Variable.HasValue) {
			return entity.Variables.TryGetBoolean(Variable.Value)
				   ?? game.Variables.TryGetBoolean(Variable.Value)
				   ?? throw new VariableNotFoundException(Variable.Value);
		}
		throw new IncompleteDataException(File.FilePath);
	}
}