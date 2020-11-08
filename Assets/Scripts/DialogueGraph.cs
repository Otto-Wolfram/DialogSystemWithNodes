using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueGraph : EditorWindow
{
    private DialogueGraphView _graphView;

    // теперь в юнити сверху появится меню Graph
    [MenuItem("Graph/Dialogue Graph")]
    public static void OpenDialogueGraphWindow()
    {
        var window = GetWindow<DialogueGraph>();
        window.titleContent = new GUIContent("Dialogue graph"); 
    }

    private void OnEnable()
    {
        _graphView = new DialogueGraphView
        {
            name = "Dialogue graph"
        };

        _graphView.StretchToParentSize();
        rootVisualElement.Add(_graphView);
    }

    private void OnDisable()
    {
        rootVisualElement.Remove(_graphView);
    }
}
