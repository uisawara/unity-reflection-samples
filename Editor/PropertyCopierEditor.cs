using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PropertyCopier))]
public class PropertyCopierEditor : Editor
{
    private PropertyCopier _target;

    void OnEnable()
    {
        _target = (PropertyCopier)target;
        Debug.Log("Inspector.Enabled");
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        var list = _target.GetComponents();
        _target.Setting.srcComponentName = list[EditorGUILayout.Popup("SourceComponent", (int)list.IndexOf(_target.Setting.srcComponentName), list.ToArray())];
        var srcNames = PropertyUtil.GetFieldsAndProperties(_target.Setting.srcComponentName);
        _target.Setting.srcName = srcNames[EditorGUILayout.Popup("SourceName", (int)srcNames.IndexOf(_target.Setting.srcName), srcNames.ToArray())];

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
