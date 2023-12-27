using System;
using System.Collections.Generic;
using DemonCastle.ProjectFiles.Files.Elements;
using Newtonsoft.Json;

namespace DemonCastle.ProjectFiles;

public static class Serializer {
	private static JsonSerializerSettings Settings { get; } = new() {
		TypeNameHandling = TypeNameHandling.Auto,
		Formatting = Formatting.Indented,
		Converters = new List<JsonConverter> { new ElementConverter() }
	};

	public static string Serialize<T>(T data) {
		return JsonConvert.SerializeObject(data, Settings);
	}

	public static T Deserialize<T>(string contents) {
		return JsonConvert.DeserializeObject<T>(contents, Settings)
				   ?? throw new NullReferenceException();
	}
}