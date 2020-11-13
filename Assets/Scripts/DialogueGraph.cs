using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
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
        ConstructGraphView();
        GenerateToolbar(); 
    }

    private void OnDisable()
    {
        rootVisualElement.Remove(_graphView);
    }

    private void ConstructGraphView()
    {
        _graphView = new DialogueGraphView
        {
            name = "Dialogue graph",
        };

        _graphView.StretchToParentSize();
        rootVisualElement.Add(_graphView);
    }

    // добавляем тулбар с кнопками к окну
    private void GenerateToolbar()
    {
        var toolbar = new Toolbar();

        var nodeCreateButton = new Button(() => { _graphView.CreateNode("Dialogue node"); });
        nodeCreateButton.text = "Create Node";

        toolbar.Add(nodeCreateButton);

        rootVisualElement.Add(toolbar);
    }
}
