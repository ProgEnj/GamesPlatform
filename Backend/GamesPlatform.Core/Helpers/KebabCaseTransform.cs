using System.Text.RegularExpressions;

namespace GamesPlatform.Core.Helpers;

public static class KebabCaseTransform
{
    private static string regexPattern = "[^a-zA-Z0-9\\s]";
    
    public static string ToKebabCase(string name)
    {
        name = name.Trim();
        name = name.ToLower();
        
        var regex = new Regex(regexPattern);
        name = regex.Replace(name, "");
        name = name.Replace(' ', '-');

        return name;
    }
}