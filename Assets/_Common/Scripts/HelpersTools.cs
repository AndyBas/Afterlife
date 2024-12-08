using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpersTools
{
    public static T PickRandomElementInList<T>(List<T> list)
    {
        return list[GetRandomIndexList(list)];
    }

    public static int GetRandomIndexList<T>(List<T> list)
    {
        return GetRandomIndex(list.Count);
    }
    public static T PickRandomElementInArray<T>(T[] array)
    {
        return array[GetRandomIndexArray(array)];
    }

    public static int GetRandomIndexArray<T>(T[] list)
    {
        return GetRandomIndex(list.Length);
    }

    public static int GetRandomIndex(int max, int min = 0)
    {
        return UnityEngine.Random.Range(min, max);  
    }

    public static float GetRandomFloat(float min = 0f, float max = 1f)
    {
        return UnityEngine.Random.Range(min, max);
    }
    public static int GetRandomInt(int min = 0, int max = 2)
    {
        return Mathf.RoundToInt(UnityEngine.Random.Range(min, max));
    }

    public static float GetRandomFloatFromVec(Vector2 vec)
    {
        return GetRandomFloat(vec.x, vec.y);
    }

    public static int GetRandomIntFromVec(Vector2 vec)
    {
        return Mathf.RoundToInt(GetRandomFloatFromVec(vec));
    }

    public static float GetClampedFloatFromVec(float valToClamp, Vector2 minMaxClampingVec)
    {
        return Mathf.Clamp(valToClamp, minMaxClampingVec.x, minMaxClampingVec.y);
    }

    public static float GetLerpedFloatFromVec(Vector2 vec, float ratio)
    {
        return Mathf.Lerp(vec.x, vec.y, ratio);
    }
}
