using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static AlgorithmBFS;

public class AlgorithmBFS : MonoBehaviour
{
    
    [SerializeField]
    private Manager manager;
    private List<NewNode> newGraph = new();

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

    private NewNode Add_Node(Node node1, Node node2, float edgeSize)
    {
        Tuple<Node, Node, float> node = Tuple.Create(node1, node2, edgeSize);

        NewNode newNode = new NewNode(node, newGraph.Count);

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
        NewNode newNode = Add_Node(s, t, 0);
        
        Queue<NewNode> queue = new();
        queue.Enqueue(newNode);

        while (queue.Any())
        {
            newNode = queue.Dequeue();

            s = newNode.node.Item1;
            t = newNode.node.Item2;

            for (Node node = s; ; node = t)
            {
                
                foreach (KeyValuePair<Node, float> nodeAdj in node.Edges)
                {   
                    NewNode no = new();

                    if (node.Equals(s))
                    {
                        float path = Distance(nodeAdj.Key, t);
                        if (path <= min) continue;

                        no = Add_Node(nodeAdj.Key, t, nodeAdj.Value);
                        Add_Edge(newNode, no);
                    }
                    else
                    {
                        float path = Distance(s, nodeAdj.Key);
                        if (path <= min) continue;

                        no = Add_Node(s, nodeAdj.Key, nodeAdj.Value);
                        Add_Edge(newNode, no);
                    }
                    
                    if (!newGraph.Contains(no))
                    {
                        queue.Enqueue(no);
                    }

                }

                if (node.Equals(t)) break;
            }
        }
    }
    public List<Tuple<NewNode, float>> Dijkstra(Tuple<Node, Node> start, Tuple<Node, Node> end)
    {

        if (start.Equals(end)) return new List<Tuple<NewNode, float>>(); // incio igual ao fim

        Dictionary<Tuple<NewNode, float>, NewNode> minDistance = new();
        minDistance.Add(Tuple.Create(newGraph[0], 0f), newGraph[0]);
        
        Heap heap = new(newGraph.Count);
        heap.Enqueue(newGraph[0]);

        while (heap.Last > 0)
        {
            NewNode node = heap.Dequeue();

            foreach (NewNode edge in node.nodesAdj)
            {
                int pos = heap.Hash[edge];

                float newDistance = node.node.Item3 + edge.node.Item3;

                if(pos == -1)
                {
                    minDistance.Add(Tuple.Create(edge, newDistance), node);
                    heap.Enqueue(edge);

                }else if (newDistance < heap.PriorityQueue[pos].node.Item3)
                {
                    minDistance.Remove(Tuple.Create(edge, heap.PriorityQueue[pos].node.Item3)); // nao consegui editar fiquei com raiva removi e to adicionando dnv com as alterações

                    NewNode newEdge = Add_Node(edge.node.Item1, edge.node.Item2, newDistance);
                    minDistance.Add(Tuple.Create(newEdge, newDistance), node);

                    heap.PriorityQueue[pos] = newEdge;

                }
            }

            if (node.node.Item1.Equals(end.Item1) && node.node.Item2.Equals(end.Item2))
            {
                KeyValuePair<Tuple<NewNode, float>, NewNode> t = minDistance.FirstOrDefault(x => x.Value.Equals(node));
                List<Tuple<NewNode, float>> path = new();

                while (!t.Key.Item1.Equals(newGraph[0]))
                {

                    path.Insert(0, t.Key);
                    t = minDistance.FirstOrDefault(x => x.Value.Equals(minDistance[t.Key]));

                }
                path.Insert(0, minDistance.Keys.ElementAt(0));
                return path;
            }
        }

        return new List<Tuple<NewNode, float>>(); // nao achou um caminho possivel em que nao se encontrassem
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
        StartCoroutine(startAlgorithm());
    }

    IEnumerator startAlgorithm()
    {
        yield return new WaitForSeconds(5);

        Filter(manager.graph, manager.graph[47], manager.graph[4], 5);

        List<Tuple<NewNode, float>> caminho = Dijkstra(Tuple.Create(manager.graph[47], manager.graph[4]), Tuple.Create(manager.graph[4], manager.graph[47]));

        foreach (Tuple<NewNode, float> node in caminho)
        {
            Debug.Log(node.Item1.node.Item1 + ", " + node.Item1.node.Item2 + ", " + node.Item2 + "->");
        }
    }
}
