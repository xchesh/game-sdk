using System;

namespace GameSdk.Sources.Feedbacks
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
    public sealed class FeedbackStrategyAttribute : Attribute
    {
    }
}