// DateEx.cs
// Author: shihongyang <shihongyang@weile.com>
// Data: 2019/5/7
using System;
using System.Globalization;
using UnityEngine;

namespace NGame
{
    public static class DateHelper
    {
        /// <summary>
        /// 获取当前时间戳，单位毫秒
        /// </summary>
        /// <returns></returns>
        private static DateTime _dateTimeFrom = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// 北京时间起始时间为1970年1月1日8时0分0秒
        /// </summary>
        private static DateTime _dtUnix = new DateTime(1970, 1, 1, 8, 0, 0);

        public static long GetMilliSeconds()
        {
            return (DateTime.UtcNow.Ticks - _dateTimeFrom.Ticks) / 10000;
        }

        /// <summary>
        /// 根据秒单位的时间戳 转为DateTime
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <returns></returns>
        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            DateTime dtDateTime = _dateTimeFrom.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        /// <summary>
        /// 获取当前周几
        ///  Sunday = 0,
        // Monday = 1,
        // Tuesday = 2,
        // Wednesday = 3,
        // Thursday = 4,
        // Friday = 5,
        // Saturday = 6
        /// </summary>
        /// <returns></returns>
        public static int GetCurWeekDay()
        {
            return (int)DateTime.UtcNow.DayOfWeek;
        }

        /// <summary>
        /// 获取时间戳 属于周几
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <returns></returns>
        public static int GetTimeStampWeekDay(long unixTimeStamp)
        {
            DateTime time = UnixTimeStampToDateTime(unixTimeStamp);
            return (int)time.DayOfWeek;
        }


        /// <summary>
        /// 获取当前时间戳，单位秒
        /// </summary>
        /// <returns></returns>
        public static long GetSeconds()
        {
            return GetMilliSeconds() / 1000;
        }

        /// <summary>
        /// 把秒数转成分钟数格式，GetFormatTimeMS(60) -> 01:00
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static string GetFormatTimeMS(int seconds)
        {
            int minute = (int)Math.Floor(seconds / 60.0f);
            var second = seconds % 60;
            return string.Format("{0:D2}:{1:D2}", minute, second);
        }

        /// <summary>
        /// 把秒数转成时分秒，GetFormatTimeHMS(60) -> 00:01:00
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static string GetFormatTimeHMS(long seconds)
        {
            int hour = (int)Math.Floor(seconds / 3600.0f);
            int minute = (int)Math.Floor(seconds % 3600 / 60.0f);
            var second = seconds % 60;
            return string.Format("{0:D2}:{1:D2}:{2:D2}", hour, minute, second);
        }

        /// <summary>
        /// 把秒数转成时分，GetFormatTimeHM -> 00:01
        /// </summary>
        public static string GetFormatTimeHM(long seconds)
        {
            int hour = (int)Math.Floor(seconds / 3600.0f);
            int minute = (int)Math.Floor(seconds % 3600 / 60.0f);
            return string.Format("{0:D2}:{1:D2}", hour, minute);
        }

        /// <summary>
        /// 获取当前规范化的时间，GetCurFormatTime("/") -> 2021/8/3 
        /// </summary>
        /// <param name="format">格式化参数</param>
        /// <returns></returns>
        public static string GetCurFormatTime(string format)
        {
            var f = string.Format("yyyy{0}MM{0}dd", format);
            return DateTime.UtcNow.ToString(f);
        }

        /// <summary>
        /// 返回结果包含秒，如：GetCurFormatTime2("/", ":") -> 2021/8/3 17:50:22
        /// </summary>
        /// <param name="format"></param>
        /// <param name="format2"></param>
        /// <returns></returns>
        public static string GetCurFormatTime2(string format, string format2)
        {
            var f = string.Format("yyyy{0}MM{0}dd HH{1}mm{1}ss", format, format2);
            return DateTime.UtcNow.ToString(f);
        }


