using System.Collections.Generic;
using System.Collections.Specialized;

namespace DemonCastle.ProjectFiles.Projects.Data;

public interface IEnumerableInfo<T> : IEnumerable<T>, INotifyCollectionChanged {
	public T this[int index] { get; }
	public T AppendNew();
	void Remove(T item);
	void RemoveAt(int index);
	void Move(int oldIndex, int newIndex);
}