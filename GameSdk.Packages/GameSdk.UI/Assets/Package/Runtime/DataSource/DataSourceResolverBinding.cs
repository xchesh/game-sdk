using System;
using Unity.Properties;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

[UxmlObject] [Preserve]
public partial class DataSourceResolverBinding : CustomBinding
{
    [UxmlAttribute]
    public string type { get; set; }

    private Type _type;
    private Type Type => _type ??= Type.GetType(type);

    [RequiredMember]
    public DataSourceResolverBinding()
    {
        updateTrigger = BindingUpdateTrigger.WhenDirty;
    }

    [RequiredMember]
    public DataSourceResolverBinding(Type bindingType)
    {
        _type = bindingType;
        updateTrigger = BindingUpdateTrigger.WhenDirty;
    }

    private IDataSourceResolver FindDataSourceResolver(BindingContext context)
    {
        if (context.dataSource is IDataSourceResolver localResolver)
        {
            return localResolver;
        }

        if (context.targetElement.panel.visualTree.childCount > 0 && context.targetElement.panel.visualTree[0].dataSource is IDataSourceResolver childResolver)
        {
            return childResolver;
        }

        if (context.targetElement.panel.visualTree.dataSource is IDataSourceResolver rootResolver)
        {
            return rootResolver;
        }

        return null;
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

        var resolver = FindDataSourceResolver(context);

        if (resolver == null)
        {
            return new BindingResult(BindingStatus.Success, "DataSourceResolverBinding: DataSource is not set.");
        }

        // Resolve the type from the string.
        var resolvedType = Type;

        if (resolvedType == null)
        {
            return CreateErrorResult(element, context.bindingId, 101);
        }

        // Resolve the value from the DataSource.
        var value = resolver.Resolve(resolvedType);

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
