namespace System 
{
    [Bridge.CLR.Ignore]
    [Bridge.CLR.Name("Boolean")]
    [Bridge.CLR.Constructor("!!")]
    public struct Boolean 
    {
        public Boolean(object value) 
        { 
        }
    }
}