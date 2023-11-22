using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.Animations;

public class WeaponFrameInfoCollection : IEnumerable<WeaponFrameInfo>, INotifyCollectionChanged {
	private readonly FileNavigator<WeaponFile> _file;
	protected WeaponAnimationInfo Animation { get; }
	protected List<WeaponFrameData> FrameData { get; }
	protected ObservableCollection<WeaponFrameInfo> Frames { get; }

	public WeaponFrameInfoCollection(FileNavigator<WeaponFile> file, WeaponAnimationInfo animation, List<WeaponFrameData> frames) {
		_file = file;
		Animation = animation;
		FrameData = frames;
		Frames = new ObservableCollection<WeaponFrameInfo>(frames.Select((data, i) => new WeaponFrameInfo(animation, file, data, i)).ToList());
		Frames.CollectionChanged += Animations_OnCollectionChanged;
	}

	public WeaponFrameInfo Add(WeaponFrameData frameData) {
		FrameData.Add(frameData);
		Save();
		var frameInfo = new WeaponFrameInfo(Animation, _file, frameData, Frames.Count);
		Frames.Add(frameInfo);
		return frameInfo;
	}

	public void RemoveAt(int frameIndex) {
		FrameData.RemoveAt(frameIndex);
		Save();
		Frames.RemoveAt(frameIndex);
	}

	public void Remove(WeaponFrameInfo frame) {
		FrameData.Remove(frame.FrameData);
		Save();
		Frames.Remove(frame);
	}

	protected void Save() => _file.Save();

	public WeaponFrameInfo this[int index] => Frames[index];

	#region IEnumerable
	public IEnumerator<WeaponFrameInfo> GetEnumerator() => Frames.GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	#endregion

	#region INotifyCollectionChanged
	public event NotifyCollectionChangedEventHandler? CollectionChanged;

	private void Animations_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		CollectionChanged?.Invoke(this, e);
	}
	#endregion
}