using System;
using Godot;

namespace DemonCastle.Editor.Windows.Properties {
	public class IntegerProperty : BaseProperty {
		private Action<int> _setValue;
		protected SpinBox SpinBox { get; }

		public int PropertyValue {
			get => (int)SpinBox.Value;
			set => SpinBox.Value = value;
		}
		
		public IntegerProperty(Func<int> getValue, Action<int> setValue) {
			_setValue = setValue;
			Name = nameof(IntegerProperty);
			
			AddChild(SpinBox = new SpinBox {
				RectMinSize = new Vector2(20, 20),
				Editable = false,
				SizeFlagsHorizontal = (int)(SizeFlags.Fill | SizeFlags.Expand),
				Value = getValue()
			});

			SpinBox.Connect("value_changed", this, nameof(OnValueChange));
		}

		protected void OnValueChange(float value) {
			_setValue((int)value);
		}
	}
}