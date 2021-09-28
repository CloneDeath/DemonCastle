using System.Collections.Generic;
using System.Linq;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data {
	public class AnimationInfo {
		protected FileNavigator<CharacterFile> File { get; }
		protected AnimationData Animation { get; }

		public AnimationInfo(FileNavigator<CharacterFile> file, AnimationData animation) {
			File = file;
			Animation = animation;
		}

		public string Name {
			get => Animation.Name;
			set { Animation.Name = value; Save(); }
		}

		public IEnumerable<FrameInfo> Frames => Animation.Frames.Select((f, i) => new FrameInfo(File, f, i));

		protected void Save() => File.Save();
	}
}