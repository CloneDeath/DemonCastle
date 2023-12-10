using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.States;

public class StateInfoCollection : IEnumerableInfo<StateInfo> {
	protected IFileNavigator File { get; }
	protected List<MonsterStateData> FileStates { get; }
	protected ObservableCollection<StateInfo> States { get; }

	public StateInfoCollection(IFileNavigator file, List<MonsterStateData> states) {
		File = file;
		FileStates = states;
		States = new ObservableCollection<StateInfo>(states.Select(data => new StateInfo(file, data)).ToList());
		States.CollectionChanged += Animations_OnCollectionChanged;
	}

	public StateInfo AppendNew() {
		var stateData = new MonsterStateData {
			Name = "animation"
		};
		FileStates.Add(stateData);
		Save();
		var stateInfo = new StateInfo(File, stateData);
		States.Add(stateInfo);
		return stateInfo;
	}

	public void Remove(StateInfo item) {
		var index = States.IndexOf(item);
		FileStates.RemoveAt(index);
		Save();
		States.RemoveAt(index);
	}

	public void RemoveAt(int animationIndex) {
		FileStates.RemoveAt(animationIndex);
		Save();
		States.RemoveAt(animationIndex);
	}

	protected void Save() => File.Save();

	public StateInfo this[int index] => States[index];

	#region IEnumerable
	public IEnumerator<StateInfo> GetEnumerator() => States.GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	#endregion

	#region INotifyCollectionChanged
	public event NotifyCollectionChangedEventHandler? CollectionChanged;

	private void Animations_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		CollectionChanged?.Invoke(this, e);
	}
	#endregion
}