using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BlackboardToProperty))]
public class BlackboardToPropertyEditor : Editor
{
    private BlackboardToProperty _target;

    void OnEnable()
    {
        _target = (BlackboardToProperty)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        var list = _target.GetComponents();

        _target.Setting.blackboardPropertyName = EditorGUILayout.TextField("BlackboardPropertyName", _target.Setting.blackboardPropertyName);

        _target.Setting.dstComponentName = list[EditorGUILayout.Popup("DestinationComponent", (int)list.IndexOf(_target.Setting.dstComponentName), list.ToArray())];
        var dstNames = PropertyUtil.GetFieldsAndProperties(_target.Setting.dstComponentName);
        _target.Setting.dstName = dstNames[EditorGUILayout.Popup("DestinationName", (int)dstNames.IndexOf(_target.Setting.dstName), dstNames.ToArray())];

        if (EditorGUI.EndChangeCheck())
        {
            var objectToUndo = _target;
            var name = "Extend";
            Undo.RecordObject(objectToUndo, name);
            //_target.index = index;
        }
    }
}
