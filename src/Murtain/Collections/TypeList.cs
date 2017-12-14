using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Murtain.Collections
{

    public class TypeList : TypeList<object>, ITypeList
    {
    }


    public class TypeList<TBaseType> : ITypeList<TBaseType>
    {

        public int Count { get { return typeList.Count; } }


        public bool IsReadOnly { get { return false; } }


        public Type this[int index]
        {
            get { return typeList[index]; }
            set
            {
                CheckType(value);
                typeList[index] = value;
            }
        }

        private readonly List<Type> typeList;


        public TypeList()
        {
            typeList = new List<Type>();
        }

        public void Add<T>() where T : TBaseType
        {
            typeList.Add(typeof(T));
        }

        public void Add(Type item)
        {
            CheckType(item);
            typeList.Add(item);
        }

        public void Insert(int index, Type item)
        {
            typeList.Insert(index, item);
        }

        public int IndexOf(Type item)
        {
            return typeList.IndexOf(item);
        }

        public bool Contains<T>() where T : TBaseType
        {
            return Contains(typeof(T));
        }

        public bool Contains(Type item)
        {
            return typeList.Contains(item);
        }

        public void Remove<T>() where T : TBaseType
        {
            typeList.Remove(typeof(T));
        }

        public bool Remove(Type item)
        {
            return typeList.Remove(item);
        }

        public void RemoveAt(int index)
        {
            typeList.RemoveAt(index);
        }

        public void Clear()
        {
            typeList.Clear();
        }

        public void CopyTo(Type[] array, int arrayIndex)
        {
            typeList.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Type> GetEnumerator()
        {
            return typeList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return typeList.GetEnumerator();
        }

        private static void CheckType(Type item)
        {
            if (!typeof(TBaseType).IsAssignableFrom(item))
            {
                throw new ArgumentException("Given item is not type of " + typeof(TBaseType).AssemblyQualifiedName, "item");
            }
        }
    }
}
