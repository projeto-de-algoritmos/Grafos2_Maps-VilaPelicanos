using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AlgorithmBFS : MonoBehaviour
{
    
    [SerializeField]
    private Manager manager;
    private List<NewNode> newGraph = new();

    struct NewNode
    {
        public Tuple<Node, Node> node;
        public List<NewNode> nodesAdj;

        public NewNode(Tuple<Node, Node> node)
        {
            this.node = node;
            this.nodesAdj = new List<NewNode>();
        }
    }

    private NewNode Add_Node(Node node1, Node node2)
    {
        Tuple<Node, Node> node = Tuple.Create(node1, node2);

        NewNode newNode = new NewNode(node);

        if (!newGraph.Contains(newNode))
        {
            newGraph.Add(newNode);
            return newNode;
        }
        return newGraph[newGraph.IndexOf(newNode)];
    }

    private void Add_Edge(NewNode node1, NewNode node2)
    {
        if (!node1.nodesAdj.Contains(node2))
        {
            node1.nodesAdj.Add(node2);
        }
    }

    public void Filter(List<Node> graph, Node s, Node t, float min)
    {
        NewNode newNode = Add_Node(s, t);

        Queue<NewNode> queue = new();
        queue.Enqueue(newNode);

        while (queue.Any())
        {
            newNode = queue.Dequeue();

            s = newNode.node.Item1;
            t = newNode.node.Item2;

            for (Node node = s; ; node = t)
            {
                foreach (Node nodeAdj in node.Edges.Keys)
                {
                    float path;
                    NewNode no = new();

                    if (node.Equals(s))
                    {
                        path = Vector2.Distance(nodeAdj.transform.position, t.transform.position);    // passar o djkstra para ver se a distancia entre os dois pontos não é menor que o permitido
                        if (path <= min) continue;
                        no = Add_Node(nodeAdj, t);
                    }
                    else
                    {
                        path = Vector2.Distance(nodeAdj.transform.position, t.transform.position);    // passar o djkstra para ver se a distancia entre os dois pontos não é menor que o permitido
                        if (path <= min) continue;
                        no = Add_Node(t, nodeAdj);
                    }

                    Add_Edge(newNode, no);

                    if (!newGraph.Contains(no))
                    {
                        queue.Enqueue(no);
                    }

                }

                if (node.Equals(t)) break;
            }
        }
    }

    public void Dijkstra(int origem)
    {
        
        
    }

    public void Start()
    {
        StartCoroutine(startAlgorithm());
    }

    IEnumerator startAlgorithm()
    {
        yield return new WaitForSeconds(5);

        Filter(manager.graph, manager.graph[47], manager.graph[4], 5);

        /*
        List<Node> caminho; = BFS(manager.graph, manager.graph[47], manager.graph[4]);

        foreach (Node node in caminho)
        {
            Debug.Log(node.getId() + "->");
        }
        */
    }
}
