using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    [SerializeField]
    private int id;

    [SerializeField]
    private Dictionary<Node, float> edges;

    [SerializeField]
    public List<Node> nodesAdj = new();

    public Game game;

    private Button button;

    public Dictionary<Node, float> Edges
    {
        get { return edges; }
        set { edges = value; }
    }

    public void ResetEdges()
    {
        edges = new Dictionary<Node, float>();
    }

    public float AddEdge(Node node, float distance)
    {
        edges ??= new Dictionary<Node, float>();

        edges.Add(node, distance);

        return edges[node];
    }

    public bool ContainsNode(Node node)
    {
        edges ??= new Dictionary<Node, float>();
        return edges.ContainsKey(node);
    }


    private void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(ClickNode);
    }

    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    public void ClickNode()
    {
        game.SelectionNode(this);
    }

    
}
