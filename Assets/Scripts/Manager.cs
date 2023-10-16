using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    [SerializeField]
    public Image edgePrefab;

    [SerializeField]
    public Transform parentEdge;

    [SerializeField]
    private GameObject rectanglePrefab;

    public AlgorithmBFS algorithBFS;
    public Game game;

    public Slider speedSlider;
    public float speed = 1;
    public TextMeshProUGUI speedValue;

    public List<Node> graph;
    public Node startCharacter01;
    public Node endCharacter01;
    public Node startCharacter02;
    public Node endCharacter02;
    public int friendship;
    public Slider slider;
    public TextMeshProUGUI valueFriendship;

    [SerializeField]
    public int[][] matrixAdj;

    // Start is called before the first frame update
    void Start()
    {
        slider.onValueChanged.AddListener(UpdateSliderValue);
        speedSlider.onValueChanged.AddListener(UpdateSpeedValue);

        graph ??= new List<Node>();

        SetEdges(this);

        foreach (Node node in graph)
        {
            if (node != null && node.Edges != null && node.Edges.Count == 0)
                Debug.Log(node.gameObject.name);
        }
    }

    void UpdateSpeedValue(float newValue)
    {
        speed = ((int)newValue);
        speedValue.text = speed.ToString();
    }

    public void StartGame()
    {
        /*
        List<Node> nodes = algorithBFS.BFS(graph, startCharacter01, endCharacter01);

        if (nodes.Count != 0)
            game.CreateCharacter(nodes[0], nodes);

        */
    }

    // Função chamada quando o valor do slider é alterado.
    void UpdateSliderValue(float newValue)
    {
        friendship = ((int)newValue);
        valueFriendship.text = friendship.ToString();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public static void DeleteEdges(Manager manager)
    {
        int secury = 0;
        while (manager.parentEdge.childCount > 0 && secury < 500)
        {
            secury++;
            DestroyImmediate(manager.parentEdge.GetChild(0).gameObject);
        }

        Debug.Log("Delete complete");
    }
    public static void SetEdges(Manager manager)
    {
        DeleteEdges(manager);

        foreach (Node node in manager.graph)
        {
            if (node != null && node.nodesAdj.Count > 0)
            {
                List<Node> nodesNull = new List<Node>();
                node.ResetEdges();
                foreach (Node adj in node.nodesAdj)
                {
                    if (adj == null)
                    {
                        nodesNull.Add(adj);
                        continue;
                    }

                    float distance = Vector2.Distance(adj.transform.position, node.transform.position);
                    float distancereal = node.AddEdge(adj, distance);
                    // Debug.Log(distancereal);
                }

                foreach (Node nodeNull in nodesNull)
                    node.nodesAdj.Remove(nodeNull);
            }
        }

        int numNodes = manager.graph.Count;

        manager.matrixAdj = new int[numNodes][];

        for (int i = 0; i < numNodes; i++)
        {
            manager.matrixAdj[i] = new int[numNodes];
            manager.graph[i].Id = i;

            for (int j = 0; j < numNodes; j++)
            {
                manager.matrixAdj[i][j] = 0;
            }
        }

        foreach (Node node in manager.graph)
        {
            int currentNodeId = node.Id;

            if (node.Edges == null)
                continue;

            foreach (Node edge in node.Edges.Keys)
            {
                Node adjNode = edge;
                float peso = node.Edges[edge];

                int adjNodeId = adjNode.Id;

                manager.matrixAdj[currentNodeId][adjNodeId] = 1;

                if (!adjNode.ContainsNode(node))
                {
                    adjNode.AddEdge(node, peso);
                }

                if (manager.matrixAdj[adjNodeId][currentNodeId] != 1)
                {
                    Vector3 position = (node.transform.localPosition + adjNode.transform.localPosition) / 2f;

                    float distance = Vector3.Distance(node.transform.localPosition, adjNode.transform.localPosition);

                    Image edgeImage = Instantiate(manager.edgePrefab, position, Quaternion.identity);

                    edgeImage.rectTransform.sizeDelta = new Vector2(distance - 20, 4f);

                    edgeImage.transform.SetParent(manager.parentEdge);
                    edgeImage.transform.localPosition = position;

                    Vector3 direction = (adjNode.transform.localPosition - node.transform.localPosition).normalized;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    edgeImage.rectTransform.rotation = Quaternion.Euler(0f, 0f, angle);
                }
            }
        }

        Debug.Log("SetEdge complete");
    }
}
