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

        int numNodes = manager.graph.Count;

        manager.matrixAdj = new int[numNodes][];

        for (int i = 0; i < numNodes; i++)
        {
            manager.matrixAdj[i] = new int[numNodes];
            manager.graph[i].setId(i);

            for (int j = 0; j < numNodes; j++)
            {
                manager.matrixAdj[i][j] = 0;
            }
        }

        foreach (Node node in manager.graph)
        {
            int currentNodeId = node.getId();

            foreach (Node edge in node.Edges.Keys)
            {
                Node adjNode = edge;
                int adjNodeId = adjNode.getId();

                manager.matrixAdj[currentNodeId][adjNodeId] = 1;

                if (!adjNode.isAdjacente(node))
                {
                    adjNode.setNodeAdj(node);
                }

                if (manager.matrixAdj[adjNodeId][currentNodeId] != 1)
                {
                    // Calcule a posição média entre os dois nós adjacentes.
                    Vector3 position = (node.transform.localPosition + adjNode.transform.localPosition) / 2f;

                    // Calcule a distância entre os nós adjacentes.
                    float distance = Vector3.Distance(node.transform.localPosition, adjNode.transform.localPosition);

                    // Crie a imagem retangular usando o prefab.
                    Image edgeImage = Instantiate(manager.edgePrefab, position, Quaternion.identity);

                    // Defina o tamanho da imagem retangular com base na distância entre os nós adjacentes.
                    edgeImage.rectTransform.sizeDelta = new Vector2(distance - 20, 4f); // 4f é a espessura da linha.

                    edgeImage.transform.SetParent(manager.parentEdge);
                    edgeImage.transform.localPosition = position;

                    // Certifique-se de que a imagem retangular está alinhada com a linha entre os nós.
                    Vector3 direction = (adjNode.transform.localPosition - node.transform.localPosition).normalized;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    edgeImage.rectTransform.rotation = Quaternion.Euler(0f, 0f, angle);
                }
            }
        }

        Debug.Log("SetEdge complete");
    }
}
