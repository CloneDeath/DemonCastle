using System.Collections.Generic;
using DemonCastle.Editor.Icons;
using DemonCastle.Files;
using Godot;

namespace DemonCastle.Editor.Editors.Scene.Elements;

public static class ElementTypeIcons {
	public static readonly Dictionary<ElementType, Texture2D> Map = new() {
		{ ElementType.ColorRect, IconTextures.ColorRectElementIcon },
		{ ElementType.HealthBar, IconTextures.HealthBarElementIcon },
		{ ElementType.Label, IconTextures.LabelElementIcon },
		{ ElementType.LevelView, IconTextures.LevelViewElementIcon },
		{ ElementType.Sprite, IconTextures.SpriteElementIcon },
		{ ElementType.OptionList, IconTextures.OptionListElementIcon }
	};

	public static Texture2D GetIcon(ElementType type) {
		return Map[type];
	}
}