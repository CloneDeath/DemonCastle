using System;
using System.Collections.Generic;

namespace DemonCastle.ProjectFiles.Projects.Resources {
	public partial class ResourceCache<T> {
		protected Func<string, T> ResourceFactory { get; }

		public ResourceCache(Func<string, T> resourceFactory) {
			ResourceFactory = resourceFactory;
		}
		
		protected Dictionary<string, T> Cache { get; } = new Dictionary<string, T>();
		public T Get(string path) {
			if (!Cache.ContainsKey(path)) {
				Cache[path] = ResourceFactory(path);
			}
			return Cache[path];
		}
	}
}