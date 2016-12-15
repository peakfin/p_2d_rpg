using UnityEditor;
using UnityEngine;

public class TileMapMaker : EditorWindow
{
    string myString = "Hello World";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;

    Vector2 scrollPos;
    string t = "This is a string inside a Scroll view!";

    // Add menu item named "My Window" to the Window menu
    [MenuItem("Window/TileMap Maker")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(TileMapMaker));
    }

    static void Init()
    {
        var window = GetWindow<TileMapMaker>();
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        myString = EditorGUILayout.TextField("Text Field", myString);

        groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        myBool = EditorGUILayout.Toggle("Toggle", myBool);
        myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        EditorGUILayout.EndToggleGroup();


        using (var h = new EditorGUILayout.HorizontalScope("Button"))
        {
            if (GUI.Button(h.rect, GUIContent.none))
                Debug.Log("Go here");
            GUILayout.Label("I'm inside the button");
            GUILayout.Label("So am I");
        }

        using (var h = new EditorGUILayout.HorizontalScope())
        {
            using (var scrollView = new EditorGUILayout.ScrollViewScope(scrollPos, GUILayout.Width(100), GUILayout.Height(100)))
            {
                scrollPos = scrollView.scrollPosition;
                GUILayout.Label(t);
            }
            if (GUILayout.Button("Add More Text", GUILayout.Width(100), GUILayout.Height(100)))
                t += " \nAnd this is more text!";
        }
        if (GUILayout.Button("Clear"))
            t = "";
        
        
    }
}
