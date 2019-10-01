using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Sara.LogReader.Model.Property
{
    [TypeConverter(typeof(DynamicObjectConverter))]
    public class DynamicObjectType
    {
        private List<DynamicProperty> _props = new List<DynamicProperty>();
        [Browsable(false)]
        public List<DynamicProperty> Properties { get { return _props; } set { _props = value; }}

        private readonly Dictionary<string, object> _values = new Dictionary<string, object>();

        public object this[string name]
        {
            get { object val; _values.TryGetValue(name, out val); return val; }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                _values.Remove(name);
            }
        }

        private class DynamicObjectConverter : ExpandableObjectConverter
        {
            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
            {
                var stdProps = base.GetProperties(context, value, attributes);
                var obj = value as DynamicObjectType;
                var customProps = obj == null ? null : obj.Properties;
                var props = new PropertyDescriptor[stdProps.Count + (customProps == null ? 0 : customProps.Count)];
                stdProps.CopyTo(props, 0);
                if (customProps != null)
                {
                    int index = stdProps.Count;
                    foreach (var prop in customProps)
                    {
                        props[index++] = new CustomPropertyDescriptor(prop);
                    }
                }
                return new PropertyDescriptorCollection(props);
            }
        }
        private class CustomPropertyDescriptor : PropertyDescriptor
        {
            private readonly DynamicProperty _prop;
            public CustomPropertyDescriptor(DynamicProperty prop)
                : base(prop.Name, null)
            {
                _prop = prop;
            }
            public override string Category { get { return _prop.Category ?? "Dynamic"; } }
            public override string Description { get { return _prop.Desc; } }
            public override string Name { get { return _prop.Name; } }
            public override bool ShouldSerializeValue(object component) { return ((DynamicObjectType)component)[_prop.Name] != null; }
            public override void ResetValue(object component) { ((DynamicObjectType)component)[_prop.Name] = null; }
            public override bool IsReadOnly { get { return false; } }
            public override Type PropertyType { get { return _prop.Type ?? typeof(string); } }
            public override bool CanResetValue(object component) { return true; }
            public override Type ComponentType { get { return typeof(DynamicObjectType); } }
            public override void SetValue(object component, object value) { ((DynamicObjectType)component)[_prop.Name] = value; }
            public override object GetValue(object component) { return ((DynamicObjectType)component)[_prop.Name] ?? _prop.Value; }
        }
    }

    public class DynamicProperty
    {
        public DynamicProperty(string category, string description, string name, object value)
        {
            Category = category;
            Desc = description;
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public string Desc { get; set; }
        public string Category { get; set; }
        public object Value { get; set; }
        Type _type;

        public Type Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
                Value = Activator.CreateInstance(value);
            }
        }
    }
}
