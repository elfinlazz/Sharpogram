using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace Telegram.TLCore
{
    /**
     * Basic vector type in TL language
     * For working with primitive internal types you might instantiate class TLIntVector, TLStringVector, TLLongVector for
     * vector of integer, strings or long.
     *
     * @param <T> type of elements in vector
     * Based on (@author Korshakov Stepan <me@ex3ndr.com> for Java)
     */
    public class TLVector<T> : TLObject/*, List<T>*/ {

        public static readonly uint CLASS_ID = 0x1cb5c415;
        
       /* private Object destClass = Type.ReflectionOnlyGetType("TLObject", true, false);*/
        private Object destClass = typeof(TLObject);

        private List<T> items = new List<T>();

        override public uint getClassId() {
            return CLASS_ID;
        }

        public Type getDestClass() {
            return destClass.GetType();
        }

        public void setDestClass(Type destClass) {
            try {
                if (destClass == null) {
                    throw new SystemException("DestClass could not be null");
                } else if (!destClass.GetType().Equals(typeof(int)) 
                    && !destClass.GetType().Equals(typeof(long)) 
                    && !destClass.GetType().Equals(typeof(String)) 
                    && !typeof(TLObject).IsAssignableFrom(destClass)) {
                    throw new SystemException("Unsupported DestClass");
                }
                this.destClass = destClass;
            } catch(SystemException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw e;
            }
        }

        new public void serializeBody(/*OutputStream*/StreamWriter stream) {
            stream.Write(items.Count);
            if (destClass is int) {
                foreach (T i in items) {
                    StreamingUtils.writeInt(Convert.ToUInt32(i), stream);
                }
            } else if (destClass is long) {
                foreach (T i in items) {
                    StreamingUtils.writeLong(Convert.ToInt64(i), stream);
                }
            } else if (destClass is String) {
                foreach (T i in items) {
                    StreamingUtils.writeTLString(Convert.ToString(i), stream);
                }
            } else {
                foreach (T i in items) {
                    StreamingUtils.writeTLObject(i as TLObject, stream);
                }
            }
        }

        new public void deserializeBody(/*InputStream*/BufferedStream stream, TLContext context) {
            try {
                if (destClass == null) {
                    throw new IOException("DestClass not set");
                }
                int count = StreamingUtils.readInt(stream);
                for (int i = 0; i < count; i++) {
                    if (destClass is int) {
                        items.Add((T)Convert.ChangeType(StreamingUtils.readInt(stream), typeof(T)));
                    } else if (destClass is long) {
                        items.Add((T)Convert.ChangeType(StreamingUtils.readLong(stream), typeof(T)));
                    } else if (destClass is String) {
                        items.Add((T)Convert.ChangeType(StreamingUtils.readTLString(stream), typeof(T)));
                    } else {
//                        items.Add((T) context.deserializeMessage(stream));
                    }
                }
            } catch (IOException e) {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
            }
        }

        // List implementations

        public int size() {
            return items.Count;
        }

        public Boolean isEmpty() {
            if (items.Count > 0)
                return false;
            else return true;
            
        }

        public Boolean contains(Object o) {
            return items.Contains((T)o);
        }

        public IEnumerator<T> iterator() {
            return listIterator();
        }

        public Object[] toArray() {
            return (Object[])Convert.ChangeType(items.ToArray(), typeof(Object[]));
        }
        /*
        public <T1> T1[] toArray(T1[] t1s) {
            return items.toArray(t1s);
        }
        */
        public Boolean add(T t) {
            int count = items.Count;
            items.Add(t);
            if(count == items.Count) return false;
            else return true;
        }

        public Boolean remove(Object o) {
            int count = items.Count;
            items.Remove((T) o);
            if(count == items.Count) return false;
            else return true;
        }
        /*
        public Boolean containsAll(Collection<?> objects) {
            return items.containsAll(objects);
        }

        public Boolean addAll(Collection<? extends T> ts) {
            return items.addAll(ts);
        }
        
        public Boolean addAll(int i, Collection<? extends T> ts) {
            return items.addAll(i, ts);
        }
        
        public Boolean removeAll(IEnumerable<T> objects)
        {
            return true;
        }
        */

        public Boolean retainAll(IEnumerable<T> objects)
        {
            int count = items.Count;
            items.AddRange(objects);
            if (count == items.Count) return false;
            else return true;
        }
        
        public void clear() {
            items.Clear();
        }

        public T get(int i) {
            return items.ElementAt(i);
        }

        public T set(int i, T t) {
            items[i] = t;
            return items.ElementAt(i);
        }

        public void add(int i, T t) {
            items.Insert(i, t);
        }

        public T remove(int i) {
            items.RemoveAt(i);
            return items.ElementAt(i);
        }

        public int indexOf(Object o) {
            return items.IndexOf((T)o);
        }

        public int lastIndexOf(Object o) {
            return items.LastIndexOf((T)o);
        }

        public /*ListIterator<T>*/IEnumerator<T> listIterator() {
            
            return items.GetEnumerator();
        }
        
        private IEnumerator<T> GetEnumerator(int offset)
        {
            for (int i = offset; i < items.Count; i++)
                yield return items[i];
        }

        public /*ListIterator<T>*/IEnumerator<T> listIterator(int i) {
            return GetEnumerator(i);
        }
        
        public List<T> subList(int i, int i2) {
            return items.GetRange(i, i2 - i);
            /*return (List<T>)items.Skip(1).Take(i2 - i);*/
        }

        public String toString() {
            return "vector#1cb5c415";
        }
    }
}
