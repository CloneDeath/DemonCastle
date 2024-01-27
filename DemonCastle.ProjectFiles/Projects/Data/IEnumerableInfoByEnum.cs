namespace DemonCastle.ProjectFiles.Projects.Data;

public interface IEnumerableInfoByEnum<TInfo, in TEnum> : IEnumerableInfo<TInfo> {
	public TInfo AppendNew(TEnum type, string name);
}