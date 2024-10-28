using System;
using UnityEngine;

namespace GameSdk.Sources.Feedbacks
{
    [AttributeUsage(AttributeTargets.Field)]
    public class AnimatorTriggerDropdownAttribute : PropertyAttribute
    {
        public readonly string AnimatorFieldName;

        public AnimatorTriggerDropdownAttribute(string animatorFieldName)
        {
            AnimatorFieldName = animatorFieldName;
        }
    }
}
