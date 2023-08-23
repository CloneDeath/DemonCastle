using System.Collections.Generic;
using System.Linq;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data {
	public partial class AnimationInfo {
		protected FileNavigator<CharacterFile> File { get; }
		protected AnimationData Animation { get; }
		public List<FrameInfo> Frames { get; }

		public AnimationInfo(FileNavigator<CharacterFile> file, AnimationData animation) {
			File = file;
			Animation = animation;
			Frames = Animation.Frames.Select((f, i) => new FrameInfo(this, File, f, i)).ToList();
		}

		public string Name {
			get => Animation.Name;
			set { Animation.Name = value; Save(); }
		}

		protected void Save() => File.Save();

		public void AddFrame() {
			var frame = new FrameData();
			Animation.Frames.Add(frame);
			Frames.Add(new FrameInfo(this, File, frame, Frames.Count));
			Save();
		}

		public void RemoveFrame(FrameInfo frameInfo, FrameData frameData) {
			Animation.Frames.Remove(frameData);
			Frames.Remove(frameInfo);
			Save();
		}
	}
}