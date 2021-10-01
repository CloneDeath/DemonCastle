using System.Collections.Generic;
using System.Linq;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data {
	public class AnimationInfo {
		protected FileNavigator<CharacterFile> File { get; }
		protected AnimationData Animation { get; }
		public List<FrameInfo> Frames { get; }

		public AnimationInfo(FileNavigator<CharacterFile> file, AnimationData animation) {
			File = file;
			Animation = animation;
			Frames = Animation.Frames.Select((f, i) => new FrameInfo(File, f, i)).ToList();
		}

		public string Name {
			get => Animation.Name;
			set { Animation.Name = value; Save(); }
		}

		protected void Save() => File.Save();

		public void AddFrame() {
			var frame = new FrameData();
			Animation.Frames.Add(frame);
			Frames.Add(new FrameInfo(File, frame, Frames.Count));
			Save();
		}
	}
}