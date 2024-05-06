namespace Core.Common.Toolbox
{
    public class AdvancedDropdownData
    {
        public object Value { get; }
        public string DisplayName { get; }
        public string Group { get; }

        public AdvancedDropdownData(object value, string displayName, string group = null)
        {
            Value = value;
            DisplayName = displayName;
            Group = group;
        }
    }
}
