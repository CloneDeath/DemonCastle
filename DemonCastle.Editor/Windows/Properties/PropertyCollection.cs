using System;
using System.Linq.Expressions;
using Godot;

namespace DemonCastle.Editor.Windows.Properties {
	public class PropertyCollection : VBoxContainer {
		public PropertyCollection() {
			Name = nameof(PropertyCollection);
		}

		public void AddInteger<T>(string name, T target, Expression<Func<T, int>> propertyExpression) {
			AddChild(new IntegerProperty(new PropertyBinding<T,int>(target, propertyExpression)) {
				PropertyName = name
			});
		}
	}
}