using System;
using System.ComponentModel;

namespace DemonCastle.ProjectFiles.Projects.Data.Elements;

public interface IElementInfo : INotifyPropertyChanged {
	Guid Id { get; }
	string Name { get; set; }
}