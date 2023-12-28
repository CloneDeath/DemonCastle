using System;
using System.ComponentModel;
using DemonCastle.ProjectFiles.Files;

namespace DemonCastle.ProjectFiles.Projects.Data.Elements;

public interface IElementInfo : INotifyPropertyChanged {
	Guid Id { get; }
	ElementType Type { get; }
	string Name { get; set; }
}