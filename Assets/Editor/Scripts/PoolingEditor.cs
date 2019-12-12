using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(ObjectPooler))]
[CanEditMultipleObjects]
public class PoolingEditor : Editor
{
    ObjectPooler obj;
    SerializedObject getTarget;
    SerializedProperty objList;
    private void OnEnable()
    {
        obj = (ObjectPooler)target;
        getTarget = new SerializedObject(obj);
        objList = serializedObject.FindProperty("pools");
    }
    public override void OnInspectorGUI()
    {
        getTarget.Update();
        GUI.backgroundColor = Color.Lerp(Color.yellow, Color.red, 0.6f);
        for (int i = 0; i < objList.arraySize; i++)
        {
            SerializedProperty listRef = objList.GetArrayElementAtIndex(i);
            SerializedProperty tagRef = listRef.FindPropertyRelative("tag");
            SerializedProperty prefabRef = listRef.FindPropertyRelative("prefab");
            SerializedProperty sizeRef = listRef.FindPropertyRelative("size");

            EditorGUILayout.LabelField("Automatic Field By Property Type", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(tagRef);
            EditorGUILayout.PropertyField(prefabRef);
            EditorGUILayout.IntSlider(sizeRef, 0, 500, new GUIContent("Size"));
            if (!sizeRef.hasMultipleDifferentValues)
            {
                ProgressBar(sizeRef.intValue / 500.0f, "Size");
            }

            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }

        EditorGUILayout.LabelField("Add a new item", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        if (GUILayout.Button("Add"))
        {
            obj.pools.Add(new ObjectPooler.Pool());
        }
        if (GUILayout.Button("Delete"))
        {
            obj.pools.Clear();
        }

        serializedObject.ApplyModifiedProperties();
    }

    void ProgressBar(float value_, string label_)
    {
        Rect rect = GUILayoutUtility.GetRect(18, 18, "TextField");
        EditorGUI.ProgressBar(rect, value_, label_);
        EditorGUILayout.Space();
    }
}

