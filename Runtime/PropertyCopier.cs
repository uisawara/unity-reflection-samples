using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class PropertyCopier : MonoBehaviour
{
    [Serializable]
    public struct CopySetting
    {
        public string srcComponentName;
        public string srcName;
        public string dstComponentName;
        public string dstName;
    }

    [SerializeField]
    private CopySetting _copySetting;

    public ref CopySetting Setting { get { return ref _copySetting; } }

    private void Update()
    {
        var copySetting = _copySetting;
        {
            var sourceComponentType = PropertyUtil.GetType(copySetting.srcComponentName);
            var sourceComponent = GetComponent(sourceComponentType);
            object sourceValue;
            sourceValue = PropertyUtil.GetObjectByObjectPath(copySetting.srcName, sourceComponent);

            var destinationComponentType = PropertyUtil.GetType(copySetting.dstComponentName);
            var destinationComponent = GetComponent(destinationComponentType);
            PropertyUtil.SetObject(copySetting.dstName, sourceValue, destinationComponentType, destinationComponent);
        }
    }

    public List<string> GetComponents()
    {
        var result = new List<string>();
        foreach(var component in gameObject.GetComponents(typeof(Component)))
        {
            if(component.GetType()==typeof(PropertyCopier)) {
                continue;
            }
            result.Add(component.GetType().FullName);
        }
        return result;
    }

}

public static class PropertyUtil
{
    public static List<string> GetFieldsAndProperties(Type type)
    {
        return GetFieldsAndProperties(type, 2);
    }

    public static List<string> GetFieldsAndProperties(Type type, int descent)
    {
        descent--;

        var result = new List<string>();
        foreach (var propertyInfo in type.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public))
        {
            result.Add(propertyInfo.Name);
            if (descent > 0)
            {
                result.AddRange(GetFieldsAndProperties(propertyInfo.PropertyType, descent).Select(x => propertyInfo.Name + "." + x));
            }
        }
        foreach (var fieldInfo in type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public))
        {
            result.Add(fieldInfo.Name);
            if (descent > 0)
            {
                result.AddRange(GetFieldsAndProperties(fieldInfo.FieldType, descent).Select(x => fieldInfo.Name + "." + x));
            }
        }
        return result;
    }

    public static List<string> GetFieldsAndProperties(string typeName)
    {
        return GetFieldsAndProperties(GetType(typeName));
    }

    public static void SetObject(string dstName, object sourceValue, Type destinationType, object destinationObject)
    {
        var index = dstName.LastIndexOf('.');
        if (index == -1)
        {

        }
        else
        {
            var path = dstName.Substring(0, index);
            destinationObject = GetObjectByObjectPath(path, destinationObject);
            dstName = dstName.Substring(index + 1);
        }

        var destinationField = destinationType.GetField(dstName);
        if (destinationField != null)
        {
            destinationField.SetValue(destinationObject, sourceValue);
        }
        else
        {
            var destinationProperty = destinationType.GetProperty(dstName);
            destinationProperty.SetValue(destinationObject, sourceValue);
        }
    }

    public static object GetObjectByObjectName(string name, object sourceObject)
    {
        object sourceValue;
        var sourceType = sourceObject.GetType();
        var sourceField = sourceType.GetField(name);
        if (sourceField != null)
        {
            sourceValue = sourceField.GetValue(sourceObject);
        }
        else
        {
            var sourceProperty = sourceType.GetProperty(name);
            sourceValue = sourceProperty.GetValue(sourceObject);
        }

        return sourceValue;
    }

    public static object GetObjectByObjectPath(string name, object sourceObject)
    {
        var names = name.Split(new[] { '.' });
        foreach (var n in names)
        {
            sourceObject = GetObjectByObjectName(n, sourceObject);
        }
        return sourceObject;
    }

    public static Type GetType(string typeName)
    {
        var type = Type.GetType(typeName);

        if (type != null)
            return type;

        var assemblyName = typeName.Substring(0, typeName.IndexOf('.'));
        var assembly = Assembly.LoadWithPartialName(assemblyName);
        if (assembly == null)
            return null;

        return assembly.GetType(typeName);
    }
}
