using DemonCastle.Files;
using DemonCastle.Game.Scenes;
using DemonCastle.Game.Scenes.ElementTypes;
using DemonCastle.ProjectFiles.Exceptions;
using DemonCastle.ProjectFiles.Projects.Data.Elements;
using DemonCastle.ProjectFiles.Projects.Data.Elements.Types;
using DemonCastle.ProjectFiles.State;
using Godot;

namespace DemonCastle.Game;

public static class ElementViewFactory {
	public static Node GetView(IElementInfo element, IGameState game, ISceneState scene) {
		return element.Type switch {
			ElementType.ColorRect => new ColorRectElementView((ColorRectElementInfo)element),
			ElementType.HealthBar => new HealthBarElementView((HealthBarElementInfo)element, game),
			ElementType.Label => new LabelElementView((LabelElementInfo)element, game),
			ElementType.LevelView => new LevelViewElementView((LevelViewElementInfo)element, game),
			ElementType.Sprite => new SpriteElementView((SpriteElementInfo)element),
			ElementType.OptionList => new OptionListElementView((OptionListElementInfo)element, game, scene),
			_ => throw new InvalidEnumValueException<ElementType>(element.Type)
		};
	}
}