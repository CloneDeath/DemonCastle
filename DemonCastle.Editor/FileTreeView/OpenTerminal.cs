using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace DemonCastle.Editor.FileTreeView;

public static class OpenTerminal {
	// Ported from Godot source
	// https://github.com/Calinou/godot/blob/0e97acff84ca6c859b48b22a6d42c4680ef1c432/editor/filesystem_dock.cpp#L2145
	public static void Open(string fpath) {
		var terminal_emulators = new List<string>();
		if (OS.HasFeature("windows")) {
			terminal_emulators.Add("powershell");
		} else if (OS.HasFeature("macos")) {
			terminal_emulators.Add("/System/Applications/Utilities/Terminal.app");
		} else if (OS.HasFeature("linux")) {
			terminal_emulators.Add("gnome-terminal");
			terminal_emulators.Add("konsole");
			terminal_emulators.Add("xfce4-terminal");
			terminal_emulators.Add("lxterminal");
			terminal_emulators.Add("kitty");
			terminal_emulators.Add("alacritty");
			terminal_emulators.Add("urxvt");
			terminal_emulators.Add("xterm");
		}

		// NOTE: This default value is ignored further below if the terminal executable is `powershell` or `cmd`,
		// due to these terminals requiring nonstandard syntax to start in a specified folder.
		var arguments = "{directory}";

		string chosen_terminal_emulator = "";
		if (OS.HasFeature("linux")) {
			foreach (var terminal_emulator in terminal_emulators) {
				var test_args = new List<string>();
				test_args.Add("-v");
				test_args.Add(terminal_emulator);
				// Silence command name being printed when found. (stderr is already silenced by `OS::execute()` by default.)
				// FIXME: This doesn't appear to silence stdout.
				test_args.Add(">");
				test_args.Add("/dev/null");
				var exitCode = OS.Execute("command", test_args.ToArray());
				if (exitCode == 0) {
					chosen_terminal_emulator = terminal_emulator;
					break;
				}
			}
		} else {
			// On Windows and macOS, the first (and only) terminal emulator in the list is always available.
			chosen_terminal_emulator = terminal_emulators[0];
		}

		List<String> terminal_emulator_args = new List<string>(); // Required for `execute()`, as it doesn't accept `Vector<String>`.
		if (OS.HasFeature("linuxbsd")) {
			if (chosen_terminal_emulator.EndsWith("konsole")) {
				terminal_emulator_args.Add("--workdir");
			}
		}

		bool append_default_args = true;
		if (OS.HasFeature("windows")) {
			// Prepend default arguments based on the terminal emulator name.
			// Use `String.get_basename().to_lower()` to handle Windows' case-insensitive paths
			// with optional file extensions for executables in `PATH`.
			if (chosen_terminal_emulator.GetBaseName().ToLower() == "powershell") {
				terminal_emulator_args.Add("-noexit");
				terminal_emulator_args.Add("-command");
				terminal_emulator_args.Add("cd '{directory}'");
				append_default_args = false;
			} else if (chosen_terminal_emulator.GetBaseName().ToLower() == "cmd") {
				terminal_emulator_args.Add("/K");
				terminal_emulator_args.Add("cd /d {directory}");
				append_default_args = false;
			}
		}

		List<String> arguments_array = arguments.Split(" ").ToList();
		foreach (var argument in arguments_array) {
			if (!append_default_args && argument == "{directory}") {
				// Prevent appending a `{directory}` placeholder twice when using powershell or cmd.
				// This allows users to enter the path to cmd or PowerShell in the custom terminal emulator path,
				// and make it work without having to enter custom arguments.
				continue;
			}
			terminal_emulator_args.Add(argument);
		}

		bool is_directory = fpath.EndsWith("/");
		for (int i = 0; i < terminal_emulator_args.Count; i++) {
			if (is_directory) {
				terminal_emulator_args[i] = terminal_emulator_args[i].Replace("{directory}", ProjectSettings.GlobalizePath(fpath));
			} else {
				terminal_emulator_args[i] = terminal_emulator_args[i].Replace("{directory}", ProjectSettings.GlobalizePath(fpath).GetBaseDir());
			}
		}

		if (OS.IsStdOutVerbose()) {
			// Print full command line to help with troubleshooting.
			String command_string = chosen_terminal_emulator;
			foreach (var arg in terminal_emulator_args) {
				command_string += " " + arg;
			}
			GD.Print("Opening terminal emulator:", command_string);
		}

		int err = OS.CreateProcess(chosen_terminal_emulator, Enumerable.ToArray(terminal_emulator_args), true);
		if (err != 0) {
			String args_string = "";
			for (int i = 0; i < terminal_emulator_args.Count; i++) {
				args_string += terminal_emulator_args[i];
			}
			GD.PrintErr($"Couldn't run external terminal program (error code {err}): {chosen_terminal_emulator} {args_string}");
		}
	}
}