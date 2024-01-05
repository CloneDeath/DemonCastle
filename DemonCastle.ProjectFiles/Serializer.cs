using System;
using System.Collections.Generic;
using DemonCastle.Files.Elements;
using DemonCastle.ProjectFiles.Converters;
using Newtonsoft.Json;

namespace DemonCastle.ProjectFiles;

public static class Serializer {
	private static JsonSerializerSettings GetSettings() => new() {
		Formatting = Formatting.Indented,
		Converters = new List<JsonConverter> { new ElementConverter() }
	};

	public static string Serialize<T>(T data) {
		var settings = GetSettings();
		settings.NullValueHandling = NullValueHandling.Ignore;
		return JsonConvert.SerializeObject(data, settings);
	}

	public static T Deserialize<T>(string contents) {
		return JsonConvert.DeserializeObject<T>(contents, GetSettings())
				   ?? throw new NullReferenceException();
	}
}