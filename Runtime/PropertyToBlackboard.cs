using System;
using System.Collections.Generic;
using UnityEngine;

public class PropertyToBlackboard : MonoBehaviour
{
    [Serializable]
    public struct CopySetting
    {
        public string srcComponentName;
        public string srcName;
        public string blackboardPropertyName;
    }

    [SerializeField]
    private CopySetting _copySetting;

    public ref CopySetting Setting { get { return ref _copySetting; } }

    private void Update()
    {
        var copySetting = _copySetting;
        var sourceComponentType = PropertyUtil.GetType(copySetting.srcComponentName);
        var sourceComponent = GetComponent(sourceComponentType);
        object sourceValue;
        sourceValue = PropertyUtil.GetObjectByObjectPath(copySetting.srcName, sourceComponent);
        Blackboard.CurrentContext.Properties[Setting.blackboardPropertyName] = sourceValue;
    }

    public List<string> GetComponents()
    {
        var result = new List<string>();
        foreach (var component in gameObject.GetComponents(typeof(Component)))
        {
            if (component.GetType() == typeof(PropertyToBlackboard))
            {
                continue;
            }
            result.Add(component.GetType().FullName);
        }
        return result;
    }

}
