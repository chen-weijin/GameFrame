
public enum LongUtil
{
    None,
    W,
    E,
    WE,
}

public static class LongExtension
{
   
    public static string FormatBigMoneyWithoutUtil(this long value,ref LongUtil longUtil,bool isUserEnglishChar = false)
    {
        longUtil = LongUtil.None;
        if (value < 10000000)
        {
            longUtil = LongUtil.None;
        }
        else if (value < 100000000)
        {
            longUtil = LongUtil.W;
        }
        else if(value < 1000000000000)
        {
            longUtil = LongUtil.E;
        }
        else if(value < 10000000000000000)
        {
            longUtil = LongUtil.WE;
        }
        return NumberFormatUtil.FormatMoney(value,isUserEnglishChar,false);
    }
}