using System.IO;
using System.Threading.Tasks;

namespace DemonCastle.ProjectFiles.Projects.Resources;

public class FileNavigator<T> : FileNavigator, IFileNavigator {
	public T Resource { get; }
	private Task _saveTask = Task.CompletedTask;

	public FileNavigator(string filePath) : this(filePath, new ProjectResources()) { }
	public FileNavigator(string filePath, ProjectResources resources)
		: base(filePath, resources) {
		var fileContents = File.ReadAllText(filePath);
		Resource = Serializer.Deserialize<T>(fileContents);
	}

	public bool Saving => _saveTask.IsCompleted;

	public override void Save() {
		var contents = Serializer.Serialize(Resource);
		_saveTask = _saveTask.ContinueWith(_ => File.WriteAllTextAsync(FilePath, contents));
	}
}