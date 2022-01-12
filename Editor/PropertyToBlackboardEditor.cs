using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PropertyToBlackboard))]
public class PropertyToBlackboardEditor : Editor
{
    private PropertyToBlackboard _target;

    void OnEnable()
    {
        _target = (PropertyToBlackboard)target;
        Debug.Log("Inspector.Enabled");
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        var list = _target.GetComponents();
        _target.Setting.srcComponentName = list[EditorGUILayout.Popup("SourceComponent", (int)list.IndexOf(_target.Setting.srcComponentName), list.ToArray())];
        var srcNames = PropertyUtil.GetFieldsAndProperties(_target.Setting.srcComponentName);
        _target.Setting.srcName = srcNames[EditorGUILayout.Popup("SourceName", (int)srcNames.IndexOf(_target.Setting.srcName), srcNames.ToArray())];

        _target.Setting.blackboardPropertyName = EditorGUILayout.TextField("BlackboardPropertyName", _target.Setting.blackboardPropertyName);

        if (EditorGUI.EndChangeCheck())
        {
            var objectToUndo = _target;
            var name = "Extend";
            Undo.RecordObject(objectToUndo, name);
            //_target.index = index;
        }
    }
}
