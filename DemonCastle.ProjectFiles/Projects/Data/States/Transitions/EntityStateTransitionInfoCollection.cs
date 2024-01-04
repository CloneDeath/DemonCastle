using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using DemonCastle.ProjectFiles.Files.BaseEntity;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.States.Transitions;

public class EntityStateTransitionInfoCollection : IEnumerableInfo<EntityStateTransitionInfo> {
	protected IFileNavigator File { get; }
	protected List<EntityStateTransitionData> FileTransitions { get; }
	protected ObservableCollection<EntityStateTransitionInfo> Transitions { get; }

	public EntityStateTransitionInfoCollection(IFileNavigator file, List<EntityStateTransitionData> transitions) {
		File = file;
		FileTransitions = transitions;
		Transitions = new ObservableCollection<EntityStateTransitionInfo>(transitions.Select(data => new EntityStateTransitionInfo(file, data)).ToList());
		Transitions.CollectionChanged += Transitions_OnCollectionChanged;
	}

	public EntityStateTransitionInfo AppendNew() {
		var transitionData = new EntityStateTransitionData();
		FileTransitions.Add(transitionData);
		Save();
		var transitionInfo = new EntityStateTransitionInfo(File, transitionData);
		Transitions.Add(transitionInfo);
		return transitionInfo;
	}

	public void Remove(EntityStateTransitionInfo item) {
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

	public EntityStateTransitionInfo this[int index] => Transitions[index];

	#region IEnumerable
	public IEnumerator<EntityStateTransitionInfo> GetEnumerator() => Transitions.GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	#endregion

	#region INotifyCollectionChanged
	public event NotifyCollectionChangedEventHandler? CollectionChanged;

	private void Transitions_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		CollectionChanged?.Invoke(this, e);
	}
	#endregion
}