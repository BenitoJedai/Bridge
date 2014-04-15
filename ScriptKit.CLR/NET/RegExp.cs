[ScriptKit.CLR.Ignore]
public sealed class RegExp 
{
    public int LastIndex = 0;

    public readonly string Source = null;
    
    public readonly bool Global = false;
    public readonly bool IgnoreCase = false;
    public readonly bool Multiline = false;

    public RegExp(string pattern) 
    { 
    }

    public RegExp(string pattern, string flags) 
    { 
    }

    public string[] exec(string value) 
    { 
        return null; 
    }

    public bool Test(string value) 
    { 
        return false; 
    }
}
