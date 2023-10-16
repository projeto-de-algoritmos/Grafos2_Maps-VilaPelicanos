using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    [SerializeField]
    private int id;

    [SerializeField]
    private Dictionary<Node, float> edges = new();

    [SerializeField]
    public List<newEdges> edgesList = new();

    public Game game;

    private Button button;

    [SerializeField]
    [Serializable]
    public struct newEdges
    {
        public Node node;
        public float distance;
    }

    public Dictionary<Node, float> Edges
    {
        get { return edges; }
        set { edges = value; }
    }

    public void AddEdge(Node node, float distance)
    {
        Dictionary<Node, float> edge = new Dictionary<Node, float>();
        edges.Add(node, distance);
    }

    public bool ContainsNode(Node node)
    {
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
