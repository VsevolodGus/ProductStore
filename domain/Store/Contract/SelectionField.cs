using System.Collections.Generic;

namespace Store.Contract
{
    public class SelectionField : Field
    {
        public IReadOnlyDictionary<string, string> Items { get; }
        public SelectionField(string label, string name, string value, IReadOnlyDictionary<string, string> items)
            : base(label, name, value)
        {
            Items = items;
        }
    }
}
