[assembly: Metalama.Extensions.Multicast.AspectTests.AddTag( "PublicType", AttributeTargetElements = MulticastTargets.Class, AttributeTargetTypeAttributes = MulticastAttributes.Public )]
[assembly: Metalama.Extensions.Multicast.AspectTests.AddTag( "MethodOfPublicType", AttributeTargetElements = MulticastTargets.Method, AttributeTargetTypeAttributes = MulticastAttributes.Public )]
[Tag( "PublicType" )]
// <target>
public class PublicClass
{
    [Tag( "MethodOfPublicType" )]
    public void PublicMethod()
    {
    }
    [Tag( "MethodOfPublicType" )]
    internal void InternalMethod()
    {
    }
}
internal class InternalClass
{
    public void PublicMethod()
    {
    }
    internal void InternalMethod()
    {
    }
}