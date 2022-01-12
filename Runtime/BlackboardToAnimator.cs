using System;
using UnityEngine;

public class BlackboardToAnimator : MonoBehaviour
{
    [Serializable]
    public struct CopySetting
    {
        public string blackboardPropertyName;
        public string animatorParamName;
    }

    [SerializeField]
    private CopySetting _copySetting;

    [SerializeField]
    private Animator _animator;

    private void OnValidate()
    {
        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }
    }

    private void Update()
    {
        var copySetting = _copySetting;
        {
            object sourceValue = Blackboard.CurrentContext.Properties[copySetting.blackboardPropertyName];

            var type = sourceValue.GetType();
            if (type == typeof(int))
            {
                _animator.SetInteger(copySetting.animatorParamName, (int)sourceValue);
            }
            else if (type == typeof(float))
            {
                _animator.SetFloat(copySetting.animatorParamName, (float)sourceValue);
            }
            else if (type == typeof(bool))
            {
                _animator.SetBool(copySetting.animatorParamName, (bool)sourceValue);
            }
            else
            {
                Debug.LogError($"unknown type {type.FullName}");
            }
        }
    }
    
}