        /// <summary>
        /// 使用指定的时间戳获得规范化时间格式，如“2020-12-11”
        /// </summary>
        /// <param name="format">格式化如："-" ，"/"</param>
        /// <param name="timeStamp">指定时间戳，单位毫秒</param>
        /// <returns></returns>
        public static string GetFormatTimeByTimeStamp(string format, long timeStamp)
        {
            var startTime = TimeZoneInfo.ConvertTimeFromUtc(new System.DateTime(1970, 1, 1), TimeZoneInfo.Local);
            DateTime dt = startTime.AddMilliseconds(timeStamp);
            var f = string.Format("yyyy{0}MM{0}dd", format);
            return dt.ToString(f);
        }

        /// <summary>
        /// 使用指定的时间戳获得规范化时间格式，如GetFormatTimeByTimeStamp2("-", ":", 1632323232) -> “2020-12-11 17:50:22”
        /// </summary>
        /// <param name="format"></param>
        /// <param name="format2"></param>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static string GetFormatTimeByTimeStamp2(string format, string format2, long timeStamp)
        {
            var startTime = TimeZoneInfo.ConvertTimeFromUtc(new System.DateTime(1970, 1, 1), TimeZoneInfo.Local);
            DateTime dt = startTime.AddSeconds(timeStamp);
            var f = string.Format("yyyy{0}MM{0}dd HH{1}mm{1}ss", format, format2);
            return dt.ToString(f);
        }
        /// <summary>
        /// 使用指定的时间戳获得规范化时间格式，如GetFormatTimeByTimeStamp2("-", ":", 1632323232) -> “2020-12-11 17:50:22”
        /// </summary>
        /// <param name="format"></param>
        /// <param name="format2"></param>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime GetDateTimeByTimeStamp(long timeStamp)
        {
            var startTime = TimeZoneInfo.ConvertTimeFromUtc(new System.DateTime(1970, 1, 1), TimeZoneInfo.Local);
            DateTime dt = startTime.AddMilliseconds(timeStamp);
            return dt;
        }
        /// <summary>
        /// 如GetTimeSpanByFormat("2021/8/3 17:50:22")
        /// </summary>
        /// <param name="format">时间格式：2021/8/3 17:50:22</param> 
        /// <returns></returns>
        public static TimeSpan GetTimeSpanByFormat(string format)
        {
            DateTimeFormatInfo dtFormat = new System.Globalization.DateTimeFormatInfo();
            dtFormat.ShortDatePattern = "yyyy/MM/dd HH:mm:ss";
            DateTime dt = Convert.ToDateTime(format, dtFormat);
            DateTime startTime = new DateTime(1970, 1, 1, 8, 0, 0);
            return dt - startTime;
        }

        /// <summary>
        /// 通过时间格式返回时间戳(秒)，如GetTimeSpanByFormat("2021/8/3 17:50:22") -> 1607699944
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public static double GetSecondByFormat(string format)
        {
            var s = GetTimeSpanByFormat(format);
            return s.TotalSeconds;
        }

