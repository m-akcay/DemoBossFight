using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class Util
{
    public static float Remap(this float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        value = Mathf.Clamp(value, fromMin, fromMax);
        float normalizedValue = (value - fromMin) / (fromMax - fromMin);
        float remappedValue = toMin + (normalizedValue * (toMax - toMin));
        return remappedValue;
    }
}

public static class DictionaryExtensions
{
    public static List<TKey> GetKeysAsList<TKey, TValue>(this Dictionary<TKey, TValue> dict)
    {
        var keys = new List<TKey>();
        foreach (var kvp in dict)
        {
            keys.Add(kvp.Key);
        }
        return keys;
    }
}

public static class Vector3Extensions
{
    public static Vector3 Abs(this Vector3 vec)
    {
        return new Vector3(Mathf.Abs(vec.x), Mathf.Abs(vec.y), Mathf.Abs(vec.z));
    }

    public static void ReplaceXYZ(ref this Vector3 vec, int xyz0, int xyz1)
    {
        float val = vec[xyz0];
        vec[xyz0] = vec[xyz1];
        vec[xyz1] = val;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetUniform(ref this Vector3 vec, float value)
    {
        vec.x = value;
        vec.y = value;
        vec.z = value;
    }
   
}
