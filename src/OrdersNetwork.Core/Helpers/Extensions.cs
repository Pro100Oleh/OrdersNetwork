using OrdersNetwork.Core.Streams;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersNetwork.Core
{
    public static class Extensions
    {
        public static IStream Write(this IStream stream, string format, params object[] args)
        {
            stream.Write(string.Format(format, args));

            return stream;
        }

        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> values)
        {
            return new HashSet<T>(values);
        }

        public static IEnumerable<KeyValuePair<TKey, TValue>> ToKeyValues<TKey, TValue>(this IEnumerable<TValue> values, Func<TValue, TKey> getKey)
        {
            return values.Select(v => new KeyValuePair<TKey, TValue>(getKey(v), v));
        }

        public static IEnumerable<KeyValuePair<TKey, TValue>> ToKeyValues<T, TKey, TValue>(this IEnumerable<T> values, Func<T, TKey> getKey, Func<T, TValue> getValue)
        {
            return values.Select(v => new KeyValuePair<TKey, TValue>(getKey(v), getValue(v)));
        }

        public static IImmutableDictionary<HashValue, T> ToImmutableDictionary<T>(this IEnumerable<T> values)
            where T : ISigned
        {
            return ImmutableDictionary.CreateRange(values.ToKeyValues(v => v.Signature));
        }

        public static IImmutableDictionary<HashValue, T> AddRange<T>(this IImmutableDictionary<HashValue, T> dictionary, IEnumerable<T> values)
            where T : ISigned
        {
            var keyValues = values.ToKeyValues(v => v.Signature);
            return dictionary.AddRange(keyValues);
        }

        public static IImmutableDictionary<HashValue, T> RemoveRange<T>(this IImmutableDictionary<HashValue, T> dictionary, IEnumerable<T> values)
            where T : ISigned
        {
            return dictionary.RemoveRange(values.Select(v => v.Signature));
        }
    }
}
