using System.Reflection;

namespace PocketbaseNET.utils
{
    /// <summary>
    /// A utility class for deep cloning.
    /// </summary>
    public class Cloner
    {
        /// <summary>
        /// Reflectively and recursively clone an object.
        /// </summary>
        /// <param name="source">The object to clone. <b>Must not be of an anonymous type.</b></param>
        /// <returns>The cloned object.</returns>
        public static object? ReflectiveClone(object? source)
        {
            if (source is null)
                return null;

            object? target;
            Type srcType = source.GetType();

            if (srcType.IsValueType || srcType.IsEnum || srcType.Equals(typeof(string)))
                return source; // cloning not required
            
            if (srcType.IsArray) // handle array cloning
            {
                Type srcBaseType = srcType.GetElementType()!;
                var srcAsArray = (Array)source;
                
                target = Array.CreateInstance(srcBaseType, srcAsArray.Length);
                
                for (int i = 0; i < srcAsArray.Length; i++)
                    ((Array)target).SetValue(ReflectiveClone(srcAsArray.GetValue(i)), i);

                return target;
            }

            try
            {
                target = Activator.CreateInstance(srcType);
            } catch (Exception e)
            {
                throw new CloneException($"An exception occured when cloning: {e.Message}", e);
            }


            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            var propInfos = srcType.GetProperties(flags);

            foreach (PropertyInfo propInfo in propInfos)
                if (propInfo.CanWrite)
                    if (propInfo.PropertyType.IsValueType || propInfo.PropertyType.IsEnum || propInfo.PropertyType.Equals(typeof(string)))
                        propInfo.SetValue(target, propInfo.GetValue(source, null), null);
                    else
                    {
                        object? propVal = propInfo.GetValue(source, null);
                        if (propVal is not null)
                            propInfo.SetValue(target, ReflectiveClone(propVal), null);
                        else
                            propInfo.SetValue(target, null, null);  
                    }
            return target;
        }

        /// <summary>
        /// Reflectively and recursively clone an object, then cast it to a type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to cast the result to</typeparam>
        /// <param name="source">The object to clone. <b>Must not be of an anonymous type.</b></param>
        /// <returns>The cloned object.</returns>
        public static T? ReflectiveClone<T>(object? source) => (T?)ReflectiveClone(source);
    }

    internal class CloneException : Exception
    {
        internal CloneException() : base() { }
        internal CloneException(string message) : base(message) { }
        internal CloneException(string message, Exception inner) : base(message, inner) { }
    }
}