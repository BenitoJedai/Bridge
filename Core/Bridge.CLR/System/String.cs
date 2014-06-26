using Bridge.CLR;

namespace System
{
    [Ignore]
    [Name("String")]
    [Constructor("String")]
    public sealed class String
    {
        public readonly int Length = 0;

        public static string FromCharCode(params int[] chars) 
        { 
            return null; 
        }

        public String() 
        { 
        }
        
        public String(object obj) 
        { 
        }

        public string CharAt(int pos) 
        { 
            return null; 
        }
        
        public int CharCodeAt(int pos) 
        { 
            return 0; 
        }

        public static string Concat(string s1, string s2)
        {
            return null;
        }

        public static string Concat(string s1, string s2, string s3)
        {
            return null;
        }

        public static string Concat(string s1, string s2, string s3, string s4)
        {
            return null;
        }

        
        public static string Concat(params string[] strings)
        {
            return null;
        }

        
        public static string Concat(object o1, object o2)
        {
            return null;
        }

        
        public static string Concat(object o1, object o2, object o3)
        {
            return null;
        }

        
        public static string Concat(object o1, object o2, object o3, object o4)
        {
            return null;
        }

        
        public static string Concat(params object[] o)
        {
            return null;
        }

        /// <summary>
        /// The indexOf() method returns the index within the calling String object of the first occurrence of the specified value, starting the search at fromIndex. Returns -1 if the value is not found.
        /// </summary>
        /// <param name="searchString">A string representing the value to search for.</param>
        /// <returns></returns>
        public int IndexOf(string searchValue) 
        { 
            return 0; 
        }

        /// <summary>
        /// The indexOf() method returns the index within the calling String object of the first occurrence of the specified value, starting the search at fromIndex. Returns -1 if the value is not found.
        /// </summary>
        /// <param name="searchString">A string representing the value to search for.</param>
        /// <param name="fromIndex">The location within the calling string to start the search from. It can be any integer. The default value is 0. If fromIndex < 0 the entire string is searched (same as passing 0). If fromIndex >= searchValue.length, the method will return -1.</param>
        /// <returns></returns>
        public int IndexOf(string searchValue, int fromIndex) 
        { 
            return 0; 
        }

        /// <summary>
        /// The lastIndexOf() method returns the index within the calling String object of the last occurrence of the specified value, or -1 if not found. The calling string is searched backward, starting at fromIndex.
        /// </summary>
        /// <param name=" searchValue">A string representing the value to search for.</param>
        /// <returns></returns>
        public int LastIndexOf(string searchValue) 
        { 
            return 0; 
        }

        /// <summary>
        /// The lastIndexOf() method returns the index within the calling String object of the last occurrence of the specified value, or -1 if not found. The calling string is searched backward, starting at fromIndex.
        /// </summary>
        /// <param name=" searchValue">A string representing the value to search for.</param>
        /// <param name="fromIndex">The location within the calling string to start the search at, indexed from left to right. It can be any integer. The default value is searchValue.length - 1. If fromIndex < 0 or fromIndex >= searchValue.length, the method will return -1.</param>
        /// <returns></returns>
        public int LastIndexOf(string searchValue, int fromIndex) 
        { 
            return 0; 
        }

        /// <summary>
        /// The localeCompare() method returns a number indicating whether a reference string comes before or after or is the same as the given string in sort order.
        /// The new locales and options arguments let applications specify the language whose sort order should be used and customize the behavior of the function. In older implementations, which ignore the locales and options arguments, the locale and sort order used are entirely implementation dependent.
        /// </summary>
        /// <param name="compareString">The string against which the referring string is comparing</param>
        /// <returns></returns>
        public int LocaleCompare(string compareString) 
        { 
            return 0; 
        }

        /// <summary>
        /// The localeCompare() method returns a number indicating whether a reference string comes before or after or is the same as the given string in sort order.
        /// The new locales and options arguments let applications specify the language whose sort order should be used and customize the behavior of the function. In older implementations, which ignore the locales and options arguments, the locale and sort order used are entirely implementation dependent.
        /// </summary>
        /// <param name="compareString">The string against which the referring string is comparing</param>
        /// <param name="locales">A string with a BCP 47 language tag, or an array of such strings. For the general form and interpretation of the locales argument, see the Intl page. The following Unicode extension keys are allowed:</param>
        /// <returns></returns>
        public int LocaleCompare(string compareString, string locales)
        {
            return 0;
        }

        /// <summary>
        /// The localeCompare() method returns a number indicating whether a reference string comes before or after or is the same as the given string in sort order.
        /// The new locales and options arguments let applications specify the language whose sort order should be used and customize the behavior of the function. In older implementations, which ignore the locales and options arguments, the locale and sort order used are entirely implementation dependent.
        /// </summary>
        /// <param name="compareString">The string against which the referring string is comparing</param>
        /// <param name="locales">A string with a BCP 47 language tag, or an array of such strings. For the general form and interpretation of the locales argument, see the Intl page. The following Unicode extension keys are allowed:</param>
        /// <param name="options">An object with some or all of the following properties:</param>
        /// <returns></returns>
        public int LocaleCompare(string compareString, string locales, object options)
        {
            return 0;
        }

