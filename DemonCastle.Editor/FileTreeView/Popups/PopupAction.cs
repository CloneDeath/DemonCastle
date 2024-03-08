using System;
using Godot;

namespace DemonCastle.Editor.FileTreeView.Popups;

public class PopupAction {
	public string Text { get; set; } = string.Empty;
	public Texture2D? Icon { get; set; }
	public Action Action { get; set; } = () => { };
	public PopupAction[] Children { get; set; } = Array.Empty<PopupAction>();
}