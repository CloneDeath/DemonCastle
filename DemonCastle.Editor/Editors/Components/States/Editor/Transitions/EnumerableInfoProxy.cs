using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using DemonCastle.ProjectFiles.Projects.Data;

namespace DemonCastle.Editor.Editors.Components.States.Editor.Transitions;

public class EnumerableInfoProxy<T> : IEnumerableInfo<T>
	where T : class {
	private IEnumerableInfo<T>? _proxy;

	public IEnumerableInfo<T>? Proxy {
		get => _proxy;
		set {
			if (_proxy != null) {
				_proxy.CollectionChanged -= Proxy_OnCollectionChanged;
			}
			_proxy = value;
			if (_proxy != null) {
				_proxy.CollectionChanged += Proxy_OnCollectionChanged;
			}
			CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}
	}

	private void Proxy_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) => CollectionChanged?.Invoke(sender, e);

	#region IEnumerable
	public IEnumerator<T> GetEnumerator() => _proxy?.GetEnumerator() ?? new List<T>().GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	#endregion

	public event NotifyCollectionChangedEventHandler? CollectionChanged;
	public T this[int index] => _proxy?[index] ?? throw new IndexOutOfRangeException();
	public T AppendNew() => _proxy?.AppendNew() ?? throw new Exception("Can not add transition to null proxy");
	public void Remove(T item) => _proxy?.Remove(item);
	public void RemoveAt(int index) => _proxy?.RemoveAt(index);
	public void Move(int oldIndex, int newIndex) => _proxy?.Move(oldIndex, newIndex);
	public int IndexOf(T option) => _proxy?.IndexOf(option) ?? -1;
}