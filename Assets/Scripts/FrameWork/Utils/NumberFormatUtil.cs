using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
/**
 * 格式化数字工具类
 * @author Bughuang
 */
public class NumberFormatUtil
{
    public static string K = "千";
    public static string W = "万";
    public static string E = "亿";
    public static string WE = "万亿";
    public static string Z = "兆";

    public static string EnglishK = "k";
    public static string EnglishW = "w";
    public static string EnglishE = "e";
    public static string EnglishWE = "we";
    public static string EnglishZ = "z";

    public static string BW = "百万";
    public static string KW = "千万";
    public static string[] IntToChineseArray = { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九", "十" };
    public static string IntToChinese(int num)
    {
        if (num / 100 > 0)
        {
            Debug.LogError("暂不支持99以上的表达方式");
            return string.Empty;
        }
        else
        {
            int tenParam = num / 10;
            int digits = num % 10;
            if (tenParam > 0)
            {
                if (tenParam > 1)
                {
                    if (digits > 0)
                        return $"{IntToChineseArray[tenParam]}十{IntToChineseArray[digits]}";
                    else
                        return $"{IntToChineseArray[tenParam]}十";
                }
                else
                {
                    if (digits > 0)
                        return $"十{IntToChineseArray[digits]}";
                    else
                        return $"十";
                }
            }
            else
            {
                return IntToChineseArray[digits];
            }
        }
    }
    public static string GetK(bool isUserEnglishChar, bool hasutil = true, bool isUserEnglishUpperChar = false)
    {
        // 如果不组装单位 直接返回空字符串
        if (!hasutil)
            return "";
        if (isUserEnglishChar)
        {
            return isUserEnglishUpperChar ? EnglishK.ToUpper() : EnglishK;
        }

        return K;
    }

    public static string GetW(bool isUserEnglishChar, bool hasutil = true, bool isUserEnglishUpperChar = false)
    {
        if (!hasutil)
            return "";
        if (isUserEnglishChar)
        {
            return isUserEnglishUpperChar ? EnglishW.ToUpper() : EnglishW;
        }

        return W;
    }

    public static string GetE(bool isUserEnglishChar, bool hasutil = true, bool isUserEnglishUpperChar = false)
    {
        if (!hasutil)
            return "";
        if (isUserEnglishChar)
        {
            return isUserEnglishUpperChar ? EnglishE.ToUpper() : EnglishE;
        }

        return E;
    }
    public static string GetWE(bool isUserEnglishChar, bool hasutil = true, bool isUserEnglishUpperChar = false)
    {
        if (!hasutil)
            return "";
        if (isUserEnglishChar)
        {
            return isUserEnglishUpperChar ? EnglishWE.ToUpper() : EnglishWE;
        }

        return WE;
    }
    public static string GetZ(bool isUserEnglishChar, bool hasutil = true, bool isUserEnglishUpperChar = false)
    {
        if (!hasutil)
            return "";
        if (isUserEnglishChar)
        {
            return isUserEnglishUpperChar ? EnglishZ.ToUpper() : EnglishZ;
        }

        return Z;
    }

    public static string FormatDiamand(decimal value)
    {
        if (value < 0)
        {
            decimal newvalue = -value;
            return "-" + FormatDiamand(newvalue);
        }
        if (value < 100000000)
        {
            return value.ToString();
        }
        else
        {
            decimal n = (value / 100000000);
            string str = n.ToString();
            if (str.Length > 4)
            {
                return n.ToString("0.##") + GetE(false);
            }
            else
            {
                return str + GetE(false);
            }
        }
    }
    /// <summary>
    /// 数字格式转换请使用此接口 大厅/局内都适配此规则
    /// </summary>
    /// <param name="value"></param>
    /// <param name="isBW"></param>
    /// <param name="isK"></param>
    /// <param name="isUserEnglishChar"></param>
    /// <param name="hasutil"></param>
    /// <returns></returns>
    public static string FormatMoney(decimal value, bool isBW = false, bool isK = false, bool isUserEnglishChar = false, bool hasutil = true, bool isUserEnglishUpperChar = false)
    {
        if (value < 0)
        {
            decimal newvalue = -value;
            return "-" + FormatMoney(newvalue, isBW, isK, isUserEnglishChar, hasutil);
        }

        if (value <= 10000)
        {
            if (value < 1000)
                return value.ToString();
            else if (isK)
            {
                decimal n = ((decimal)value / 1000);
                string str = n.ToString();
                if (str.Length > 5)
                    return DecimalToString(n, 2) + GetK(isUserEnglishChar, hasutil, isUserEnglishUpperChar);
                else
                    return str + GetK(isUserEnglishChar, hasutil, isUserEnglishUpperChar);

            }
            return value.ToString();
        }
        else if (value < 100000000)
        {
            decimal n = (value / 10000);

            if (value < 100000)
            {
                string str = n.ToString();
                if (str.Length > 4)
                    return DecimalToString(n, 2) + GetW(isUserEnglishChar, hasutil, isUserEnglishUpperChar);
                else
                    return str + GetW(isUserEnglishChar, hasutil, isUserEnglishUpperChar);
            }
            else if (value < 1000000)
            {
                string str = n.ToString();
                if (str.Length > 4)
                    return DecimalToString(n, 1) + GetW(isUserEnglishChar, hasutil, isUserEnglishUpperChar);
                else
                    return str + GetW(isUserEnglishChar, hasutil, isUserEnglishUpperChar);
            }
            else
            {
                if (isBW)
                {
                    if (value < 10000000)
                    {
                        n = ((decimal)value / 1000000);
                        value = (long)Math.Floor(n);
                        return value.ToString() + BW;
                    }
                    else
                    {
                        n = ((decimal)value / 10000000);
                        value = (long)Math.Floor(n);
                        return value.ToString() + KW;
                    }
                }
                else
                {
                    value = (long)Math.Floor(n);
                    return value.ToString() + GetW(isUserEnglishChar, hasutil, isUserEnglishUpperChar);
                }

            }
        }
        else if (value < 10000000000000)
        {
            decimal n = (value / 100000000);

            if (value < 1000000000)
            {
                string str = n.ToString();
                if (str.Length > 4)
                {
                    return DecimalToString(n, 2) + GetE(isUserEnglishChar, hasutil, isUserEnglishUpperChar);
                }
                else
                {
                    return str + GetE(isUserEnglishChar, hasutil, isUserEnglishUpperChar);
                }
            }
            else if (value < 10000000000)
            {
                string str = n.ToString();
                if (str.Length > 4)
                    return DecimalToString(n, 1) + GetE(isUserEnglishChar, hasutil, isUserEnglishUpperChar);
                else
                    return str + GetE(isUserEnglishChar, hasutil, isUserEnglishUpperChar);
            }
            else
            {
                value = Math.Floor(n);
                return value.ToString() + GetE(isUserEnglishChar, hasutil, isUserEnglishUpperChar);
            }
        }//小于10万亿
        else if (value < 10000000000000000) //小于10000万亿
        {                      //10000000000000000
            decimal n = (value / 1000000000000);
            //100000000000000
            if (value < 100000000000000)
            {
                string str = n.ToString();
                if (str.Length > 4)
                    return DecimalToString(n, 1) + GetWE(isUserEnglishChar, hasutil, isUserEnglishUpperChar);
                else
                    return str + GetWE(isUserEnglishChar, hasutil, isUserEnglishUpperChar);
            }
            else
            {
                value = Math.Floor(n);
                return value.ToString() + GetWE(isUserEnglishChar, hasutil, isUserEnglishUpperChar);
            }
        }
        else
        {
            decimal n = Math.Floor(value / 1000000000000);
            return n.ToString() + GetZ(isUserEnglishChar, hasutil, isUserEnglishUpperChar);
        }
    }

    /** 显示正负号
     */
    public static string FormatProfit(int value)
    {
        return FormatProfit((long)value);
    }

    public static string FormatProfit(long value)
    {
        if (value > 0)
            return ("+" + FormatMoney(value));
        else if (value < 0)
            return ("-" + FormatMoney(Math.Abs(value)));
        else
            return FormatMoney(value);
    }

    public static string FormatPercent(float percent)
    {
        string str = null;
        str = string.Format("{0}%", (int)(percent * 100));
        return str;
    }

    /// <summary>
    /// 数位分组，格式化为xxx,xxx,xxx形式
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public static string FormatBigNumber(long num)
    {
        string str = num.ToString();
        if (num > 1000)
            str = string.Format("{0:0,00}", num);
        return str;
    }

    public static string FormatBigNumber(int num)
    {
        return FormatBigNumber((long)num);
    }
    public static string FormatGameFloatNumber(decimal num)
    {
        if (num >= 1000000000000)
        {
            decimal n = Math.Floor(num / 100000000);
            string str = n.ToString();
            if (n > 1000)
                str = string.Format("{0:0,00}", n);
            return str + E;
        }
        else
        {
            string str = num.ToString();
            if (num > 1000)
                str = string.Format("{0:0,00}", num);
            return str;
        }
    }

    public static string FormatGameBeanToStr(long money, out float val, out string des)
    {
        if (money > 0)
            return ("+" + GameBeanToStr(money,out  val, out  des));
        else if (money < 0)
            return ("-" + GameBeanToStr(money, out val, out des));
        else
            return GameBeanToStr(money, out val, out des);
    }


    //1.人数携带豆豆统计按大厅的豆豆逻辑显示
    //2.八喜牌倍数除了提牌提示窗口带万单位，其他都不带单位
    //3.飘分不带单位
    //4.游戏内窗口的豆豆提示格式
    //①小于等于1万显示具体数字，大于1万，单位使用【万】；1万-10万：2位小数；10万-100万：1位小数；100万-1亿：不保留小数
    //②大于1亿，单位使用【亿】；1亿-10亿：2位小数；10亿-100亿：1位小数；100亿-1万亿：不保留小数
    //③大于等于1万亿，单位使用【万亿】；[1万亿-10万亿)：保留2位小数；10万亿-100万亿：保留1位小数；100万亿以上：不保留小数
    //④大于等于10000万亿~999999兆，单位使用【兆】，1兆=1万亿；10000兆以上，不保留小数
    //⑤大于等于1000000兆时，单位使用【万兆】，不保留小数
    //向下取整
    /// <param name="money">输入值</param>
    /// <param name="val">返回值</param>
    /// <param name="str">返回单位</param>
    static long[] beanArray = new long[] { 10000, 100000000, 1000000000000, 10000000000000000, 1000000000000000000 };
    static string[] beanDesArray = new string[] { "万", "亿", "万亿", "兆", "万兆" };
    public static string GameBeanToStr(long money, out float val, out string des, bool PlusMinusSign = false,bool usePlusSign=true)
    {
        long initMoney = money;
        long num = money > 0 ? money : money * -1;
        val = num;
        des = "";
        for (int i = beanArray.Length - 1; i >= 0; i--)
        {
            var max = beanArray[i];
            if (num >= max)
            {
                des = beanDesArray[i];
                if (max == 1000000000000000000)
                {
                    // 直接显示 万兆
                    val = num / 10000000000000000;
                    break;
                }

                if(max == 10000000000000000)
                {
                    // 直接显示 兆
                    val = num / 1000000000000;
                    break;
                }

                var valid = num / max;
                if (valid.ToString().Length >= 3)
                {
                    // 保证有效显示值的长度三位以上，不然就要保留一位小数
                    val = valid;
                }
                else if (num % max == 0)
                {
                    // 刚好整除
                    val = valid;
                }
                else
                {
                    // 不整除 保留一位小数
                    if((max == 10000 && num >= 10000 && num <= 100000) 
                        || (max == 100000000 && num >= 100000000 && num <= 1000000000)
                        || (max == 1000000000000 && num >= 1000000000000 && num <= 10000000000000))
                    {
                        val = num * 100 / max / 100f;
                    }
                    else
                    {
                        val = num * 10 / max / 10f;
                    }
                }
                break;
            }
        }

        string prefix = string.Empty;
        if (PlusMinusSign)
        {
            if(initMoney > 0&&usePlusSign)
            {
                prefix = "+";
            }
            else if(initMoney < 0)
            {
                prefix = "-";
            }
        }
        return $"{prefix}{val}{des}";
    }

    // 有效位数大于三 带逗号
    public static string GameBeanToStr(long money)
    {
        GameBeanToStr(money, out float val, out string des);

        var str = val.ToString();
        var str1 = "";

        if (str.Contains('.'))
        {
            // 有小数
            str1 = "." + str.Split('.')[1];
            str = str.Split('.')[0];
        }

        int idx = 0;
        for (int i = str.Length - 1; i >= 0; i--)
        {
            idx++;
            if (idx == 3 && i != 0)
            {
                str = str.Insert(i, ",");
                idx = 0;
            }
        }

        return str + str1 + des;
    }

    // 房间专用 1千万到1亿之间需要显示千万
    public static string GameBeanToRoomStr(long money)
    {
        var ret =  GameBeanToStr(money, out float val, out string des);

        var str = val.ToString();

        if (str.Contains('.'))
        {
            str = str.Split('.')[0];
        }

        if(des.Equals(beanDesArray[0]) && str.Length == 4)
        {
            str = (int.Parse(str) / 1000).ToString();
            des = "千万";

            return str + des;
        }

        return ret;
    }

    //  - 星星数显示格式：1000万以下直接显示，超过1000万带单位“万”
    public static string StarToStr(long star) {
        if (star >= 10000000)
        {
            return star / 10000 + "万";
        }
        else
        {
            return star.ToString();
        }
    }

    /// <summary>
    /// decimal 保留指定位小数 且不四舍五入
    /// </summary>
    /// <param name="num">具体数值</param>
    /// <param name="scale">保留小数位数</param>
    /// <returns></returns>
    public static decimal DecimalToString(decimal num, int scale)
    {
        string numToString = num.ToString();

        int index = numToString.IndexOf(".");
        int length = numToString.Length;

        if (index != -1)
        {
            string dm = string.Format("{0}.{1}",
            numToString.Substring(0, index),
            numToString.Substring(index + 1, Mathf.Min(length - index - 1, scale)));
            return System.Convert.ToDecimal(dm);
        }
        else
        {
            string dm = num.ToString();
            return System.Convert.ToDecimal(dm);
        }
    }
}



