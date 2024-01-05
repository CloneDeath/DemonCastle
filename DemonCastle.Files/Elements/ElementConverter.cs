using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DemonCastle.Files.Elements;

public class ElementConverter : JsonConverter {
	public override bool CanConvert(Type objectType) {
		return objectType == typeof(ElementData);
	}

	public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer) {
		var jsonObject = JObject.Load(reader);
		var typeToken = jsonObject["Type"];

		if (typeToken == null)
		{
			throw new JsonSerializationException("Missing Type field.");
		}

		var elementType = typeToken.ToObject<ElementType>();
		var targetType = ElementTypeMapping.GetType(elementType);

		return jsonObject.ToObject(targetType, serializer) ?? throw new NullReferenceException();
	}

	public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer) {
		var type = value?.GetType() ?? throw new NullReferenceException();
		var typeEnum = ElementTypeMapping.GetTypeEnum(type);

		var jsonObject = JObject.FromObject(value, serializer);
		jsonObject.AddFirst(new JProperty("Type", typeEnum));

		jsonObject.WriteTo(writer);
	}
}