        /// <summary>
        /// 通过时间格式返回时间戳(毫秒)，如GetTimeSpanByFormat("2021/8/3 17:50:22") -> 1607699944000
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public static double GetMillSecondByFormat(string format)
        {
            var s = GetTimeSpanByFormat(format);
            return s.TotalMilliseconds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="systemTime"></param>
        /// <returns></returns>
        //public static double GetSecondsFromSystemTime(MsgSystemTime systemTime)
        //{
        //    string format = string.Format("{0}/{1}/{2} {3}:{4}:{5}", systemTime.wYear, systemTime.wMonth, systemTime.wDay, systemTime.wHour, systemTime.wMinute, systemTime.wSecond);
        //    return GetSecondByFormat(format);
        //}

        /// <summary>
        /// 获取当前年份
        /// </summary>
        /// <returns></returns>
        /// 例: GetCurYear() 输出2021
        public static int GetCurYear()
        {
            return DateTime.UtcNow.Year;
        }

        /// <summary>
        /// 获取当前月份
        /// </summary>
        /// <returns></returns>
        /// 例: GetCurMonth() 输出8
        public static int GetCurMonth()
        {
            return DateTime.UtcNow.Month;
        }

        /// <summary>
        /// 获取当前是几日
        /// </summary>
        /// <returns></returns>
        /// 例: GetCurDate() 输出4
        public static int GetCurDate()
        {
            return DateTime.UtcNow.Day;
        }

        /// <summary>
        /// 获取当前时间在当天的秒数
        /// </summary>
        /// <returns></returns>
        /// 例: GetCurDaySeconds() 输出43233
        public static int GetCurDaySeconds()
        {
            return DateTime.UtcNow.Hour * 3600 + DateTime.UtcNow.Minute * 60 + DateTime.UtcNow.Second;
        }

        /// <summary>
        /// 当前时间距离目标时间还剩多长时间，文本显示，如：1天、20小时、2分钟
        /// </summary>
        /// <param name="targetTimeStamp">目标时间戳，单位秒</param>
        /// <returns></returns>
        /// 例:GetDateByEndTimeDay(1628061555) 输出 5分钟
        public static string GetLastTimeDes(long targetTimeStamp)
        {
            var curTime = GetSeconds();

            return GetTimeDes(targetTimeStamp, curTime);
        }
        /// <summary>
        /// 当前时间距离目标时间已经过去多长时间，文本显示，如：xx天前、xx小时前、xx分钟分钟前
        /// </summary>
        /// <param name="targetTimeStamp"></param>
        /// <returns></returns>
        public static string GetFirstTimeDes(long curTime, long targetTimeStamp)
        {
            return GetTimeDesNew(curTime, targetTimeStamp);
        }

        private static string GetTimeDes(long startTime, long endTime)
        {
            double t = startTime - endTime;


            if (t >= 86400)
            {
                // 大于1天
                return Math.Ceiling(t / 86400) + "天";
            }
            else if (t >= 3600)
            {
                // 小时
                return Math.Ceiling(t / 3600) + "小时";
            }
            else if (t >= 60)
            {
                // 分钟
                return Math.Ceiling(t / 60) + "分钟";
            }

            // 不能小于0
            var st = Math.Max(0, t);
            return st + "秒";
        }

        private static string GetTimeDesNew(long startTime, long endTime)
        {
            double t = startTime - endTime;

            if (t >= 86400)
            {
                // 大于1天
                return Math.Ceiling(t / 86400) + "天前";
            }
            else if (t >= 3600)
            {
                // 小时
                return Math.Ceiling(t / 3600) + "小时前";
            }
            else if (t >= 60)
            {
                // 分钟
                return Math.Ceiling(t / 60) + "分钟前";
            }

            return "刚刚";
        }

        /// <summary>
        /// 根据传进来的DateTime获取时间戳
        /// </summary>
        /// <returns></returns>
        public static long GetMilliSecondsByDateTime(DateTime targetTime)
        {
            return Convert.ToInt64((targetTime - _dtUnix).TotalSeconds);
        }

        /// <summary>
        /// 防止玩家快速点击，限制了时间间隔默认0.5f
        /// </summary>
        /// <param name="callback"></param>
        public static float clickInterval = 0.5f;
        private static long _clickLastTime = 0;
        public static void AvoidQuickClick(Action callback, float clickInterval = 0.5f)
        {
            DateHelper.clickInterval = clickInterval;
            var clicktTime = GetMilliSeconds();
            if (clicktTime - _clickLastTime < clickInterval * 1000)
            {
                return;
            }
            _clickLastTime = clicktTime;
            callback.Invoke();
        }

        public static string GetPropExpirationTimeDes(long time)
        {
            double t = time;

            //-1代表是永久有效期
            if (t == -1)
            {
                return "永久";
            }

            return Math.Ceiling(t / 86400) + "天";
        }

        /// <summary>
        /// 使用指定的时间戳获得规范化时间格式，如“2020-12-11”
        /// </summary>
        /// <param name="format">格式化如："-" ，"/"</param>
        /// <param name="timeStamp">指定时间戳，单位毫秒</param>
        /// <returns></returns>
        public static string GetFormatTimeByTimeStamp3(string format, long timeStamp)
        {
            var startTime = TimeZoneInfo.ConvertTimeFromUtc(new System.DateTime(1970, 1, 1), TimeZoneInfo.Local);
            DateTime dt = startTime.AddMilliseconds(timeStamp);
            var f = string.Format("MM{0}dd", format);
            return dt.ToString(format);
        }
    }
}
