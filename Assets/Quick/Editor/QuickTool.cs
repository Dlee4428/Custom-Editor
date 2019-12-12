using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class QuickTool : EditorWindow
{
    [MenuItem("Quickbar/Open _%#T")]
    public static void ShowWindow()
    {
        QuickTool tool = GetWindow<QuickTool>();

        tool.titleContent = new GUIContent("Quickbar");
        tool.minSize = new Vector2(256, 64);
    }

    private void OnEnable()
    {
        VisualElement root = rootVisualElement;

        root.styleSheets.Add(Resources.Load<StyleSheet>("Quick_Style"));

        VisualTreeAsset tree = Resources.Load<VisualTreeAsset>("Quick_Main");
        tree.CloneTree(root);

        UQueryBuilder<Button> buttons = root.Query<Button>();
        buttons.ForEach(SetupButton);
    }

    private void SetupButton(Button button)
    {
        VisualElement icon = button.Q(className: "quicktool-button-icon");
        string path = "Icons/" + button.parent.name;
        Texture2D image = Resources.Load<Texture2D>(path);

        icon.style.backgroundImage = image;
        button.clickable.clicked += () => CreateObject(button.parent.name);
        button.tooltip = button.parent.name;
    }

    private void CreateObject(string type)
    {
        GameObject o;

        switch (type)
        {
            case "cube":
                o = ObjectFactory.CreateGameObject("CubeSpawner", typeof(CubeSpawner));
                break;
            case "sphere":
                o = ObjectFactory.CreateGameObject("SphereSpawner", typeof(SphereSpawner));
                break;
            default:
                o = ObjectFactory.CreateGameObject("CubeSpawner", typeof(CubeSpawner));
                break;
        }

        o.transform.position = Vector3.zero;
        Selection.activeGameObject = o;
    }
}
