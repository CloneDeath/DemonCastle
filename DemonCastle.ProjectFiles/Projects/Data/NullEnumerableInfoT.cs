using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace DemonCastle.ProjectFiles.Projects.Data;

public class NullEnumerableInfo<T> : IEnumerableInfo<T> {
	public IEnumerator<T> GetEnumerator() => new List<T>().GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public event NotifyCollectionChangedEventHandler? CollectionChanged;

	public T this[int index] => throw new Exception("Cannot access index of null enumerable.");

	public T AppendNew() => throw new Exception("Cannot append to null enumerable.");

	public void Remove(T item) {
		CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove));
	}

	public void RemoveAt(int index) {}
	public bool CanMoveUp(T item) => false;

	public bool CanMoveDown(T item) => false;

	public void MoveUp(T item) {}

	public void MoveDown(T item) {}

	public void Move(int oldIndex, int newIndex) => throw new Exception("Cannot move items in a null enumerable.");
	public int IndexOf(T option) => -1;
}