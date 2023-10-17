using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Xml.Linq;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AlgorithmBFS : MonoBehaviour
{
    
    [SerializeField]
    private Manager manager;
    private List<NewNode> newGraph = new();
    private int[][] matrixAdj;

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
            node1.nodesAdj.Add(node2);        

        if (!node2.nodesAdj.Contains(node1))
            node2.nodesAdj.Add(node1);
    }

    public void Filter(List<Node> graph, Node s, Node t, float min)
    {
        matrixAdj = new int[graph.Count][];

        for (int i = 0; i < graph.Count; i++)
        {
            matrixAdj[i] = new int[graph.Count];

            for (int j = 0; j < graph.Count; j++)
            {
                matrixAdj[i][j] = 0;
            }
        }

        NewNode newNode = Add_Node(s, t, 0);
        
        Queue<NewNode> queue = new();
        queue.Enqueue(newNode);


        while (queue.Any())
        {
            newNode = queue.Dequeue();

            s = newNode.node.Item1;
            t = newNode.node.Item2;

            foreach (Node sAdj in s.Edges.Keys)
            {
                if (matrixAdj[sAdj.Id][t.Id] == 0)
                {
                    float distance = Distance(sAdj, t);
                    if (distance <= min) continue; 
                        
                    NewNode temp = Add_Node(sAdj, t, distance);
                    matrixAdj[sAdj.Id][t.Id] = 1;
                    Add_Edge(newNode, temp);
                    queue.Enqueue(temp);
                }

                foreach (Node tAdj in t.Edges.Keys)
                {
                    if (matrixAdj[sAdj.Id][tAdj.Id] == 0)
                    {
                        float distance = Distance(sAdj, tAdj);
                        if (distance <= min) continue;

                        NewNode newEdge = Add_Node(sAdj, tAdj, distance);
                        matrixAdj[sAdj.Id][tAdj.Id] = 1;
                        Add_Edge(newNode, newEdge);
                        queue.Enqueue(newEdge);
                    }                    

                    if (matrixAdj[s.Id][tAdj.Id] == 0)
                    {
                        float distance = Distance(s, tAdj);
                        if (distance <= min) continue;

                        NewNode newEdge = Add_Node(s, tAdj, distance);
                        matrixAdj[s.Id][tAdj.Id] = 1;
                        Add_Edge(newNode, newEdge);
                        queue.Enqueue(newEdge);
                    }
                }
            }
        }
    }
    public Stack<NewNode> Dijkstra(Tuple<Node, Node> start, Tuple<Node, Node> end)
    {

        if (start.Equals(end)) return new Stack<NewNode>(); // incio igual ao fim
        
        Dictionary<Tuple<NewNode, float>, NewNode> minDistance = new();
        minDistance.Add(Tuple.Create(newGraph[0], 0f), newGraph[0]);

        Dictionary<int, int> MST = new();

        Heap heap = new(newGraph.Count);
        heap.Enqueue(newGraph[0], 0, 0f);
        
        while (heap.Last > 0)
        {
            Tuple<int, int> ids = heap.Dequeue();
            MST.Add(ids.Item1, ids.Item2);

            NewNode node = newGraph[ids.Item1];
            foreach (NewNode edge in node.nodesAdj)
            {
                float newDistance = node.node.Item3 + edge.node.Item3;
                if (MST.ContainsKey(edge.id)) continue;
                int response = heap.Enqueue(edge, node.id, newDistance);

                if(response == 0)
                {
                    minDistance.Add(Tuple.Create(edge, newDistance), node);              
                }             
            }

            // PrintHeap(heap);

            if (node.node.Item1.Id == end.Item1.Id && node.node.Item2.Id == end.Item2.Id)
            {
                Debug.Log("Uhuuuuuuul");           
                Stack<NewNode> path = new();

                int three = node.id;

                while (three != newGraph[0].id)
                {
                    path.Push(newGraph[three]);
                    three = MST[three];
                }
                // path.Insert(0, minDistance.Keys.ElementAt(0));
                return path;
            }
        }

        return new Stack<NewNode>(); // nao achou um caminho possivel em que nao se encontrassem
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
        // StartCoroutine(startAlgorithm());
    }

    IEnumerator startAlgorithm()
    {
        yield return new WaitForSeconds(1);
        
        Filter(manager.graph, manager.graph[87], manager.graph[37], 110f);
        Debug.Log(newGraph.Count);
        Stack<NewNode> caminho = Dijkstra(
            Tuple.Create(manager.graph[87], manager.graph[37]), Tuple.Create(manager.graph[37], manager.graph[87]));
        
        foreach (NewNode node in caminho)
        {
            Debug.Log(node.node.Item1 + ", " + node.node.Item2 + " -> ");
        }
    }

    public Stack<NewNode> MST(Tuple<Node, Node> start, Tuple<Node, Node> end, float minDistance)
    {
        Filter(manager.graph, start.Item1, start.Item2, minDistance);

        return Dijkstra(start, end);
    }

    public void PrintHeap(Heap heap)
    {
        Tuple<int, int, float>[] queue = heap.PriorityQueue;
        int max = heap.Last;
        for (int i = 1; i < max; i++)
        {
            if (queue[i] != null)
                Debug.Log("Queueeeeeeeee:  " + queue[1].Item3);
            else
                Debug.Log("DEEEEU MERDAAA");

            heap.Dequeue();            
        }
    }
}
