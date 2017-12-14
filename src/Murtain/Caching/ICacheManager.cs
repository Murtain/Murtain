using System;
using System.Collections.Generic;
using System.Text;

namespace Murtain.Caching
{
    public interface ICacheManager
    {
        object Get(string key);

        T Get<T>(string key, Func<T> invoker = null);

        IDictionary<string, object> MultiGet(IEnumerable<string> keys);

        IEnumerable<T> MultiGet<T>(IEnumerable<string> keys);

        IEnumerable<T> MultiGet<T>(IEnumerable<string> keys, Func<IEnumerable<string>, IEnumerable<T>> invoker);

        void Set(string key, object value);

        void Set(string key, object value, DateTime invalidatedTime);

        void Set(string key, object value, TimeSpan invalidatedSpan);

        T Modify<T>(string key, Func<T, T> invoker);

        T Modify<T>(string key, Func<T, T> invoker, DateTime expireAt);

        T Modify<T>(string key, Func<T, T> invoker, TimeSpan validFor);

        T Retrive<T>(string key, Func<T> invoker);

        T Retrive<T>(string key, Func<T> invoker, DateTime invalidatedTime);

        T Retrive<T>(string key, Func<T> invoker, TimeSpan invalidatedSpan);

        T Lengthen<T>(string key, Func<T, Tuple<T, bool>> lengthenInvoker, Func<T> initInvoker, DateTime expireAt);

        T Lengthen<T>(string key, Func<T, Tuple<T, bool>> lengthenInvoker, Func<T> initInvoker, TimeSpan validFor);

        void Remove(string key);

        void FlushAll();

        #region Increment、Decrement

        void Increment(string key, int delta);

        void Increment(string key, int delta, DateTime expiresAt);

        void Increment(string key, int delta, TimeSpan validFor);

        void Increment(string key, int defaultValue, int delta);

        void Increment(string key, int defaultValue, int delta, DateTime expiresAt);
        
        void Increment(string key, int defaultValue, int delta, TimeSpan validFor);

        void Decrement(string key, int delta);
       
        void Decrement(string key, int delta, DateTime expiresAt);
       
        void Decrement(string key, int delta, TimeSpan validFor);
        
        void Decrement(string key, int defaultValue, int delta);
        
        void Decrement(string key, int defaultValue, int delta, DateTime expiresAt);

        void Decrement(string key, int defaultValue, int delta, TimeSpan validFor);

        #endregion
    }
}
