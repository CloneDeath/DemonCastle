using System;
using Newtonsoft.Json;

namespace DemonCastle.ProjectFiles;

public static class Serializer {
	private static JsonSerializerSettings Settings { get; } = new() {
		TypeNameHandling = TypeNameHandling.Auto,
		Formatting = Formatting.Indented
	};

	public static string Serialize<T>(T data) {
		return JsonConvert.SerializeObject(data, Settings);
	}

	public static T Deserialize<T>(string contents) {
		return JsonConvert.DeserializeObject<T>(contents, Settings)
				   ?? throw new NullReferenceException();
	}
}