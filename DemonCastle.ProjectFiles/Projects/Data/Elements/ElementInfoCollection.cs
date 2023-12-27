using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.Elements;

public class ElementInfoCollection : IEnumerableInfo<IElementInfo> {
	protected IFileNavigator File { get; }
	protected List<ElementData> FileElements { get; }
	protected ObservableCollection<IElementInfo> Elements { get; }

	public ElementInfoCollection(IFileNavigator file, List<ElementData> elements) {
		File = file;
		FileElements = elements;
		Elements = new ObservableCollection<IElementInfo>(elements.Select(data => ElementInfoFactory.CreateInfo(file, data))
																  .ToList());
		Elements.CollectionChanged += Elements_OnCollectionChanged;
	}

	public IElementInfo this[int index] => Elements[index];

	public IElementInfo AppendNew() => throw new System.NotImplementedException();
	public IElementInfo AppendNew(ElementType type) {
		var elementData = ElementInfoFactory.CreateData(type);
		FileElements.Add(elementData);
		Save();
		var elementInfo = ElementInfoFactory.CreateInfo(File, elementData);
		Elements.Add(elementInfo);
		return elementInfo;
	}

	public void Remove(IElementInfo item) {
		var index = Elements.IndexOf(item);
		FileElements.RemoveAt(index);
		Save();
		Elements.RemoveAt(index);
	}

	public void RemoveAt(int index) {
		FileElements.RemoveAt(index);
		Save();
		Elements.RemoveAt(index);
	}

	protected void Save() => File.Save();


	#region IEnumerable
	public IEnumerator<IElementInfo> GetEnumerator() => Elements.GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	#endregion


	#region INotifyCollectionChanged
	public event NotifyCollectionChangedEventHandler? CollectionChanged;
	private void Elements_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		CollectionChanged?.Invoke(this, e);
	}
	#endregion
}