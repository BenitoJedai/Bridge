[ScriptKit.Core.Ignore]
public sealed class RegExp 
{
    public int lastIndex = 0;

    public readonly string source = null;
    
    public readonly bool global = false;
    public readonly bool ignoreCase = false;
    public readonly bool multiline = false;

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

    public bool test(string value) 
    { 
        return false; 
    }
}
