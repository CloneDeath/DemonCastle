using System;
using System.ComponentModel;
using System.Linq;
using DemonCastle.Files.BaseEntity;
using DemonCastle.Files.Common;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
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
	public string Name { get; }
	Guid InitialState { get; }
	public Vector2I Size { get; }
	public AnimationInfoCollection Animations { get; }
	public EntityStateInfoCollection States { get; }
	public VariableDeclarationInfoCollection Variables { get; }
}

public abstract class BaseEntityInfo<TData> : BaseInfo<TData>, IBaseEntityInfo, IEntityStateInfoRetriever
	where TData : BaseEntityFile {

	protected BaseEntityInfo(IFileNavigator file, TData data) : base(file, data) {
		Animations = new AnimationInfoCollection(file, Data.Animations);
		States = new EntityStateInfoCollection(file, this, Data.States);
		Variables = new VariableDeclarationInfoCollection(file, Data.Variables);
	}

	public Guid Id => Data.Id;
	public string ListLabel => Name;

	public string Name {
		get => Data.Name;
		set {
			Data.Name = value;
			Save();
			OnPropertyChanged();
			OnPropertyChanged(nameof(ListLabel));
		}
	}

	public Guid InitialState {
		get => Data.InitialState;
		set => SaveField(ref Data.InitialState, value);
	}

	public Vector2I Size {
		get => Data.Size.ToVector2I();
		set {
			Data.Size = value.ToSize();
			Save();
			OnPropertyChanged();
		}
	}

	public AnimationInfoCollection Animations { get; }
	public EntityStateInfoCollection States { get; }
	public VariableDeclarationInfoCollection Variables { get; }

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