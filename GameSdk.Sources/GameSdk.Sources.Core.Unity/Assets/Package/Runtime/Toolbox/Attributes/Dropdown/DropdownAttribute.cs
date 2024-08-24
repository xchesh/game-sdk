using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameSdk.Sources.Core.Toolbox
{
    [AttributeUsage(AttributeTargets.Field)]
    public abstract class DropdownAttribute : PropertyAttribute
    {
        public abstract IEnumerable<AdvancedDropdownData> GetDropdownValues();
    }
}
