

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using NGame;
using UnityEngine;

namespace NGame
{
    public static class CJSHelper
    {
        public static string URLKEY = "14132ef17af12643ade5bf07134d7810870141";
        //[MenuItem("测试/MD5", false, 100)]
        //private static void DoIt()
        //{
        //    var st = CJSHelper.JSCryptStr("1abf04fa3fB,fTKsQGMKyog3DJeo71O-mvjEmHjvFS7ecxDAMunUHPuopa",
        //        "14182a8a5e2cb650ee1fdc5d4a85205a429141",
        //        false,
        //        0);
        //    //var st = CJSHelper.JSCryptStr("abcdefghij",
        //    //    "14182a8a5e2cb650ee1fdc5d4a85205a429141",
        //    //    true,
        //    //    0);
        //    NGDebug.Log(st);
        //}

        public static string JSCryptStr(string strData,
            string strKey)
        {
            bool bEncode = true;
            int nExpiry = 0;
            if (bEncode)
            {
                string str = CryptStr(strData, strKey, bEncode, nExpiry);
                str = str.Replace('/', '-');
                str = str.Replace('+', ',');
                return str;
            }

            string strSrc = strData;
            strSrc = strSrc.Replace('-', '/');
            strSrc = strSrc.Replace(',', '+');
            return CryptStr(strSrc, strKey, bEncode, nExpiry);
        }


        private static string CryptStr(string strData, string strKeyValue, bool bEncode, int dwExpiry/*=0*/ )
        {
            string strKey = MakeMd5(strKeyValue);
            string strKeyA = MakeMd5(strKey.Substring(0, 16));
            string strKeyB = MakeMd5(strKey.Substring(16, 16));
            string strKeyC;

            long lTime = DateHelper.GetSeconds();
            string szBuff = string.Format("0.0001 {0}", lTime);
            if (bEncode)
                strKeyC = MakeMd5(szBuff).Substring(22);
            else
                strKeyC = strData.Substring(0, 10);

            string strDataTmp;
            if (bEncode)
            {
                long len = dwExpiry != 0 ? dwExpiry + lTime : 0;
                var str = len.ToString().PadLeft(10, '0');
                strDataTmp = str + MakeMd5(strData + strKeyB).Substring(0, 16) + strData;
            }
            else if (strData.Length >= 10)
            {
                string strBase64 = strData.Substring(10);
                int nLeft = strBase64.Length % 4;
                if (nLeft != 0)
                    strBase64 = strBase64 + "".PadRight(4 - nLeft, '=');

                var byteBase64_1 = Encoding.UTF8.GetBytes(strBase64);
                strDataTmp = Convert.ToBase64String(byteBase64_1);
            }
            else
                return "";

            int string_lenght = strDataTmp.Length;
            string strcryptkey = strKeyA + MakeMd5(strKeyA + strKeyC); ;
            int ncryptkeyLen = strcryptkey.Length;

            byte[] szBox = new byte[256];
            byte[] szRandKey = new byte[256];
            for (int i = 0; i < 256; ++i)
            {
                szBox[i] = (byte)i;
                szRandKey[i] = (byte)strcryptkey[i % ncryptkeyLen];
            }

            int j = 0;
            byte tmp;
            for (int i = 0; i < 256; ++i)
            {
                j = (j + szBox[i] + szRandKey[i]) % 256;
                tmp = szBox[i];
                szBox[i] = szBox[j];
                szBox[j] = tmp;
            }

            byte[] vResult = new byte[string_lenght];
            j = 0;
            int a = 0;
            for (int i = 0; i < string_lenght; ++i)
            {
                a = (a + 1) % 256;
                j = (j + szBox[a]) % 256;
                tmp = szBox[a];
                szBox[a] = szBox[j];
                szBox[j] = tmp;

                int nTemp = (strDataTmp[i]) ^ szBox[(szBox[a] + szBox[j]) % 256];
                vResult[i] = (byte)(nTemp);
            }

            if (bEncode)
            {
                string strBase64 = Convert.ToBase64String(vResult);
                int nPos = strBase64.IndexOf('=');
                if (nPos != -1)
                    strBase64 = strBase64.Substring(0, nPos);
                return strKeyC + strBase64;
            }
            else
            {
                if (vResult.Length < 10)
                    return "";

                string result = Encoding.UTF8.GetString(vResult, 0, 10);
                long lPre = long.Parse(result);
                if (lPre == 0 || (lPre - lTime > 0))
                {
                    string strRet = Encoding.UTF8.GetString(vResult, 26, vResult.Length - 26);
                    string strCheck = Encoding.UTF8.GetString(vResult, 10, 16);
                    if (strCheck == MakeMd5(strRet + strKeyB).Substring(0, 16))
                        return strRet;
                }
                return "";
            }
        }

        private static string MakeMd5(string strText)
        {
            var md5 = MD5.Create();
            byte[] pMd5Value = md5.ComputeHash(Encoding.UTF8.GetBytes(strText));

            const int MD5_PSW_LEN = 16;
            byte[] strResult = new byte[MD5_PSW_LEN * 2];
            for (int i = 0; i < MD5_PSW_LEN; ++i)
            {
                int cValue = pMd5Value[i] >> 4;
                if (cValue < 10)
                    strResult[i * 2] = (byte)('0' + cValue);
                else
                    strResult[i * 2] = (byte)(cValue + 'a' - 10);

                cValue = pMd5Value[i] & 0xf;
                if (cValue < 10)
                    strResult[i * 2 + 1] = (byte)('0' + cValue);
                else
                    strResult[i * 2 + 1] = (byte)(cValue + 'a' - 10);
            }

            return Encoding.UTF8.GetString(strResult);
        }

    }

}
