#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace GameSdk.Core.Toolbox
{
    public abstract class AdvancedDropdownAbstract<T> : AdvancedDropdown where T : AdvancedDropdownItem
    {
        protected readonly string Name;
        protected readonly IEnumerable<AdvancedDropdownData> Items;

        protected AdvancedDropdownAbstract(string name, IEnumerable<AdvancedDropdownData> items,
            AdvancedDropdownState state) : base(state)
        {
            Name = name;
            Items = items;
        }

        protected override AdvancedDropdownItem BuildRoot()
        {
            var root = new AdvancedDropdownItem("Select " + Name);

            var groups = Items.GroupBy(i => i.Group).ToArray();

            if (groups.Length > 1)
            {
                foreach (var group in groups)
                {
                    var isGroupValid = group.Key != null;
                    var groupItem = isGroupValid ? new AdvancedDropdownItem(group.Key) : root;

                    foreach (var item in group)
                    {
                        groupItem.AddChild(CreateDropdownItem(item));
                    }

                    if (isGroupValid)
                    {
                        root.AddChild(groupItem);
                    }
                }

                return root;
            }

            foreach (var item in Items)
            {
                root.AddChild(CreateDropdownItem(item));
            }

            return root;
        }

        protected override void ItemSelected(AdvancedDropdownItem item)
        {
            base.ItemSelected(item);

            if (item is T dropdownAdvancedItem)
            {
                SetPropertyValue(dropdownAdvancedItem);
            }
        }

        protected abstract T CreateDropdownItem(AdvancedDropdownData data);
        protected abstract void SetPropertyValue(T item);


        public virtual void SetMinimumSize(float width, float height)
        {
            var minWidth = Mathf.Max(minimumSize.x, width);
            var minHeight = Mathf.Max(minimumSize.y, height);

            minimumSize = new UnityEngine.Vector2(minWidth, minHeight);
        }
    }
}
#endif