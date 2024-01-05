using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using DemonCastle.Files.Actions;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.States.Events;

public class EntityActionInfoCollection : IEnumerableInfo<EntityActionInfo> {
	protected IFileNavigator File { get; }
	protected List<EntityActionData> FileActions { get; }
	protected ObservableCollection<EntityActionInfo> Actions { get; }

	public EntityActionInfoCollection(IFileNavigator file, List<EntityActionData> actions) {
		File = file;
		FileActions = actions;
		Actions = new ObservableCollection<EntityActionInfo>(actions.Select(data => new EntityActionInfo(file, data)).ToList());
		Actions.CollectionChanged += Actions_OnCollectionChanged;
	}

	public EntityActionInfo AppendNew() {
		var stateData = new EntityActionData();
		FileActions.Add(stateData);
		Save();
		var actionInfo = new EntityActionInfo(File, stateData);
		Actions.Add(actionInfo);
		return actionInfo;
	}

	public void Remove(EntityActionInfo item) {
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

	public EntityActionInfo this[int index] => Actions[index];

	#region IEnumerable
	public IEnumerator<EntityActionInfo> GetEnumerator() => Actions.GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	#endregion

	#region INotifyCollectionChanged
	public event NotifyCollectionChangedEventHandler? CollectionChanged;

	private void Actions_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		CollectionChanged?.Invoke(this, e);
	}
	#endregion
}