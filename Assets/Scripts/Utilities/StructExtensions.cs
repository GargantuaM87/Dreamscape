using System;
using System.Collections.Generic;
using System.Linq;

public static class StructExtensions
{
    public static Dictionary<TKey, TValue> Shuffle<TKey, TValue>(this Dictionary<TKey, TValue> source)
    {
        Random r = new Random();
        return source.OrderBy(x => r.Next())
            .ToDictionary(item => item.Key, item => item.Value);
    }

    public static void Shuffle<T>(this IList<T> ts) {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i) {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }
}


