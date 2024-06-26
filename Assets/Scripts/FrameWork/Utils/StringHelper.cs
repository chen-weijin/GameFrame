using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace NGame
{

    public class StringHelper
    {
        //单线程使用的字符串拼接工具，减少gc产生
        public static StringBuilder Formater = new StringBuilder(1024);
        public static readonly char VECTOR_SEPARATOR = ',';
        public static void ClearFormater()
        {
            Formater.Remove(0, Formater.Length);
        }

        static public string BytesToString(byte[] bytes)
        {
            return System.Text.Encoding.UTF8.GetString(bytes).TrimEnd('\0');
        }

        static public string ByteToStringCutEndChar(byte[] bytes)
        {
            string tStr = System.Text.Encoding.UTF8.GetString(bytes).Trim('\0');
            if (tStr.Contains("\0"))
            {
                tStr = tStr.Substring(0, tStr.IndexOf("\0"));
            }
            return tStr;
        }

        /// <summary>
        /// 字符串拼接函数
        /// </summary>
        public static string Concat(params string[] strAry)
        {
            ClearFormater();
            foreach (var str in strAry)
            {
                Formater.Append(str);
            }
            return Formater.ToString();
        }

        static public string BytesToString(string str)
        {
            return str;
        }

        static string _GetStringByBytes(byte[] src)
        {
            if (src == null) return null;
            int iLen = src.Length;
            int idx = iLen - 1;
            for (; idx >= 0; --idx)
            {
                if (src[idx] != 0)
                {
                    break;
                }
            }
            return System.Text.Encoding.UTF8.GetString(src, 0, idx + 1);
        }

        static private Dictionary<string, string> strCache = new Dictionary<string, string>();
        static int cache_count = 0;
        static int cache_size = 0;

        public static void ClearStringCache()
        {
            cache_count = 0;
            cache_size = 0;
            //clear没有清capacity
            //strCache.Clear();
            strCache = new Dictionary<string, string>();
        }

        public static string GetCachedString(string str)
        {
            string strOut;
            if (strCache.TryGetValue(str, out strOut))
            {
                cache_size += strOut.Length;
                cache_count++;
                return strOut;
            }
            else
            {
                strCache.Add(str, str);
                return str;
            }
        }

        /// <summary>
        /// 将一个UTF8编码格式的byte数组，转换为一个String
        /// </summary>
        /// <param name="str">编码格式的数组</param>
        /// <returns>转换的string</returns>
        static public string UTF8BytesToString(ref byte[] szBytes)
        {
            try
            {
                string str = _GetStringByBytes(szBytes);
                return GetCachedString(str);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        static public string ASCIIBytesToString(byte[] data)
        {
            if (data == null)
            {
                return null;
            }

            try
            {
                return Encoding.ASCII.GetString(data).TrimEnd('\0');
            }
            catch (Exception)
            {
                return null;
            }
        }

        // adapter
        static public string UTF8BytesToString(ref string str)
        {
            return str;
            //return Localization.Get(str);
        }
        static public string GetWarnPrefabName(string path)
        {
            string res = string.Empty;
            if (!string.IsNullOrEmpty(path))
            {
                if (path.Contains('|'))
                {
                    return path;
                }
                string insertStr = "_red";
                res = path.Insert(path.LastIndexOf('.'), insertStr);
            }
            return res;
        }
        static public string UTF8BytesToString(string str)
        {
            return str;
            if (string.IsNullOrEmpty(str))
                return str;
            //return Localization.Get(str);
        }

        static public string TruncateStringByBytesCount(string str, int count)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);
            if (bytes.Length <= count)
            {
                return str;
            }

            int tLastIdx = 0;
            int tCount = 0;
            for (int i = 0; i < str.Length - 1; ++i)
            {
                string tChar = str.Substring(i, 1);
                byte[] tbytes = System.Text.Encoding.UTF8.GetBytes(tChar);
                tCount += tbytes.Length;
                tLastIdx = i;
                if (tCount > count)
                {
                    tLastIdx--;
                    break;
                }
            }

            String tNewStr = str.Substring(0, tLastIdx);

            return tNewStr;
        }

        public static string InterceptPlaces(string _mString, int _mLenght, int startIndex = 0)
        {
            int _placesNum = 0;
            char[] _charArray = _mString.ToCharArray();
            string _charReturn = string.Empty;
            for (int i = 0; i < _charArray.Length; i++)
            {
                if (_placesNum < _mLenght + startIndex)
                {
                    char _eachChar = _charArray[i];
                    if (char.IsPunctuation(_eachChar))
                        _placesNum += 1;
                    else if ((_eachChar >= 0x4e00 && _eachChar <= 0x9fa5)) //判断中文字符
                        _placesNum += 2;
                    else if (_eachChar >= 0x0000 && _eachChar <= 0x00ff) //已2个字节判断
                        _placesNum += 1;
                    if (_placesNum > startIndex) _charReturn += _eachChar.ToString();
                }
                else if (_placesNum >= _mLenght)
                {
                    break;
                }
            }
            return _charReturn.ToString();
        }
        public static int GetStringLength(string _mString)
        {
            int _placesNum = 0;
            char[] _charArray = _mString.ToCharArray();
            string _charReturn = string.Empty;
            for (int i = 0; i < _charArray.Length; i++)
            {
                char _eachChar = _charArray[i];
                if (char.IsPunctuation(_eachChar))
                    _placesNum += 1;
                if ((_eachChar >= 0x4e00 && _eachChar <= 0x9fa5) || (_eachChar >= 'A' && _eachChar <= 'Z')) //判断中文字符
                    _placesNum += 2;
                else if (_eachChar >= 0x0000 && _eachChar <= 0x00ff) //已2个字节判断
                    _placesNum += 1;
                _charReturn += _eachChar.ToString();

            }
            return _placesNum;
        }
        static string[] _tagArr = new string[] { "color", "b", "i", "size", "material", "quad" };
        /// <summary>
        /// 去除富文本标签
        /// </summary>
        /// <param name="richText"></param>
        /// <returns></returns>
        public static string ReplaceTag(string richText)
        {
            string newText = richText;
            foreach (string tag in _tagArr)
            {
                Regex m_richRegex = new Regex(string.Format("<{0}[^>]*?>[\\s\\S]*?<\\/{0}>", tag));

                var matches = m_richRegex.Matches(newText);
                for (int i = 0; i < matches.Count; i++)
                {
                    string str = Regex.Replace(matches[i].Value, string.Format("<{0}[^>]*?>", tag), "").Replace(string.Format("</{0}>", tag), "");
                    newText = newText.Replace(matches[i].Value, str);
                }
            }
            return newText;
        }
        /// <summary>
        /// 将一个string转换为UTF-8数组，复制到buffer中。
        /// </summary>
        /// <param name="str">需要转换的string</param>
        /// <param name="buffer">目标buffer</param>
        static public void StringToUTF8Bytes(string str, ref byte[] buffer)
        {
            if (str == null || buffer == null)
                return;

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);
            if (bytes.Length >= buffer.Length)
            {
                Debug.LogError(string.Format("Overflow String:{0}", str));
                FillErrorCodeToBuf(ref buffer);
            }
            else
            {
                System.Buffer.BlockCopy(bytes, 0, buffer, 0, bytes.Length);
                buffer[bytes.Length] = 0;
            }
        }

        static void FillErrorCodeToBuf(ref byte[] buffer)
        {
            try
            {
                buffer[0] = 79;
                buffer[1] = 86;
                buffer[2] = 69;
                buffer[3] = 82;
                buffer[4] = 70;
                buffer[5] = 76;
                buffer[6] = 79;
                buffer[7] = 87;

                buffer[8] = 48;
                buffer[9] = 88;
                buffer[10] = 67;
                buffer[11] = 67;
                buffer[12] = 67;
                buffer[13] = 67;
                buffer[14] = 67;
                buffer[15] = 67;

                buffer[16] = 0;

            }
            catch (Exception)
            {
                // ignored
            }
        }
        static void FillErrorCodeToSBuf(ref sbyte[] buffer)
        {
            try
            {
                buffer[0] = 79;
                buffer[1] = 86;
                buffer[2] = 69;
                buffer[3] = 82;
                buffer[4] = 70;
                buffer[5] = 76;
                buffer[6] = 79;
                buffer[7] = 87;

                buffer[8] = 48;
                buffer[9] = 88;
                buffer[10] = 67;
                buffer[11] = 67;
                buffer[12] = 67;
                buffer[13] = 67;
                buffer[14] = 67;
                buffer[15] = 67;

                buffer[16] = 0;

            }
            catch (Exception)
            {
                // ignored
            }
        }

        //--------------------------------------
        /// 是否为合法的字符串
        //--------------------------------------
        public static bool IsAvailableString(string str)
        {
            int ret = 0;
            int in_pos = 0;
            char ch = (char)0;
            bool surrogate = false;

            int len = str.Length;

            while (in_pos < len)
            {
                ch = str[in_pos];

                if (surrogate)
                {
                    if (ch >= 0xDC00 && ch <= 0xDFFF)
                    {
                        ret += 4;
                    }
                    else
                    {
                        /* invalid surrogate pair */
                        Debug.Log(string.Format("invalid utf-16 sequence at {0} (missing surrogate tail)", in_pos));
                        return false;
                    }

                    surrogate = false;
                }
                else
                {
                    /* fast path optimization */
                    if (ch < 0x80)
                    {
                        for (; in_pos < len; in_pos++)
                        {
                            if (str[in_pos] < 0x80)
                            {
                                ++ret;
                            }
                            else
                            {
                                break;
                            }
                        }

                        continue;
                    }
                    else if (ch < 0x0800)
                    {
                        ret += 2;
                    }
                    else if (ch >= 0xD800 && ch <= 0xDBFF)
                    {
                        surrogate = true;
                    }
                    else if (ch >= 0xDC00 && ch <= 0xDFFF)
                    {
                        /* invalid surrogate pair */
                        Debug.Log(string.Format("invalid utf-16 sequence at {0} (missing surrogate head)", in_pos));
                        return false;
                    }
                    else
                    {
                        ret += 3;
                    }
                }

                in_pos++;
            }

            return true;
        }

        /// <summary>
        /// 将一个string转换为UTF-8数组，复制到buffer中。
        /// </summary>
        /// <param name="str">需要转换的string</param>
        /// <param name="buffer">目标buffer</param>
        static public void StringToUTF8Bytes(string str, ref sbyte[] buffer)
        {
            if (str == null || buffer == null)
                return;


            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);
            if (bytes.Length >= buffer.Length)
            {
                Debug.LogError(string.Format("Overflow String:{0}", str));
                FillErrorCodeToSBuf(ref buffer);
            }
            else
            {
                System.Buffer.BlockCopy(bytes, 0, buffer, 0, bytes.Length);
                buffer[bytes.Length] = 0;
            }
        }

        public static string ShortName(string Name, int maxLen = 6)
        {
            if (string.IsNullOrEmpty(Name))
                return string.Empty;

            Name = Name.Replace("\r", "");
            Name = Name.Replace("\n", "");
            if (Name.Length <= maxLen)
                return Name;
            else
            {
                string tmpName = U16StrToU8Str(ref Name);
                if (tmpName.Length < maxLen)
                {
                    return tmpName;
                }
                else
                {
                    return tmpName.Substring(0, maxLen) + "...";
                }
            }
        }

        /// <summary>
        /// 将一个Unicode编码格式(可能混编u8+u16)的字符串，转换为一个U8的String
        /// </summary>
        static public string U16StrToU8Str(ref string str)
        {
            if (str == null)
                return string.Empty;

            List<Char> temp = new List<Char>();
            for (int i = 0, iMax = str.Length; i < iMax; ++i)
            {
                Char ch = str[i];
                bool bSurrogate = false;
                if (char.IsSurrogate(ch) && i != iMax - 1)
                {
                    i++;
                    bSurrogate = true;
                }
                if (!bSurrogate)
                {
                    temp.Add(ch);
                }
                else
                {
                    //unknow
                    temp.Add('\ufffd');
                }
            }

            return new string(temp.ToArray());
        }
    }

    #region StringExtension

    public static class StringExtension
    {
        public static string RemoveExtension(this string s)
        {
            if (s == null)
                return null;

            int index = s.LastIndexOf('.');
            if (index == -1)
                return s;

            return s.Substring(0, index);
        }

        public static readonly string asset_str = "Assets/";

        public static string FullPathToAssetPath(this string s)
        {
            if (s == null)
                return null;

            string path = StringExtension.asset_str + s.Substring(Application.dataPath.Length + 1);
            return path.Replace('\\', '/');
        }

        public static string AssetPathToFullPath(this string s)
        {
            if (s == null)
                return null;

            if (!s.StartsWith(StringExtension.asset_str))
                return null;

            string path = Application.dataPath;
            path += "/";
            path += s.Remove(0, StringExtension.asset_str.Length);
            return path;
        }

        public static string GetFileName(this string s)
        {
            string temp = s;
            int start = temp.LastIndexOfAny(new char[] { '/', '\\' });
            int end = temp.LastIndexOf(".");
            if (start > 0)
            {
                if (end > 0)
                {
                    temp = temp.Substring(start + 1, end - start - 1);
                }
                else
                {
                    temp = temp.Substring(start + 1);
                }
            }
            return temp;
        }
        public static string GetFileExtension(this string s)
        {
            int num = s.LastIndexOf('.');
            if (num == -1)
                return null;
            return s.Substring(num + 1);
        }

        public static string GetFileExtensionUpper(this string s)
        {
            string ext = s.GetFileExtension();
            if (ext == null)
                return null;
            return ext.ToUpper();
        }

        public static string GetHierarchyName(this GameObject go, string delimeter = ".")
        {
            if (go == null)
                return "<null>";

            string name = "";
            while (go != null)
            {
                if (string.IsNullOrEmpty(name))
                {
                    name = go.name;
                }
                else
                {
                    name = go.name + delimeter + name;
                }
                var tr = go.transform.parent;
                go = tr != null ? tr.gameObject : null;
            }

            return name;
        }

        /// <summary>
        /// 获取完整Transform路径,Debug用，非Debug请不要用该函数会产生大量GC
        /// </summary>
        public static string GetFullPath(this Transform t)
        {
            if (t == null)
                return "";

            const int maxCount = 100;
            int count = 0;

            string fullPath = t.name;
            while (t.parent != null && count < maxCount)
            {
                fullPath = t.parent.name + "/" + fullPath;
                t = t.parent;
                ++count;
            }

            return fullPath;
        }

        //这是Java的HashCode代码
        public static int JavaHashCode(this string s)
        {
            int h = 0;
            int len = s.Length;
            if (len > 0)
            {
                int off = 0;

                for (int i = 0; i < len; i++)
                {
                    char c = s[off++];
                    h = 31 * h + c;
                }
            }
            return h;
        }
        static int _JavaHashCode(this string s, int len)
        {
            int h = 0;
            if (len > 0)
            {
                int off = 0;

                for (int i = 0; i < len; i++)
                {
                    char c = s[off++];
                    if (c >= 'A' && c <= 'Z')
                    {
                        c += (char)('a' - 'A');
                    }
                    h = 31 * h + c;
                }
            }
            return h;
        }
        public static int JavaHashCodeIgnoreCaseEraseExt(this string s)
        {
            int fIndex = s.LastIndexOf('.');
            int len = fIndex > 0 ? fIndex : s.Length;
            return s._JavaHashCode(len);
        }
        //这是Java的HashCode代码，并且ignoreCase
        public static int JavaHashCodeIgnoreCase(this string s)
        {
            return s._JavaHashCode(s.Length);
        }

        public static bool isWildCardMatch(this string targetString, string wildCard, bool ignoreCase)
        {
            string pattern = "^" + System.Text.RegularExpressions.Regex.Escape(wildCard)
                             .Replace(@"\*", ".*")
                             .Replace(@"\?", ".")
                      + "$";
            if (ignoreCase)
                return System.Text.RegularExpressions.Regex.IsMatch(targetString, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            else
                return System.Text.RegularExpressions.Regex.IsMatch(targetString, pattern);
        }

        private static object[] Param1 = new object[] { null };
        public static String Format(String format, object arg0)
        {
            Param1[0] = arg0;
            return Format(format, Param1);
        }

        private static object[] Param2 = new object[] { null, null };
        public static String Format(String format, object arg0, object arg1)
        {
            Param2[0] = arg0;
            Param2[1] = arg1;
            return Format(format, Param2);
        }

        private static object[] Param3 = new object[] { null, null, null };
        public static String Format(String format, object arg0, object arg1, object arg2)
        {
            Param3[0] = arg0;
            Param3[1] = arg1;
            Param3[2] = arg2;
            return Format(format, Param3);
        }

        private static StringBuilder ms = new StringBuilder();

        public static String Format(String format, params object[] args)
        {
            ms.Clear();
            ms.AppendFormat(format, args);
            string strRet = ms.ToString();
            ms.Clear();
            return strRet;
        }
        public static string RemoveSubStr(this string str, string substr)
        {
            int i = str.IndexOf(substr);
            if (i != -1)
            {
                return str.Remove(i, substr.Length);
            }

            return str;
        }

    }

    #endregion

}
