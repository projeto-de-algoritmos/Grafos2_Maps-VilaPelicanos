using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game : MonoBehaviour
{
    public Manager manager;
    public List<Button> characters;

    public Image char01;
    public TextMeshProUGUI startNode01;
    public TextMeshProUGUI endNode01;

    public Image char02;
    public TextMeshProUGUI startNode02;
    public TextMeshProUGUI endNode02;
    
    public int selectionChar = 1;

    public void SelectionCharacter(Characters charac)
    {
        if (selectionChar == 1)
        {
            char01.sprite = charac.imageChar;
        }
        else if (selectionChar == 2)
        {
            char02.sprite = charac.imageChar;
        }
    }

    public void setNodeStart(Node node)
    {
        if (selectionChar == 1) 
        {
            startNode01.text = node.getId().ToString();
            manager.startCharacter01 = node;
        }
        else if (selectionChar == 2)
        {
            startNode02.text = node.getId().ToString();
            manager.startCharacter02 = node;
        }
    }

    public void setNodeEnd(Node node)
    {
        if (selectionChar == 1)
        {
            endNode01.text = node.getId().ToString();
            manager.endCharacter01 = node;
        }
        else if (selectionChar == 2)
        {
            endNode02.text = node.getId().ToString();
            manager.endCharacter02 = node;
        }
    }

    public void setSelectionChar(int charac) 
    {
        selectionChar = charac;
    }
}
