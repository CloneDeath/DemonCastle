using System;
using DemonCastle.Game.Scenes.ElementTypes;
using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.Projects.Data.Elements;
using DemonCastle.ProjectFiles.Projects.Data.Elements.Types;
using DemonCastle.ProjectFiles.State;
using Godot;

namespace DemonCastle.Game;

public static class ElementViewFactory {
	public static Node GetView(IElementInfo element, IGameState gameState) {
		return element.Type switch {
			ElementType.Label => new LabelElementView((LabelElementInfo)element),
			ElementType.ColorRect => new ColorRectElementView((ColorRectElementInfo)element),
			ElementType.Sprite => new SpriteElementView((SpriteElementInfo)element),
			ElementType.LevelView => new LevelViewElementView((LevelViewElementInfo)element, gameState),
			_ => throw new ArgumentOutOfRangeException(nameof(element))
		};
	}
}