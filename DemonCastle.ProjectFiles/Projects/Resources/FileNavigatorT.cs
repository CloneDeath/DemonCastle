using System.IO;
using System.Threading.Tasks;
using DemonCastle.Navigation;

namespace DemonCastle.ProjectFiles.Projects.Resources;

public class FileNavigator<T> : FileNavigator, IFileNavigator {
	public T Resource { get; }
	private Task _saveTask = Task.CompletedTask;

	public FileNavigator(string filePath)
		: base(filePath) {
		var fileContents = File.ReadAllText(filePath);
		Resource = Serializer.Deserialize<T>(fileContents);
	}

	public bool Saving => _saveTask.IsCompleted;

	public void Save() {
		var contents = Serializer.Serialize(Resource);
		_saveTask = _saveTask.ContinueWith(_ => SaveContent(contents));
	}
}