using System;
using Unity.Properties;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

[UxmlObject][UnityEngine.Scripting.Preserve]
public partial class DataSourceResolverBinding : CustomBinding
{
    [UxmlAttribute]
    public string type { get; set; }

    [RequiredMember]
    public DataSourceResolverBinding()
    {
        updateTrigger = BindingUpdateTrigger.WhenDirty;
    }

    protected override BindingResult Update(in BindingContext context)
    {
        var element = context.targetElement;

#if UNITY_EDITOR
        if (Application.isPlaying is false)
        {
            return new BindingResult(BindingStatus.Success);
        }
#endif
        // Check if the context has a DataSource. DataSource is a custom class that contains a Resolver property.
        // Return success if it does not. This is to prevent the binding from running if the DataSource is not set.
        if (context.dataSource is not DataSource dataSource)
        {
            return new BindingResult(BindingStatus.Success, "DataSourceResolverBinding: DataSource is not set.");
        }

        // Resolve the type from the string.
        var resolvedType = Type.GetType(type);

        if (resolvedType == null)
        {
            return CreateErrorResult(element, context.bindingId, 101);
        }

        // Resolve the value from the DataSource.
        var value = dataSource.Resolver.Resolve(resolvedType);

        if (value == null)
        {
            return CreateErrorResult(element, context.bindingId, 102);
        }

        // Set the value to the element.
        if (ConverterGroups.TrySetValueGlobal(ref element, context.bindingId, value, out var errorCode) is false)
        {
            return CreateErrorResult(element, context.bindingId, errorCode);
        }

        return new BindingResult(BindingStatus.Success);
    }

    private BindingResult CreateErrorResult(VisualElement element, string contextBindingId, int errorCode)
    {
        // Error handling
        var bindingTypename = TypeUtility.GetTypeDisplayName(typeof(DataSourceResolverBinding));
        var bindingId = $"{TypeUtility.GetTypeDisplayName(element.GetType())}.{contextBindingId}";

        if (errorCode <= (int)VisitReturnCode.AccessViolation)
        {
            return CreateErrorResult(element, contextBindingId, (VisitReturnCode)errorCode);
        }

        return errorCode switch
        {
            101 => new BindingResult(BindingStatus.Failure, $"{bindingTypename}: Binding id `{bindingId}` type `{type}` not found."),
            102 => new BindingResult(BindingStatus.Failure, $"{bindingTypename}: Trying set value for binding id `{bindingId}`, but it is null."),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private BindingResult CreateErrorResult(VisualElement element, string contextBindingId, VisitReturnCode errorCode)
    {
        // Error handling
        var bindingTypename = TypeUtility.GetTypeDisplayName(typeof(DataSourceResolverBinding));
        var bindingId = $"{TypeUtility.GetTypeDisplayName(element.GetType())}.{contextBindingId}";

        return errorCode switch
        {
            VisitReturnCode.InvalidPath => new BindingResult(BindingStatus.Failure, $"{bindingTypename}: Binding id `{bindingId}` is either invalid or contains a `null` value."),
            VisitReturnCode.InvalidCast => new BindingResult(BindingStatus.Failure, $"{bindingTypename}: Type `{type}` not found for binding id `{bindingId}`"),
            VisitReturnCode.AccessViolation => new BindingResult(BindingStatus.Failure, $"{bindingTypename}: Trying set value for binding id `{bindingId}`, but it is read-only."),
            VisitReturnCode.NullContainer => new BindingResult(BindingStatus.Failure, $"{bindingTypename}: Container is null for binding id `{bindingId}`"),
            VisitReturnCode.InvalidContainerType => new BindingResult(BindingStatus.Failure, $"{bindingTypename}: Container type is invalid for binding id `{bindingId}`"),
            VisitReturnCode.MissingPropertyBag => new BindingResult(BindingStatus.Failure, $"{bindingTypename}: No property bag found for binding id `{bindingId}`"),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
