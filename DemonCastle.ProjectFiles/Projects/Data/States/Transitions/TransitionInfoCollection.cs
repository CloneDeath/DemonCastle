using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.States.Transitions;

public class TransitionInfoCollection : IEnumerableInfo<TransitionInfo> {
	protected IFileNavigator File { get; }
	protected List<MonsterStateTransitionData> FileTransitions { get; }
	protected ObservableCollection<TransitionInfo> Transitions { get; }

	public TransitionInfoCollection(IFileNavigator file, List<MonsterStateTransitionData> transitions) {
		File = file;
		FileTransitions = transitions;
		Transitions = new ObservableCollection<TransitionInfo>(transitions.Select(data => new TransitionInfo(file, data)).ToList());
		Transitions.CollectionChanged += Transitions_OnCollectionChanged;
	}

	public TransitionInfo AppendNew() {
		var transitionData = new MonsterStateTransitionData();
		FileTransitions.Add(transitionData);
		Save();
		var transitionInfo = new TransitionInfo(File, transitionData);
		Transitions.Add(transitionInfo);
		return transitionInfo;
	}

	public void Remove(TransitionInfo item) {
		var index = Transitions.IndexOf(item);
		FileTransitions.RemoveAt(index);
		Save();
		Transitions.RemoveAt(index);
	}

	public void RemoveAt(int transitionIndex) {
		FileTransitions.RemoveAt(transitionIndex);
		Save();
		Transitions.RemoveAt(transitionIndex);
	}

	protected void Save() => File.Save();

	public TransitionInfo this[int index] => Transitions[index];

	#region IEnumerable
	public IEnumerator<TransitionInfo> GetEnumerator() => Transitions.GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	#endregion

	#region INotifyCollectionChanged
	public event NotifyCollectionChangedEventHandler? CollectionChanged;

	private void Transitions_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		CollectionChanged?.Invoke(this, e);
	}
	#endregion
}