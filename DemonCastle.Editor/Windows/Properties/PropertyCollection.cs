using System;
using System.Linq.Expressions;
using DemonCastle.Editor.Properties;
using Godot;

namespace DemonCastle.Editor.Windows.Properties {
	public partial class PropertyCollection : VBoxContainer {
		public PropertyCollection() {
			Name = nameof(PropertyCollection);
		}

		public void AddFile<T>(string name, T target, string directory,
			Expression<Func<T, string>> propertyExpression) {
			AddChild(new FileProperty(new PropertyBinding<T,string>(target, propertyExpression), directory) {
				PropertyName = name
			});
		}

		public void AddString<T>(string name, T target, Expression<Func<T, string>> propertyExpression) {
			AddChild(new StringProperty(new PropertyBinding<T, string>(target, propertyExpression))
			{
				PropertyName = name
			});
		}

		public void AddInteger<T>(string name, T target, Expression<Func<T, int>> propertyExpression) {
			AddChild(new IntegerProperty(new PropertyBinding<T,int>(target, propertyExpression)) {
				PropertyName = name
			});
		}
		
		public void AddFloat<T>(string name, T target, Expression<Func<T, float>> propertyExpression) {
			AddChild(new FloatProperty(new PropertyBinding<T,float>(target, propertyExpression)) {
				PropertyName = name
			});
		}

		public void AddBoolean<T>(string name, T target, Expression<Func<T, bool>> propertyExpression) {
			AddChild(new BooleanProperty(new PropertyBinding<T,bool>(target, propertyExpression)) {
				PropertyName = name
			});
		}
	}
}