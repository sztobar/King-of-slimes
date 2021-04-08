using System;
using System.Reflection;

namespace KiteEditTests
{
  /// <summary>
  /// Copied from Stack Overflow response: https://stackoverflow.com/a/1565766
  /// </summary>
  public static class PrivateHelpers
  {
    /// <summary>
    /// Returns a _private_ Property Value from a given Object. Uses Reflection.
    /// Throws a ArgumentOutOfRangeException if the Property is not found.
    /// </summary>
    /// <typeparam name="T">Type of the Property</typeparam>
    /// <param name="obj">Object from where the Property Value is returned</param>
    /// <param name="propName">Propertyname as string.</param>
    /// <returns>PropertyValue</returns>
    public static T GetPrivatePropertyValue<T>(this object obj, string propName)
    {
      if (obj == null)
        throw new ArgumentNullException("obj");
      PropertyInfo pi = obj.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
      if (pi == null)
        throw new ArgumentOutOfRangeException("propName", string.Format("Property {0} was not found in Type {1}", propName, obj.GetType().FullName));
      return (T)pi.GetValue(obj, null);
    }

    /// <summary>
    /// Returns a private Property Value from a given Object. Uses Reflection.
    /// Throws a ArgumentOutOfRangeException if the Property is not found.
    /// </summary>
    /// <typeparam name="T">Type of the Property</typeparam>
    /// <param name="obj">Object from where the Property Value is returned</param>
    /// <param name="propName">Propertyname as string.</param>
    /// <returns>PropertyValue</returns>
    public static T GetPrivateFieldValue<T>(this object obj, string propName)
    {
      if (obj == null)
        throw new ArgumentNullException("obj");
      Type t = obj.GetType();
      FieldInfo fi = null;
      while (fi == null && t != null)
      {
        fi = t.GetField(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        t = t.BaseType;
      }
      if (fi == null)
        throw new ArgumentOutOfRangeException("propName", string.Format("Field {0} was not found in Type {1}", propName, obj.GetType().FullName));
      return (T)fi.GetValue(obj);
    }

    /// <summary>
    /// Sets a _private_ Property Value from a given Object. Uses Reflection.
    /// Throws a ArgumentOutOfRangeException if the Property is not found.
    /// </summary>
    /// <typeparam name="T">Type of the Property</typeparam>
    /// <param name="obj">Object from where the Property Value is set</param>
    /// <param name="propName">Propertyname as string.</param>
    /// <param name="val">Value to set.</param>
    /// <returns>PropertyValue</returns>
    public static void SetPrivatePropertyValue<T>(this object obj, string propName, T val)
    {
      Type t = obj.GetType();
      if (t.GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance) == null)
        throw new ArgumentOutOfRangeException("propName", string.Format("Property {0} was not found in Type {1}", propName, obj.GetType().FullName));
      t.InvokeMember(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty | BindingFlags.Instance, null, obj, new object[] { val });
    }

    /// <summary>
    /// Set a private Property Value on a given Object. Uses Reflection.
    /// </summary>
    /// <typeparam name="T">Type of the Property</typeparam>
    /// <param name="obj">Object from where the Property Value is returned</param>
    /// <param name="propName">Propertyname as string.</param>
    /// <param name="val">the value to set</param>
    /// <exception cref="ArgumentOutOfRangeException">if the Property is not found</exception>
    public static void SetPrivateFieldValue<T>(ref object obj, string propName, T val)
    {
      if (obj == null)
        throw new ArgumentNullException("obj");
      Type t = obj.GetType();
      FieldInfo fi = null;
      while (fi == null && t != null)
      {
        fi = t.GetField(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        t = t.BaseType;
      }
      if (fi == null)
        throw new ArgumentOutOfRangeException("propName", string.Format("Field {0} was not found in Type {1}", propName, obj.GetType().FullName));
      fi.SetValue(obj, val);
    }

    public static void CallPrivateMethod<T>(T obj, string methodName)
    {
      if (obj == null)
        throw new ArgumentNullException("obj");
      Type t = obj.GetType();
      MethodInfo methodInfo = null;
      while (methodInfo == null && t != null)
      {
        methodInfo = t.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
        t = t.BaseType;
      }
      if (methodInfo == null)
        throw new ArgumentOutOfRangeException("methodName", string.Format("Method {0} was not found in Type {1}", methodName, obj.GetType().FullName));
      methodInfo.Invoke(obj, Array.Empty<object>());
    }
  }
}