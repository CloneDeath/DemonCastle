using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DemonCastle.ProjectFiles.Converters;

public class EnumTypeConverter : JsonConverter {
	private readonly IEnumTypeMapping[] _mappings;

	public EnumTypeConverter(params IEnumTypeMapping[] mappings) {
		_mappings = mappings;
	}

	public override bool CanConvert(Type objectType) {
		foreach (var mapping in _mappings) {
			if (objectType == mapping.BaseDataType) return true;
		}
		return false;
	}

	public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer) {
		var mapping = _mappings.FirstOrDefault(m => m.BaseDataType == objectType) ?? throw new NullReferenceException();

		var jsonObject = JObject.Load(reader);
		var typeToken = jsonObject[mapping.NameOfEnumField];
		if (typeToken == null) {
			throw new JsonSerializationException($"Missing {mapping.NameOfEnumField} field.");
		}

		var enumValue = (Enum?)typeToken.ToObject(mapping.EnumType) ?? throw new NullReferenceException();
		var targetType = mapping.GetDataType(enumValue);

		return jsonObject.ToObject(targetType, serializer) ?? throw new NullReferenceException();
	}

	public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer) {
		var type = value?.GetType() ?? throw new NullReferenceException();
		var mapping = _mappings.FirstOrDefault(m => m.BaseDataType == type) ?? throw new NullReferenceException();

		var enumValue = mapping.GetEnumValue(type);

		var jsonObject = JObject.FromObject(value, serializer);
		jsonObject.AddFirst(new JProperty(mapping.NameOfEnumField, enumValue));

		jsonObject.WriteTo(writer);
	}
}