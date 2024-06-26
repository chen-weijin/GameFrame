using System.Collections.Generic;

public static class DictionaryExtension
{
    public static TV SafeGet<TK, TV>(this Dictionary<TK, TV> dic, TK key)
    {
        if (dic.TryGetValue(key, out TV value))
        {
            return value;
        }

        return default;
    }
}
