using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.States;
using DemonCastle.ProjectFiles.Projects.Data.States.Transitions;

namespace DemonCastle.Editor.Editors.Monster.States.Editor.Transitions;

public class TransitionInfoCollectionProxy : IEnumerableInfo<EntityStateTransitionInfo> {
	private StateInfo? _proxy;

	public StateInfo? Proxy {
		get => _proxy;
		set {
			if (_proxy != null) {
				_proxy.Transitions.CollectionChanged -= CollectionChanged;
			}
			_proxy = value;
			if (_proxy != null) {
				_proxy.Transitions.CollectionChanged += CollectionChanged;
			}
			CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}
	}

	#region IEnumerable
	public IEnumerator<EntityStateTransitionInfo> GetEnumerator() => _proxy?.Transitions.GetEnumerator() ?? new List<EntityStateTransitionInfo>().GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	#endregion

	public event NotifyCollectionChangedEventHandler? CollectionChanged;

	public EntityStateTransitionInfo this[int index] => _proxy?.Transitions[index] ?? throw new IndexOutOfRangeException();

	public EntityStateTransitionInfo AppendNew() =>
		_proxy?.Transitions.AppendNew() ?? throw new Exception("Can not add transition to null proxy");

	public void Remove(EntityStateTransitionInfo item) {
		_proxy?.Transitions.Remove(item);
	}

	public void RemoveAt(int index) {
		_proxy?.Transitions.RemoveAt(index);
	}
}