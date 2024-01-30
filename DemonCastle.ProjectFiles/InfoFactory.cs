using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DemonCastle.Files;
using DemonCastle.Files.Elements;
using DemonCastle.Files.Variables;
using DemonCastle.Files.Variables.VariableTypes;
using DemonCastle.Files.Variables.VariableTypes.Boolean;
using DemonCastle.ProjectFiles.Converters;
using DemonCastle.ProjectFiles.Exceptions;
using DemonCastle.ProjectFiles.Projects.Data.Elements;
using DemonCastle.ProjectFiles.Projects.Data.Elements.Types;
using DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations;
using DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations.Types;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles;

public static class InfoFactory {
	public static IElementInfo CreateInfo(IFileNavigator file, ElementData data) {
		return data.Type switch {
			ElementType.ColorRect => new ColorRectElementInfo(file, (ColorRectElementData)data),
			ElementType.HealthBar => new HealthBarElementInfo(file, (HealthBarElementData)data),
			ElementType.Label => new LabelElementInfo(file, (LabelElementData)data),
			ElementType.LevelView => new LevelViewElementInfo(file, (LevelViewElementData)data),
			ElementType.Sprite => new SpriteElementInfo(file, (SpriteElementData)data),
			ElementType.OptionList => new OptionListElementInfo(file, (OptionListElementData)data),
			_ => throw new InvalidEnumValueException<ElementType>(data.Type)
		};
	}

	public static VariableDeclarationInfo CreateInfo(IFileNavigator file, VariableDeclarationData data) {
		return data.Type switch {
			VariableType.Boolean => new BooleanVariableDeclarationInfo(file, (BooleanVariableDeclarationData)data),
			VariableType.Integer => new IntegerVariableDeclarationInfo(file, (IntegerVariableDeclarationData)data),
			VariableType.Float => new FloatVariableDeclarationInfo(file, (FloatVariableDeclarationData)data),
			VariableType.String => new StringVariableDeclarationInfo(file, (StringVariableDeclarationData)data),
			VariableType.Monster => new MonsterVariableDeclarationInfo(file, (MonsterVariableDeclarationData)data),
			VariableType.Item => new ItemVariableDeclarationInfo(file, (ItemVariableDeclarationData)data),
			VariableType.Vector2I => new Vector2IVariableDeclarationInfo(file, (Vector2IVariableDeclarationData)data),
			_ => throw new InvalidEnumValueException<VariableType>(data.Type)
		};
	}

	public static ElementData CreateData(ElementType type) {
		var dataType = new ElementTypeMapping().GetDataType(type);
		if (dataType == null) throw new NotSupportedException();
		return (ElementData?)Activator.CreateInstance(dataType) ?? throw new NullReferenceException();
	}

	public static readonly VariableTypeMapping SetVariableMapping = new(typeof(SetVariableActionData), nameof(SetVariableActionData.Type));
	public static SetVariableActionData CreateSetVariableActionData(VariableType type) {
		var dataType = SetVariableMapping.GetDataType(type);
		if (dataType == null) throw new NotSupportedException();
		return (SetVariableActionData?)Activator.CreateInstance(dataType) ?? throw new NullReferenceException();
	}

	public static readonly VariableTypeMapping VariableDeclarationMapping = new(typeof(VariableDeclarationData), nameof(VariableDeclarationData.Type));
	public static VariableDeclarationData CreateData(VariableType type) {
		var dataType = VariableDeclarationMapping.GetDataType(type);
		if (dataType == null) throw new NotSupportedException();
		return (VariableDeclarationData?)Activator.CreateInstance(dataType) ?? throw new NullReferenceException();
	}


	public static IEnumerable<Type> GetTypesWith<TAttribute>() where TAttribute : Attribute {
		var assembly = typeof(TAttribute).Assembly;
		return assembly.GetTypes()
					   .Where(type => type.GetCustomAttribute<TAttribute>() != null);
	}
}