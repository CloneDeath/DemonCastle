using System.Collections.Generic;
using System.Collections.Specialized;

namespace DemonCastle.ProjectFiles.Projects.Data;

public interface IEnumerableInfo<T> : IEnumerable<T>, INotifyCollectionChanged {
	public T this[int index] { get; }
	public T AppendNew();
	void Remove(T item);
	void RemoveAt(int index);
	bool CanMoveUp(T item);
	bool CanMoveDown(T item);
	void MoveUp(T item);
	void MoveDown(T item);
	void Move(int oldIndex, int newIndex);
	int IndexOf(T option);
}