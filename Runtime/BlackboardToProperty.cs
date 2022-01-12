using System;
using System.Collections.Generic;
using UnityEngine;

public class BlackboardToProperty : MonoBehaviour
{
    [Serializable]
    public struct CopySetting
    {
        public string blackboardPropertyName;
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
            object sourceValue = Blackboard.CurrentContext.Properties[copySetting.blackboardPropertyName];

            var destinationComponentType = PropertyUtil.GetType(copySetting.dstComponentName);
            var destinationComponent = GetComponent(destinationComponentType);
            PropertyUtil.SetObject(copySetting.dstName, sourceValue, destinationComponentType, destinationComponent);
        }
    }

    public List<string> GetComponents()
    {
        var result = new List<string>();
        foreach (var component in gameObject.GetComponents(typeof(Component)))
        {
            if (component.GetType() == typeof(PropertyCopier))
            {
                continue;
            }
            result.Add(component.GetType().FullName);
        }
        return result;
    }

}
