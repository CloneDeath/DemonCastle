using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace DemonCastle.ProjectFiles.Projects.Data;

public class EnumerableInfoWrapper<T> : IEnumerableInfo<T> {
	public IEnumerable<T> Enumerable { get; }

	public EnumerableInfoWrapper(IEnumerable<T> enumerable) {
		Enumerable = enumerable;
	}

	public IEnumerator<T> GetEnumerator() => Enumerable.GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public event NotifyCollectionChangedEventHandler? CollectionChanged;

	public T this[int index] => Enumerable.ElementAt(index);

	public T AppendNew() => throw new NotSupportedException();

	public void Remove(T item) => throw new NotSupportedException();

	public void RemoveAt(int index) => throw new NotSupportedException();

	public void Move(int oldIndex, int newIndex) => throw new NotSupportedException();

	public int IndexOf(T option) => Enumerable.ToList().IndexOf(option);
}