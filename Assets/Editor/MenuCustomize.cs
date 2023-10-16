using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class MenuCustomize : EditorWindow
{
    [MenuItem("Grafo/Atualiza as arestas")]
    public static void LoadEdges()
    {
        GameObject manager = GameObject.Find("Manager");

        if (manager.GetComponent<Manager>())
            SetEdges(manager.GetComponent<Manager>());
        else
            Debug.Log("Objeto 'Manager' nao encontrado");

    }

    [MenuItem("Grafo/Deleta as arestas")]
    public static void DeleteEdges()
    {
        GameObject manager = GameObject.Find("Manager");

        if (manager.GetComponent<Manager>())
            DeleteEdges(manager.GetComponent<Manager>());
        else
            Debug.Log("Objeto 'Manager' nao encontrado");

    }

    /*
    [MenuItem("Grafo/Atualiza as novas arestas")]
    public static void UpdateEdges()
    {
        GameObject manager = GameObject.Find("Manager");

        if (manager.GetComponent<Manager>())
            UpdateEdge(manager.GetComponent<Manager>());
        else
            Debug.Log("Objeto 'Manager' nao encontrado");

    }

    public static void UpdateEdge(Manager manager)
    {
        foreach (Node node in manager.graph)
        {
            foreach (Node edge in node.getNodesAdj())
            {
                node.AddEdge(edge, Vector2.Distance(edge.transform.transform.position, node.transform.position));
            }
        }

        Debug.Log("Update complete");
    }
    */

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
                    Debug.Log(distancereal);
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
