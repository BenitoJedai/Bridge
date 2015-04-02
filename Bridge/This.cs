using Bridge;

namespace Bridge
{
    [Ignore]
    [Name("this")]
    public static class This
    {
        [Template("this")]
        public static ThisInstance Instance;
    }

    [Ignore]
    [Name("this")]
    public class ThisInstance
    {
    }
}