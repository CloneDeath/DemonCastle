using System.Collections.Generic;
using Godot;

namespace DemonCastle;

public class CliArguments {
	private Dictionary<string, string> Arguments { get; } = new();
	public string? ProjectPath => Get("game");

	public CliArguments() {
		var args = OS.GetCmdlineUserArgs();
		foreach (var arg in args) {
			var split = arg.Split('=');
			if (split.Length == 1) {
				Arguments[split[0][2..]] = "";
			}
			else {
				Arguments[split[0][2..]] = split[1];
			}
		}
	}

	private string? Get(string key) => Arguments.GetValueOrDefault(key);
}