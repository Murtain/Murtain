using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Murtain.Caching
{
    public class MemoryCacheManager : ICacheManager
    {
        private readonly IMemoryCache memoryCache;

        public MemoryCacheManager()
        {
            memoryCache = new MemoryCache(new MemoryCacheOptions());
        }

        object ICacheManager.Get(string key)
        {
            var obj = memoryCache.Get(key);
            return obj;
        }
        T ICacheManager.Get<T>(string key, Func<T> invoker)
        {
            var obj = memoryCache.Get(key);

            if (obj == null)
            {
                if (invoker != null)
                {
                    T invokerResult = invoker();

                    if (invokerResult != null)
                    {
                        return invokerResult;
                    }
                }

                return default(T);
            }
            else
            {
                return (T)obj;
            }
        }

        IDictionary<string, object> ICacheManager.MultiGet(IEnumerable<string> keys)
        {
            return MultiGet(keys);
        }

        IEnumerable<T> ICacheManager.MultiGet<T>(IEnumerable<string> keys)
        {
            return MultiGet(keys).Select(t => (T)t.Value);
        }

        IEnumerable<T> ICacheManager.MultiGet<T>(IEnumerable<string> keys, Func<IEnumerable<string>, IEnumerable<T>> invoker)
        {
            IDictionary<string, object> dict = MultiGet(keys.Distinct());
            IEnumerable<T> hitedT = dict.Select(t => (T)t.Value);

            int keyCount = keys.Count();
            int hitedTCount = hitedT.Count();

            if (keyCount == hitedTCount)
            {
                return hitedT;
            }
            else
            {
                IEnumerable<string> hitedKeys = dict.Select(t => t.Key);
                IEnumerable<string> missedKeys = keys.Where(t => !hitedKeys.Contains(t));

                if (missedKeys.Count() == 0)
                {
                    return hitedT;
                }
                else
                {
                    return hitedT.Concat(invoker(missedKeys));
                }
            }
        }

        IDictionary<string, object> MultiGet(IEnumerable<string> keys)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            foreach (var key in keys)
            {
                var obj = memoryCache.Get(key);

                if (obj != null)
                {
                    dict.Add(key, obj);
                }
            }

            return dict;
        }

        void ICacheManager.Set(string key, object value)
        {
            Set(key, value, new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.NeverRemove));
        }

        void ICacheManager.Set(string key, object value, DateTime invalidatedTime)
        {
            Set(key, value, invalidatedTime);
        }

        void ICacheManager.Set(string key, object value, TimeSpan invalidatedSpan)
        {
            Set(key, value, DateTime.Now.Add(invalidatedSpan));
        }

        void Set(string key, object value, DateTime invalidatedTime)
        {
            if (key.Length == 0 || value == null)
            {
                return;
            }

            memoryCache.Set(key, value, invalidatedTime);
        }
        void Set(string key, object value, MemoryCacheEntryOptions memoryCacheOptions)
        {
            if (key.Length == 0 || value == null)
            {
                return;
            }

            memoryCache.Set(key, value, memoryCacheOptions);
        }

        T ICacheManager.Modify<T>(string key, Func<T, T> invoker)
        {
            return Modify<T>(key, invoker, new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.NeverRemove));
        }

        T ICacheManager.Modify<T>(string key, Func<T, T> invoker, DateTime expireAt)
        {
            return Modify<T>(key, invoker, expireAt);
        }

        T ICacheManager.Modify<T>(string key, Func<T, T> invoker, TimeSpan validFor)
        {
            return Modify<T>(key, invoker, DateTime.Now.Add(validFor));
        }

        T Modify<T>(string key, Func<T, T> invoker, DateTime expireAt)
        {
            if (key.Length == 0)
            {
                return default(T);
            }

            lock (key)
            {
                var get = memoryCache.Get(key);

                if (get == null)
                {
                    return default(T);
                }

                T value = invoker((T)get);

                Set(key, value, expireAt);
                return value;
            }
        }

        T Modify<T>(string key, Func<T, T> invoker, MemoryCacheEntryOptions memoryCacheOptions)
        {
            if (key.Length == 0)
            {
                return default(T);
            }

            lock (key)
            {
                var get = memoryCache.Get(key);

                if (get == null)
                {
                    return default(T);
                }

                T value = invoker((T)get);

                Set(key, value, memoryCacheOptions);
                return value;
            }
        }

        T ICacheManager.Retrive<T>(string key, Func<T> invoker)
        {
            return Retrive<T>(key, invoker, new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.NeverRemove));
        }

        T ICacheManager.Retrive<T>(string key, Func<T> invoker, DateTime invaliddatedTime)
        {
            return Retrive<T>(key, invoker, invaliddatedTime);
        }

        T ICacheManager.Retrive<T>(string key, Func<T> invoker, TimeSpan invalidatedSpan)
        {
            return Retrive<T>(key, invoker, DateTime.Now.Add(invalidatedSpan));
        }

        T Retrive<T>(string key, Func<T> invoker, DateTime invalidatedTime)
        {
            if (key.Length == 0)
            {
                return invoker();
            }

            var cached = memoryCache.Get(key);

            if (cached == null)
            {
                lock (key)
                {
                    cached = memoryCache.Get(key);

                    if (cached != null)
                    {
                        return (T)cached;
                    }

                    T obj = invoker();

                    if (obj == null)
                    {
                        return default(T);
                    }

                    Set(key, obj, invalidatedTime);

                    return obj;
                }
            }
            else
            {
                return (T)cached;
            }
        }

        T Retrive<T>(string key, Func<T> invoker, MemoryCacheEntryOptions memoryCacheOptions)
        {
            if (key.Length == 0)
            {
                return invoker();
            }

            var cached = memoryCache.Get(key);

            if (cached == null)
            {
                lock (key)
                {
                    cached = memoryCache.Get(key);

                    if (cached != null)
                    {
                        return (T)cached;
                    }

                    T obj = invoker();

                    if (obj == null)
                    {
                        return default(T);
                    }

                    Set(key, obj, memoryCacheOptions);

                    return obj;
                }
            }
            else
            {
                return (T)cached;
            }
        }

        T ICacheManager.Lengthen<T>(string key, Func<T, Tuple<T, bool>> lengthenInvoker, Func<T> initInvoker, DateTime expireAt)
        {
            throw new NotImplementedException();
        }

        T ICacheManager.Lengthen<T>(string key, Func<T, Tuple<T, bool>> lengthenInvoker, Func<T> initInvoker, TimeSpan validFor)
        {
            throw new NotImplementedException();
        }

        void ICacheManager.Remove(string key)
        {
            if (key.Length == 0)
            {
                return;
            }

            memoryCache.Remove(key);
        }

        void ICacheManager.FlushAll()
        {
            throw new NotImplementedException();
        }

        void ExpireCallBack(string key, object obj)
        {

        }

        void ICacheManager.Increment(string key, int delta)
        {
            Increment(key, null, delta, new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.NeverRemove));
        }

        void ICacheManager.Increment(string key, int delta, DateTime expiresAt)
        {
            Increment(key, null, delta, expiresAt);
        }

        void ICacheManager.Increment(string key, int delta, TimeSpan validFor)
        {
            Increment(key, null, delta, DateTime.Now.Add(validFor));
        }

        void ICacheManager.Increment(string key, int defaultValue, int delta)
        {
            Increment(key, defaultValue, delta, new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.NeverRemove));
        }

        void ICacheManager.Increment(string key, int defaultValue, int delta, DateTime expiresAt)
        {
            Increment(key, defaultValue, delta, expiresAt);
        }

        void ICacheManager.Increment(string key, int defaultValue, int delta, TimeSpan validFor)
        {
            Increment(key, defaultValue, delta, DateTime.Now.Add(validFor));
        }

        void Increment(string key, int? defaultValue, int delta, DateTime expiresAt)
        {
            var current = ((ICacheManager)this);
            var cached = current.Get(key);

            if (cached == null)
            {
                if (defaultValue == null)
                {
                    return;
                }
                else
                {
                    cached = (int)defaultValue;
                }
            }
            else
            {
                cached = (int)cached + delta;
            }

            Set(key, cached, expiresAt);
        }
        void Increment(string key, int? defaultValue, int delta, MemoryCacheEntryOptions memoryCacheOptions)
        {
            var current = ((ICacheManager)this);
            var cached = current.Get(key);

            if (cached == null)
            {
                if (defaultValue == null)
                {
                    return;
                }
                else
                {
                    cached = (int)defaultValue;
                }
            }
            else
            {
                cached = (int)cached + delta;
            }

            Set(key, cached, memoryCacheOptions);
        }

        void ICacheManager.Decrement(string key, int delta)
        {
            Decrement(key, null, delta, new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.NeverRemove));
        }

        void ICacheManager.Decrement(string key, int delta, DateTime expiresAt)
        {
            Decrement(key, null, delta, expiresAt);
        }

        void ICacheManager.Decrement(string key, int delta, TimeSpan validFor)
        {
            Decrement(key, null, delta, DateTime.Now.Add(validFor));
        }

        void ICacheManager.Decrement(string key, int defaultValue, int delta)
        {
            Decrement(key, defaultValue, delta, new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.NeverRemove));
        }

        void ICacheManager.Decrement(string key, int defaultValue, int delta, DateTime expiresAt)
        {
            Decrement(key, defaultValue, delta, expiresAt);
        }

        void ICacheManager.Decrement(string key, int defaultValue, int delta, TimeSpan validFor)
        {
            Decrement(key, defaultValue, delta, DateTime.Now.Add(validFor));
        }
        void Decrement(string key, int? defaultValue, int delta, DateTime expiresAt)
        {
            var current = ((ICacheManager)this);
            var cached = current.Get(key);

            if (cached == null)
            {
                if (defaultValue == null)
                {
                    return;
                }
                else
                {
                    cached = (int)defaultValue;
                }
            }
            else
            {
                cached = (int)cached - delta;
                cached = (int)cached > 0 ? cached : 0;
            }

            Set(key, cached, expiresAt);
        }
        void Decrement(string key, int? defaultValue, int delta, MemoryCacheEntryOptions memoryCacheOptions)
        {
            var current = ((ICacheManager)this);
            var cached = current.Get(key);

            if (cached == null)
            {
                if (defaultValue == null)
                {
                    return;
                }
                else
                {
                    cached = (int)defaultValue;
                }
            }
            else
            {
                cached = (int)cached - delta;
                cached = (int)cached > 0 ? cached : 0;
            }

            Set(key, cached, memoryCacheOptions);
        }


        
    }
}
