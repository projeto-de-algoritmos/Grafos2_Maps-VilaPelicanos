using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AlgorithmBFS : MonoBehaviour
{

    [SerializeField]
    private Manager manager;

    [SerializeField]
    private Heap heap;

    private List<NewNode> newGraph = new();
    private NewNode[][] matrixAdj;

    public struct NewNode
    {
        public int id;
        public Tuple<Node, Node, float> node;
        public List<NewNode> nodesAdj;

        public NewNode(Tuple<Node, Node, float> _node, int _id)
        {
            node = _node;
            nodesAdj = new List<NewNode>();
            id = _id;
        }

    }

    private Tuple<int, NewNode> Add_Node(Node node1, Node node2, float edgeSize)
    {
        Tuple<Node, Node, float> node = Tuple.Create(node1, node2, edgeSize);

        NewNode newNode = new NewNode(node, newGraph.Count);

        if (matrixAdj[node1.Id][node2.Id].id == -1) // mudar para algo O(1) | Mudado!
        {
            newGraph.Add(newNode);
            return Tuple.Create(1, newNode);
        }
        // mudar a matriz para NewNode para armazenar o obj
        return Tuple.Create(-1, matrixAdj[node1.Id][node2.Id]);
    }

    private void Add_Edge(NewNode node1, NewNode node2)
    {
        if (!node1.nodesAdj.Contains(node2))
            node1.nodesAdj.Add(node2);
    }

    public void Filter(List<Node> graph, Node s, Node t, float min)
    {
        if (s == null || t == null) return;

        matrixAdj = new NewNode[graph.Count][];

        for (int i = 0; i < graph.Count; i++)
        {
            matrixAdj[i] = new NewNode[graph.Count];

            for (int j = 0; j < graph.Count; j++)
            {
                matrixAdj[i][j] = new(null, -1);
            }
        }

        NewNode newNode = Add_Node(s, t, 0).Item2;

        Queue<NewNode> queue = new();
        queue.Enqueue(newNode);

        while (queue.Any())
        {
            newNode = queue.Dequeue();

            s = newNode.node.Item1;
            t = newNode.node.Item2;

            for (Node node = s, node2 = t; ; node = t, node2 = s)
            {

                foreach (KeyValuePair<Node, float> nodeAdj in node.Edges)
                {
                    Tuple<int, NewNode> no;

                    if (node.Equals(s))
                    {
                        float path = Distance(nodeAdj.Key, t);
                        if (path < min) continue;

                        no = Add_Node(nodeAdj.Key, t, nodeAdj.Value);
                        if (no.Item1 != -1)
                        {
                            matrixAdj[nodeAdj.Key.Id][t.Id] = no.Item2;
                            queue.Enqueue(no.Item2);
                        }
                    }
                    else
                    {
                        float path = Distance(s, nodeAdj.Key);
                        if (path < min) continue;

                        no = Add_Node(s, nodeAdj.Key, nodeAdj.Value);

                        if (no.Item1 != -1)
                        {
                            matrixAdj[s.Id][nodeAdj.Key.Id] = no.Item2;
                            queue.Enqueue(no.Item2);
                        }
                    }

                    Add_Edge(newNode, no.Item2);                    
                }

                if (node.Equals(t)) break;
            }
        }
    }

    public List<NewNode> Dijkstra(Tuple<Node, Node> start, Tuple<Node, Node> end)
    {
        if (start.Equals(end) || end.Item1 == null || end.Item2 == null) return new List<NewNode>(); // incio igual ao fim

        Dictionary<int, int> MST = new();

        heap = new(newGraph.Count);
        heap.Enqueue(newGraph[0].id, 0, 0f);

        while (heap.Last > 0)
        {
            Tuple<int, int, float> ids = heap.Dequeue();

            if (MST.ContainsKey(ids.Item1)) return new List<NewNode>();
            MST.Add(ids.Item1, ids.Item2);

            NewNode node = newGraph[ids.Item1];

            foreach (NewNode edge in node.nodesAdj)
            {
                float newDistance = ids.Item3 + edge.node.Item3;
                heap.Enqueue(edge.id, node.id, newDistance);

            }

            if (node.node.Item1.Id == end.Item1.Id && node.node.Item2.Id == end.Item2.Id)
            {
                List<NewNode> path = new();

                int three = node.id;

                while (three != newGraph[0].id)
                {
                    path.Insert(0, newGraph[three]);
                    three = MST[three];
                }
                path.Insert(0, newGraph[three]);
                return path;
            }
        }

        return new List<NewNode>(); // nao achou um caminho possivel em que nao se encontrassem
    }

    private float Distance(Tuple<Node, Node> element)
    {
        return Vector2.Distance(element.Item1.transform.position, element.Item2.transform.position);
    }

    private float Distance(Node node1, Node node2)
    {
        return Vector2.Distance(node1.transform.position, node2.transform.position);
    }

    public void Start()
    {
        //StartCoroutine(startAlgorithm());
    }

    IEnumerator startAlgorithm()
    {
        yield return new WaitForSeconds(1);

        Filter(manager.graph, manager.graph[87], manager.graph[37], 110f);
        List<NewNode> caminho = Dijkstra(
            Tuple.Create(manager.graph[87], manager.graph[37]), Tuple.Create(manager.graph[37], manager.graph[87]));

        foreach (NewNode node in caminho)
        {
            Debug.Log(node.node.Item1 + ", " + node.node.Item2 + " -> ");
        }
    }

    public List<NewNode> MST(Tuple<Node, Node> start, Tuple<Node, Node> end, float minDistance)
    {
        newGraph = new();

        Filter(manager.graph, start.Item1, start.Item2, minDistance);

        return Dijkstra(start, end);
    }
}