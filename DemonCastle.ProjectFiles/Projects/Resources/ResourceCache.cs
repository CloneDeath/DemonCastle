using System;
using System.Collections.Generic;
using System.Linq;
using DemonCastle.Navigation;

namespace DemonCastle.ProjectFiles.Projects.Resources;

public class ResourceCache<T> {
	protected Func<FileNavigator, T> ResourceFactory { get; }

	public ResourceCache(Func<FileNavigator, T> resourceFactory) {
		ResourceFactory = resourceFactory;
	}

	protected List<FileResourcePair<T>> Cache { get; } = new();
	public T Get(FileNavigator file) {
		var existing = Cache.FirstOrDefault(c => c.Matches(file));
		if (existing != null) {
			return existing.Resource;
		}

		var resource = ResourceFactory(file);
		Cache.Add(new FileResourcePair<T>(file, resource));
		return resource;
	}
}

public class FileResourcePair<T> {
	public FileNavigator File { get; }
	public T Resource { get; }

	public FileResourcePair(FileNavigator file, T resource) {
		File = file;
		Resource = resource;
	}

	public bool Matches(FileNavigator path) => File.FilePath == path.FilePath;
}