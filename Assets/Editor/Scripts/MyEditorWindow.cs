using UnityEngine;
using UnityEditor;

public class MyEditorWindow : EditorWindow
{
    Color color;
    float scale = 0.0f;

    [MenuItem("Window/MyEditor")]
    public static void ShowWindow()
    {
        GetWindow<MyEditorWindow>("Colorizer");
    }

    void OnGUI()
    {
        GUI.backgroundColor = Color.Lerp(Color.yellow, Color.red, 0.6f);
        EditorGUILayout.Space();
        GUILayout.Label("Color the selected an object/objects", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        color = EditorGUILayout.ColorField("Color", color);

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        
        GUILayout.Label("Scale the selected an object/objects", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        
        scale = EditorGUILayout.FloatField("Scale", scale);
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        if (GUILayout.Button("Proceed"))
        {
            Colorize();
            Transform();
        }
    }

    private void Colorize()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = color;
                
            }
        }
    }

    private void Transform()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            Transform trans = obj.GetComponent<Transform>();
            if (trans != null)
            {
                trans.transform.localScale = new Vector3(scale, scale, scale);
            }
        }
    }
}
