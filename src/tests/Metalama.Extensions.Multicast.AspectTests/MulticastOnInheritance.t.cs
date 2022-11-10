[AddTagInherited("Tagged", true)]
[Tag("Tagged")]
public class C
{
  [Tag("Tagged")]
  protected virtual void M()
  {
  }
}
[Tag("Tagged")]
// <target>
public class D : C
{
  [Tag("Tagged")]
  protected override void M()
  {
  }
  [Tag("Tagged")]
  protected void N()
  {
  }
}