using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.States.Events;

public class ActionInfoCollection : IEnumerableInfo<ActionInfo> {
	protected IFileNavigator File { get; }
	protected List<MonsterStateActionData> FileActions { get; }
	protected ObservableCollection<ActionInfo> Actions { get; }

	public ActionInfoCollection(IFileNavigator file, List<MonsterStateActionData> actions) {
		File = file;
		FileActions = actions;
		Actions = new ObservableCollection<ActionInfo>(actions.Select(data => new ActionInfo(file, data)).ToList());
		Actions.CollectionChanged += Actions_OnCollectionChanged;
	}

	public ActionInfo AppendNew() {
		var stateData = new MonsterStateActionData();
		FileActions.Add(stateData);
		Save();
		var actionInfo = new ActionInfo(File, stateData);
		Actions.Add(actionInfo);
		return actionInfo;
	}

	public void Remove(ActionInfo item) {
		var index = Actions.IndexOf(item);
		FileActions.RemoveAt(index);
		Save();
		Actions.RemoveAt(index);
	}

	public void RemoveAt(int actionIndex) {
		FileActions.RemoveAt(actionIndex);
		Save();
		Actions.RemoveAt(actionIndex);
	}

	protected void Save() => File.Save();

	public ActionInfo this[int index] => Actions[index];

	#region IEnumerable
	public IEnumerator<ActionInfo> GetEnumerator() => Actions.GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	#endregion

	#region INotifyCollectionChanged
	public event NotifyCollectionChangedEventHandler? CollectionChanged;

	private void Actions_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		CollectionChanged?.Invoke(this, e);
	}
	#endregion
}