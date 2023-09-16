using System.Collections.Generic;
using System.Linq;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.ProjectFiles.Projects; 

public partial class InfoItemList<TInfo> : ItemList where TInfo: IListableInfo {
	private List<TInfo> _infoItems;
		
	public void Load(IEnumerable<TInfo> infoItems) {
		Clear();
		_infoItems = infoItems.ToList();
		foreach (var project in _infoItems) {
			AddItem(project.Name);
		}
	}

	public bool IsItemSelected => IsAnythingSelected();
	public bool NoItemSelected => !IsAnythingSelected();
	public TInfo SelectedItem => _infoItems[GetSelectedItems()[0]];
}