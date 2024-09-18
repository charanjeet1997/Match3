/// <summary>
/// This Class Helps to give extra functionality to List Class
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public static class ListExtensions
{
    private static System.Random rng = new System.Random ();
    /// <summary>
    /// Shuffle the specified list.
    /// </summary>
    /// <param name="list">List.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public static void Shuffle<T> (this IList<T> list) {
        int n = list.Count;
        while (n > 1) {
            n--;
            int k = rng.Next (n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    /// <summary>
    /// Better than regular shuffle.
    /// </summary>
    /// <param name="a">The alpha component.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public static void FYShuffle<T> (this IList<T> a) {
        // Loops through array
        for (int i = a.Count - 1; i > 0; i--) {
            // Randomize a number between 0 and i (so that the range decreases each time)
            int rnd = UnityEngine.Random.Range (0, i);
            // Save the value of the current i, otherwise it'll overwrite when we swap the values
            T temp = a[i];
            // Swap the new and old values
            a[i] = a[rnd];
            a[rnd] = temp;
        }
    }
    public static List<int> GenerateRandom (int count, int min, int max) {
        if (max <= min || count < 0 ||
            (count > max - min && max - min > 0)) {
            throw new ArgumentOutOfRangeException ("Range " + min + " to " + max +
                " (" + ((Int64) max - (Int64) min) + " values), or count " + count + " is illegal");
        }

        HashSet<int> candidates = new HashSet<int> ();

        for (int top = max - count; top < max; top++) {
            if (!candidates.Add (rng.Next (min, top + 1))) {
                candidates.Add (top);
            }
        }

        List<int> result = candidates.ToList ();

        for (int i = result.Count - 1; i > 0; i--) {
            int k = rng.Next (i + 1);
            int tmp = result[k];
            result[k] = result[i];
            result[i] = tmp;
        }
        return result;
    }
    /// <summary>
    /// Sorts the number list (int).
    /// </summary>
    /// <param name="list">List to sort.</param>
    /// <param name="ascending">If set to true; sort in ascending order, else sort in descending order.</param>
    public static void SortNumberList (this List<int> list, bool ascending = true) {
        int temp;
        for (int i = 0; i < list.Count - 1; i++) {
            for (int j = i + 1; j < list.Count; j++) {
                if (ascending) {
                    if (list[i] > list[j]) {
                        temp = list[i];
                        list[i] = list[j];
                        list[j] = temp;
                    }
                } else {
                    if (list[i] < list[j]) {
                        temp = list[i];
                        list[i] = list[j];
                        list[j] = temp;
                    }
                }
            }
        }
    }

    public static List<T> CreateList<T>(this List<T> list)
    {
        if(list == null)
        {
            list = new List<T>();
        }
        return list;
    }

    public static IList<T> Swap<T>(this IList<T> list, int indexA, int indexB)
    {
        T tmp = list[indexA];
        list[indexA] = list[indexB];
        list[indexB] = tmp;
        return list;
    }
    
    public static int Min(this HashSet<int> set)
    {
        if (set == null || set.Count == 0)
        {
            throw new ArgumentException("The HashSet is empty or null.");
        }

        int min = int.MaxValue;
        foreach (int value in set)
        {
            if (value < min)
            {
                min = value;
            }
        }
        return min;
    }

    public static int Max(this HashSet<int> set)
    {
        if (set == null || set.Count == 0)
        {
            throw new ArgumentException("The HashSet is empty or null.");
        }

        int max = int.MinValue;
        foreach (int value in set)
        {
            if (value > max)
            {
                max = value;
            }
        }
        return max;
    }
    
    
    // create a extention that will remove same elements from list
    public static List<T> RemoveDuplicates<T>(this List<T> list)
    {
        List<T> newList = new List<T>();
        foreach (T item in list)
        {
            if (!newList.Contains(item))
            {
                newList.Add(item);
            }
        }
        return newList;
    }
    
    
}