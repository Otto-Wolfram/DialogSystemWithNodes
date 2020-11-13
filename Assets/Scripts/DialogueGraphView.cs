using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueGraphView : GraphView
{
    private readonly Vector2 defaultNodeSize = new Vector2(150, 200);

    public DialogueGraphView()
    {
        // добавляем стиль USS
        styleSheets.Add(Resources.Load<StyleSheet>("Editor/DialogueGraph"));

        // добавляем возможности
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);


        // раскрашиваем фон (свистоперделки)
        var grid = new GridBackground();
        Insert(0, grid);
        grid.StretchToParentSize();


        // готово
        AddElement(GenerateEntryPointNode());
    }

    // делает все порты совместимыми с данным
    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        var comparablePorts = new List<Port>();

        ports.ForEach(port =>
        {
            if (startPort != port && startPort.node != port.node)
            {
                comparablePorts.Add(port); 
            }
        });

        return comparablePorts;
    }

    // создает стартовую ноду
    public DialogueNode GenerateEntryPointNode()
    {
        var node = new DialogueNode
        {
            title = "Start",
            GUID = Guid.NewGuid().ToString(),
            dialogueText = "Entry point",
            entryPoint = true
        };

        // генерируем порт и добавляем его в выходной контейнер ноды
        var generatedPort = GeneratePort(node, Direction.Output);
        generatedPort.portName = "Next";
        node.outputContainer.Add(generatedPort);

        // обновляем внешний вид ноды
        RefreshNode(node);

        // устанавливаем позицию ноды при создании
        node.SetPosition(new Rect(100, 200, 100, 150));

        return node; 
    }

    // добавляет выбор
    private void AddChoicePort(DialogueNode dialogueNode)
    {
        var generatedPort = GeneratePort(dialogueNode, Direction.Output);

        var outputPartCount = dialogueNode.outputContainer.Query("connector").ToList().Count;

        generatedPort.portName = $"Choice {outputPartCount}";

        dialogueNode.outputContainer.Add(generatedPort);

        RefreshNode(dialogueNode);
    }

    // создает порт у переданной ноды
    private Port GeneratePort(DialogueNode node, Direction portDirection, Port.Capacity capacity = Port.Capacity.Single)
    {
        return node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(float));
    }

    // подготавливает НЕ СТАРТОВУЮ ноду к созданию
    public DialogueNode CreateDialogueNode(string nodeName)
    {
        var dialogueNode = new DialogueNode
        {
            title = nodeName,
            dialogueText = nodeName,
            GUID = Guid.NewGuid().ToString()
        };

        // добавление входного порта
        var inputPort = GeneratePort(dialogueNode, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "Input";
        dialogueNode.inputContainer.Add(inputPort);

        // добавляем кнопку добавлениявыбора
        var button = new Button(() => { AddChoicePort(dialogueNode); });
        button.text = "New choice";
        dialogueNode.titleContainer.Add(button);

        RefreshNode(dialogueNode);

        dialogueNode.SetPosition(new Rect(Vector2.zero, defaultNodeSize));

        return dialogueNode;
    }

    // отображает ноду на экране
    public void CreateNode(string nodeName)
    {
        AddElement(CreateDialogueNode(nodeName));
    }


    private void RefreshNode(DialogueNode node)
    {
        node.RefreshExpandedState();
        node.RefreshPorts();
    }
}
