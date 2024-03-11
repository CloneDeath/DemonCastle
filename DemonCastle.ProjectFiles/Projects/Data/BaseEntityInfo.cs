using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using DemonCastle.Files.BaseEntity;
using DemonCastle.Files.Common;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using DemonCastle.ProjectFiles.Projects.Data.Events;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinitions;
using DemonCastle.ProjectFiles.Projects.Data.States;
using DemonCastle.ProjectFiles.Projects.Data.States.Transitions;
using DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data;

public interface IBaseEntityInfo : IListableInfo, INotifyPropertyChanged {
	public Guid Id { get; }
	public string Name { get; set; }
	Guid InitialState { get; set; }
	public Vector2I Size { get; }
	public IAnimationInfoCollection Animations { get; }
	public IEntityStateInfoCollection States { get; }
	public IVariableDeclarationInfoCollection Variables { get; }
	public IEntityEventInfoCollection Events { get; }
}

public abstract class BaseEntityInfo<TData> : BaseInfo<TData>, IBaseEntityInfo, IEntityStateInfoRetriever
	where TData : BaseEntityFile {

	private EntityStateInfo? _currentState;
	private IAnimationInfo? _currentAnimation;
	private IFrameInfo? _currentFrame;

	protected BaseEntityInfo(IFileNavigator file, TData data) : base(file, data) {
		Animations = new AnimationInfoCollection(file, Data.Animations);
		States = new EntityStateInfoCollection(file, this, Data.States);
		Variables = new VariableDeclarationInfoCollection(file, Data.Variables);
		Events = new EntityEventInfoCollection(file, Data.Events);

		LoadStateAndAnimationHooks();
	}

	public Guid Id => Data.Id;
	public string ListLabel => Name;

	public string Name {
		get => Data.Name;
		set {
			if (!SaveField(ref Data.Name, value)) return;
			OnPropertyChanged(nameof(ListLabel));
		}
	}

	public Guid InitialState {
		get => Data.InitialState;
		set {
			if (!SaveField(ref Data.InitialState, value)) return;
			LoadStateAndAnimationHooks();
			OnPropertyChanged(nameof(PreviewSpriteDefinition));
			OnPropertyChanged(nameof(PreviewTexture));
		}
	}

	private void LoadStateAndAnimationHooks() {
		if (_currentState != null) _currentState.PropertyChanged -= State_OnPropertyChanged;
		_currentState = States.Get(InitialState);
		if (_currentState != null) _currentState.PropertyChanged += State_OnPropertyChanged;

		LoadAnimationHooks();
	}

	private void LoadAnimationHooks() {
		if (_currentAnimation != null) _currentAnimation.Frames.CollectionChanged -= AnimationFrames_OnCollectionChanged;
		_currentAnimation = Animations.Get(_currentState?.Animation ?? Guid.Empty);
		if (_currentAnimation != null) _currentAnimation.Frames.CollectionChanged += AnimationFrames_OnCollectionChanged;

		LoadFrameHooks();
	}

	private void LoadFrameHooks() {
		if (_currentFrame != null) _currentFrame.PropertyChanged -= Frame_OnPropertyChanged;
		_currentFrame = _currentAnimation?.Frames.FirstOrDefault();
		if (_currentFrame != null) _currentFrame.PropertyChanged += Frame_OnPropertyChanged;
		OnPropertyChanged(nameof(PreviewSpriteDefinition));
		OnPropertyChanged(nameof(PreviewTexture));
	}

	private void State_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		if (e.PropertyName != nameof(EntityStateInfo.Animation)) return;
		LoadAnimationHooks();
		OnPropertyChanged(nameof(PreviewSpriteDefinition));
		OnPropertyChanged(nameof(PreviewTexture));
	}

	private void AnimationFrames_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		LoadFrameHooks();
		OnPropertyChanged(nameof(PreviewSpriteDefinition));
		OnPropertyChanged(nameof(PreviewTexture));
	}

	private void Frame_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		if (e.PropertyName != nameof(IFrameInfo.SpriteDefinition)) return;
		OnPropertyChanged(nameof(PreviewSpriteDefinition));
		OnPropertyChanged(nameof(PreviewTexture));
	}

	public Vector2I Size {
		get => Data.Size.ToVector2I();
		set => SaveField(ref Data.Size, value.ToSize());
	}

	public IAnimationInfoCollection Animations { get; }
	public IEntityStateInfoCollection States { get; }
	public IVariableDeclarationInfoCollection Variables { get; }
	public IEntityEventInfoCollection Events { get; }

	public ISpriteDefinition PreviewSpriteDefinition => PreviewFrame?.SpriteDefinition ?? new NullSpriteDefinition();

	public Vector2 PreviewOrigin => PreviewFrame?.Origin ?? Vector2.Zero;

	protected IFrameInfo? PreviewFrame {
		get {
			var state = States.FirstOrDefault(s => s.Id == InitialState);
			var animation = Animations.FirstOrDefault(a => a.Id == state?.Animation);
			return animation?.Frames.FirstOrDefault();
		}
	}

	public Texture2D PreviewTexture => PreviewSpriteDefinition.ToTexture();

	public EntityStateInfo? RetrieveEntityStateInfo(Guid stateId) => States.FirstOrDefault(s => s.Id == stateId);
}