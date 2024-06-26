using System;
using System.IO;
using UnityEngine;

namespace NGame
{
    public class BytesUtils
    {
        public static int ReadInt32(byte[] bytes,ref int index)
        {
            int value = BitConverter.ToInt32(bytes, index);
            index += 4;
            return value;
        }
        public static float ReadFloat(byte[] bytes, ref int index)
        {
            float value = BitConverter.ToSingle(bytes, index);
            index += 4;
            return value;
        }
        public static void ReadVector3(byte[] bytes,out Vector3 value ,ref int index)
        {
            value.x= BitConverter.ToSingle(bytes, index);
            index += 4;
            value.y = BitConverter.ToSingle(bytes, index);
            index += 4;
            value.z = BitConverter.ToSingle(bytes, index);
            index += 4;
        }
        public static string ReadString(byte[] bytes, ref int index)
        {
            int len = BitConverter.ToInt32(bytes, index);
            index += 4;
            if (len == 0)
            {
                return "";
            }
            string value = System.Text.Encoding.UTF8.GetString(bytes, index,len);
            index+= len;
            return value;
        }

        public static void Write(Stream stream,int value)
        {
            stream.Write(BitConverter.GetBytes(value));
        }
        public static void Write(Stream stream, Vector3 value)
        {
            stream.Write(BitConverter.GetBytes(value.x));
            stream.Write(BitConverter.GetBytes(value.y));
            stream.Write(BitConverter.GetBytes(value.z));
        }
        public static void Write(Stream stream, float value)
        {
            stream.Write(BitConverter.GetBytes(value));
        }
        public static void Write(Stream stream, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                Write(stream,(int) 0);
                return;
            }
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(value);
            stream.Write(BitConverter.GetBytes(bytes.Length));
            stream.Write(bytes);
        }
    }
}
