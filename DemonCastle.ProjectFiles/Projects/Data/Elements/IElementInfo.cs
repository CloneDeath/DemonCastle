using System;
using System.ComponentModel;
using DemonCastle.Files;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Elements;

public interface IElementInfo : INotifyPropertyChanged {
	Guid Id { get; }
	ElementType Type { get; }
	string Name { get; set; }
	Rect2I Region { get; set; }
}