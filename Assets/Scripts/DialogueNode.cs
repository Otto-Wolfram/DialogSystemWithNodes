using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DialogueNode : Node
{
    // уникальный ID ноды, чтобы переходить от одной к другой
    public string GUID;

    // предложение, которое надо сказать
    public string dialogText;

    // является ли стартовой?
    public bool entryPoint;


}
