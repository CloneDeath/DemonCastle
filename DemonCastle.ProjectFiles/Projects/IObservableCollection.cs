using System.Collections.Generic;
using System.Collections.Specialized;

namespace DemonCastle.ProjectFiles.Projects;

public interface IObservableCollection<out T> : INotifyCollectionChanged, IEnumerable<T> {}