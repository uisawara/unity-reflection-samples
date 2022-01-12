using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Blackboard))]
public class BlackboardEditor : Editor
{
    private Blackboard _target;

    void OnEnable()
    {
        _target = (Blackboard)target;
    }



    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        var blackboard = Blackboard.CurrentContext.Properties;
        foreach (var kv in blackboard)
        {
            var type = kv.Value.GetType();
            if (type == typeof(int))
            {
                blackboard[kv.Key] = (object)EditorGUILayout.IntField(kv.Key, (int)kv.Value);
            }
            else if (type == typeof(float))
            {
                blackboard[kv.Key] = (object)EditorGUILayout.FloatField(kv.Key, (float)kv.Value);
            }
            else if (type == typeof(string))
            {
                blackboard[kv.Key] = (object)EditorGUILayout.TextField(kv.Key, (string)kv.Value);
            }
            else if (type == typeof(Vector2))
            {
                blackboard[kv.Key] = (object)EditorGUILayout.Vector2Field(kv.Key, (Vector2)kv.Value);
            }
            else if (type == typeof(Vector2Int))
            {
                blackboard[kv.Key] = (object)EditorGUILayout.Vector2IntField(kv.Key, (Vector2Int)kv.Value);
            }
            else if (type == typeof(Vector3))
            {
                blackboard[kv.Key] = (object)EditorGUILayout.Vector3Field(kv.Key, (Vector3)kv.Value);
            }
            else if (type == typeof(Vector3Int))
            {
                blackboard[kv.Key] = (object)EditorGUILayout.Vector3IntField(kv.Key, (Vector3Int)kv.Value);
            }
            else if (type == typeof(Vector4))
            {
                blackboard[kv.Key] = (object)EditorGUILayout.Vector3Field(kv.Key, (Vector3)kv.Value);
            }
            else if (type == typeof(Vector4))
            {
                blackboard[kv.Key] = (object)EditorGUILayout.Vector3Field(kv.Key, (Vector4)kv.Value);
            }
            else
            {
                EditorGUILayout.LabelField(kv.Key + " " + (string)kv.Value);
            }
        }

        if (EditorGUI.EndChangeCheck())
        {
            var objectToUndo = _target;
            var name = "Extend";
            Undo.RecordObject(objectToUndo, name);
            //_target.index = index;
        }

        EditorUtility.SetDirty(this);
    }
}
