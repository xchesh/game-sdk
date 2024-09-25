using Unity.Properties;
using UnityEngine.Scripting;

[UnityEngine.Scripting.Preserve]
public class DataSource
{
    [CreateProperty]
    public IDataSourceResolver Resolver { get; }

    [RequiredMember]
    public DataSource(IDataSourceResolver resolver)
    {
        Resolver = resolver;
    }
}
