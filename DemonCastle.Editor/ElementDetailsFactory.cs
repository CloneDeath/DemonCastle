using DemonCastle.Editor.Editors.Scene.Elements.Types;
using DemonCastle.Files;
using DemonCastle.ProjectFiles.Exceptions;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Elements;
using DemonCastle.ProjectFiles.Projects.Data.Elements.Types;
using Godot;

namespace DemonCastle.Editor;

public static class ElementDetailsFactory {
	public static Control GetEditor(IFileInfo file, IElementInfo element) {
		return element.Type switch {
			ElementType.ColorRect => new ColorRectElementDetails((ColorRectElementInfo)element),
			ElementType.HealthBar => new HealthBarElementDetails(file, (HealthBarElementInfo)element),
			ElementType.Label => new LabelElementDetails(file, (LabelElementInfo)element),
			ElementType.LevelView => new LevelViewElementDetails((LevelViewElementInfo)element),
			ElementType.Sprite => new SpriteElementDetails(file, (SpriteElementInfo)element),
			ElementType.OptionList => new OptionListElementDetails(file, (OptionListElementInfo)element),
			_ => throw new InvalidEnumValueException<ElementType>(element.Type)
		};
	}
}