        public string[] Match(RegExp regexp) 
        { 
            return null; 
        }

        public string[] Match(string regexp) 
        { 
            return null; 
        }

        /// <summary>
        /// The replace() method returns a new string with some or all matches of a pattern replaced by a replacement.  The pattern can be a string or a RegExp, and the replacement can be a string or a function to be called for each match.
        /// </summary>
        /// <param name="regexp">A RegExp object. The match is replaced by the return value of parameter #2.</param>
        /// <param name="newSubStr">The String that replaces the substring received from parameter #1. A number of special replacement patterns are supported; see the "Specifying a string as a parameter" section below.</param>
        /// <returns></returns>
        public string Replace(RegExp regexp, string newSubStr) 
        { 
            return null; 
        }

        /// <summary>
        /// The replace() method returns a new string with some or all matches of a pattern replaced by a replacement.  The pattern can be a string or a RegExp, and the replacement can be a string or a function to be called for each match.
        /// </summary>
        /// <param name="regexp">A RegExp object. The match is replaced by the return value of parameter #2.</param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public string Replace(RegExp regexp, Func<string, string> callback) 
        { 
            return null; 
        }

        /// <summary>
        /// The replace() method returns a new string with some or all matches of a pattern replaced by a replacement.  The pattern can be a string or a RegExp, and the replacement can be a string or a function to be called for each match.
        /// </summary>
        /// <param name="regexp">A RegExp object. The match is replaced by the return value of parameter #2.</param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public string Replace(RegExp regexp, Func<string, int, string> callback) 
        { 
            return null; 
        }

        /// <summary>
        /// The replace() method returns a new string with some or all matches of a pattern replaced by a replacement.  The pattern can be a string or a RegExp, and the replacement can be a string or a function to be called for each match.
        /// </summary>
        /// <param name="regexp">A RegExp object. The match is replaced by the return value of parameter #2.</param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public string Replace(RegExp regexp, Func<string, int, string, string> callback) 
        { 
            return null; 
        }

        /// <summary>
        /// The replace() method returns a new string with some or all matches of a pattern replaced by a replacement.  The pattern can be a string or a RegExp, and the replacement can be a string or a function to be called for each match.
        /// </summary>
        /// <param name="substr">A String that is to be replaced by newSubStr.</param>
        /// <param name="newSubStr">The String that replaces the substring received from parameter #1. A number of special replacement patterns are supported; see the "Specifying a string as a parameter" section below.</param>
        /// <returns></returns>
        public string Replace(string substr, string newSubStr) 
        { 
            return null; 
        }

        /// <summary>
        /// The replace() method returns a new string with some or all matches of a pattern replaced by a replacement.  The pattern can be a string or a RegExp, and the replacement can be a string or a function to be called for each match.
        /// </summary>
        /// <param name="substr">A String that is to be replaced by newSubStr.</param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public string Replace(string substr, Func<string, string> callback) 
        { 
            return null; 
        }

        /// <summary>
        /// The replace() method returns a new string with some or all matches of a pattern replaced by a replacement.  The pattern can be a string or a RegExp, and the replacement can be a string or a function to be called for each match.
        /// </summary>
        /// <param name="substr">A String that is to be replaced by newSubStr.</param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public string Replace(string substr, Func<string, int, string> callback) 
        { 
            return null; 
        }

        /// <summary>
        /// The replace() method returns a new string with some or all matches of a pattern replaced by a replacement.  The pattern can be a string or a RegExp, and the replacement can be a string or a function to be called for each match.
        /// </summary>
        /// <param name="substr">A String that is to be replaced by newSubStr.</param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public string Replace(string substr, Func<string, int, string, string> callback)
        { 
            return null; 
        }

        public int Search(RegExp regexp) 
        { 
            return 0; 
        }
        
        public int Search(string regexp) 
        { 
            return 0; 
        }

        public string Slice(int start) 
        { 
            return null; 
        }

        public string Slice(int start, int end) 
        { 
            return null; 
        }

        public string[] Split(string separator) 
        { 
            return null; 
        }

        public string[] Split(RegExp separator) 
        { 
            return null; 
        }

        public string[] Split(string separator, int limit) 
        { 
            return null; 
        }

        public string[] Split(RegExp separator, int limit) 
        { 
            return null; 
        }

        public string Substring(int start) 
        { 
            return null; 
        }

        public string Substring(int start, int end) 
        { 
            return null; 
        }

        public string Substr(int start) 
        { 
            return null; 
        }

        public string Substr(int start, int length) 
        { 
            return null; 
        }

        public string ToLowerCase() 
        { 
            return null; 
        }
        
        public string ToLocaleLowerCase() 
        { 
            return null; 
        }
        
        public string ToUpperCase() 
        { 
            return null; 
        }
        
        public string ToLocaleUpperCase() 
        { 
            return null; 
        }

        public static bool operator ==(string s1, string s2)
        {
            return false;
        }

        public static bool operator !=(string s1, string s2)
        {
            return false;
        }

    }
}