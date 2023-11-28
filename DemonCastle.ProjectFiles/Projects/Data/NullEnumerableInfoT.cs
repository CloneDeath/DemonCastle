using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace DemonCastle.ProjectFiles.Projects.Data;

public class NullEnumerableInfo<T> : IEnumerableInfo<T> {
	public IEnumerator<T> GetEnumerator() => (IEnumerator<T>)Array.Empty<T>().GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public event NotifyCollectionChangedEventHandler? CollectionChanged;

	public T this[int index] => throw new Exception("Cannot access index of null enumerable.");

	public T AppendNew() => throw new Exception("Cannot append to null enumerable.");

	public void Remove(T item) {}

	public void RemoveAt(int index) {}
}