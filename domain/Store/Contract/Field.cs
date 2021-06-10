namespace Store.Contract
{
    public abstract class Field
    {
        public string Label { get; }

        public string Name { get; }

        public string Value { get; }

        protected Field(string label, string name, string value)
        {
            Label = label;
            Name = name;
            Value = value;
        }
    }
}
