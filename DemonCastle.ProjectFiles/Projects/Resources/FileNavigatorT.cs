using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DemonCastle.ProjectFiles.Projects.Resources;

public class FileNavigator<T> : FileNavigator, ISaveFile {
	public T Resource { get; }
	private Task _saveTask = Task.CompletedTask;

	public FileNavigator(string filePath) : this(filePath, new ProjectResources()) { }
	public FileNavigator(string filePath, ProjectResources resources)
		: base(filePath, resources) {
		var fileContents = File.ReadAllText(filePath);
		Resource = JsonConvert.DeserializeObject<T>(fileContents)
				   ?? throw new NullReferenceException();
	}

	public bool Saving => _saveTask.IsCompleted;

	public void Save() {
		var contents = JsonConvert.SerializeObject(Resource, Formatting.Indented);
		_saveTask = _saveTask.ContinueWith(_ => File.WriteAllTextAsync(FilePath, contents));
	}
}