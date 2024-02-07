using Godot;

namespace DemonCastle.Editor.Editors;

public abstract partial class BaseEditor : Control {
	public abstract Texture2D TabIcon { get; }
}