using System.Reflection;

namespace PocketbaseNET.utils
{
    public class Cloner
    {
        /// <summary>
        /// Reflectively and recursively clone an object.
        /// </summary>
        /// <param name="source">The object to clone. <b>Must not be of an anonymous type.</b></param>
        /// <returns></returns>
        public static object? ReflectiveClone(object? source)
        {
            if (source is null)
                return null;

            Type srcType = source.GetType();
            object? target = Activator.CreateInstance(srcType);

            if (target is null)
                return null;

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
    }
}