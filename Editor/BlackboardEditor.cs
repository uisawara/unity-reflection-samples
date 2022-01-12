using System.Linq;
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
        var keys = blackboard.Keys.ToArray();
        
        foreach (var key in keys)
        {
            var value = blackboard[key];
            var type = value.GetType();
            if (type == typeof(int))
            {
                blackboard[key] = (object)EditorGUILayout.IntField(key, (int)value);
            }
            else if (type == typeof(float))
            {
                blackboard[key] = (object)EditorGUILayout.FloatField(key, (float)value);
            }
            else if (type == typeof(string))
            {
                blackboard[key] = (object)EditorGUILayout.TextField(key, (string)value);
            }
            else if (type == typeof(Vector2))
            {
                blackboard[key] = (object)EditorGUILayout.Vector2Field(key, (Vector2)value);
            }
            else if (type == typeof(Vector2Int))
            {
                blackboard[key] = (object)EditorGUILayout.Vector2IntField(key, (Vector2Int)value);
            }
            else if (type == typeof(Vector3))
            {
                blackboard[key] = (object)EditorGUILayout.Vector3Field(key, (Vector3)value);
            }
            else if (type == typeof(Vector3Int))
            {
                blackboard[key] = (object)EditorGUILayout.Vector3IntField(key, (Vector3Int)value);
            }
            else if (type == typeof(Vector4))
            {
                blackboard[key] = (object)EditorGUILayout.Vector3Field(key, (Vector3)value);
            }
            else if (type == typeof(Vector4))
            {
                blackboard[key] = (object)EditorGUILayout.Vector3Field(key, (Vector4)value);
            }
            else
            {
                EditorGUILayout.LabelField(key + " " + (string)value);
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
