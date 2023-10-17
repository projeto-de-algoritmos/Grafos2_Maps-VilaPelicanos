using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game : MonoBehaviour
{
    public Manager manager;
    public GameObject prefabChar;
    public Transform parentChar;

    public List<Button> characters;

    public Image char01;
    public TextMeshProUGUI startNode01;
    public TextMeshProUGUI endNode01;

    public Image char02;
    public TextMeshProUGUI startNode02;
    public TextMeshProUGUI endNode02;

    public GameObject charObj01;
    public GameObject charObj02;

    public float tamBarra = 50f;
    
    public int selectionChar = 1;
    public int selectionText = 1;

    public void CreateCharacter(Node node, List<Node> nodes, List<Node> nodes2, int numChar)
    {
        GameObject characterInstance = Instantiate(prefabChar, parentChar);
        characterInstance.transform.localPosition = node.gameObject.transform.localPosition;

        Image characterImage = characterInstance.GetComponent<Image>();

        if (numChar == 1)
        {
            characterImage.sprite = char01.sprite;
            charObj01 = characterInstance;
        }
        else
        {
            characterImage.sprite = char02.sprite;
            charObj02 = characterInstance;
        }

        characterInstance.GetComponent<Characters>().StartChar(nodes, nodes2, manager);
    }

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

    public void setSelectionText(int text)
    {
        selectionText = text;
    }

    public void SelectionNode(Node node)
    {
        if (selectionText == 1)
        {
            setNodeStart(node);
        }
        else 
        { 
            setNodeEnd(node);
        }
    }

    public void setNodeStart(Node node)
    {
        if (selectionChar == 1) 
        {
            startNode01.text = node.Id.ToString();
            manager.startCharacter01 = node;
        }
        else if (selectionChar == 2)
        {
            startNode02.text = node.Id.ToString();
            manager.startCharacter02 = node;
        }
    }

    public void setNodeEnd(Node node)
    {
        if (selectionChar == 1)
        {
            endNode01.text = node.Id.ToString();
            manager.endCharacter01 = node;
        }
        else if (selectionChar == 2)
        {
            endNode02.text = node.Id.ToString();
            manager.endCharacter02 = node;
        }
    }

    public void setSelectionChar(int charac) 
    {
        selectionChar = charac;
    }
}
