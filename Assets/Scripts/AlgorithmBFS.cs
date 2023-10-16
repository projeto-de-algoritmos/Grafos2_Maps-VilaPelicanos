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
    public void Dijkstra(Tuple<Node, Node> start, Tuple<Node, Node> end)
    {
        /*
        int[] distancia = new int[newGraph.Count];
        for (int i = 0; i < newGraph.Count; i++)
        {
            distancia[i] = int.MaxValue;
        }

        distancia[newGraph[0].id] = 0;

        // adiciona o menor na distancia 

        Heap heap = new(newGraph.Count);

        if (start.Equals(end)) // inicio igual fim

        heap.Enqueue(newGraph[0]);
        // verifica se o menor é o final 
        // adiciona os adj do menor na heap
        
        PriorityQueue<Tuple<int, int>> filaPrioridade = new PriorityQueue<Tuple<int, int>>();
        filaPrioridade.Enqueue(new Tuple<int, int>(origem, 0));

        while (filaPrioridade.Count > 0)
        {
            Tuple<int, int> u = filaPrioridade.Dequeue();
            int uVertice = u.Item1;
            int uDistancia = u.Item2;

            if (uDistancia != distancia[uVertice])
            {
                continue;
            }

            foreach (var vizinho in adj[uVertice])
            {
                int vVertice = vizinho.Item1;
                int peso = vizinho.Item2;
                int novaDistancia = distancia[uVertice] + peso;

                if (novaDistancia < distancia[vVertice])
                {
                    distancia[vVertice] = novaDistancia;
                    filaPrioridade.Enqueue(new Tuple<int, int>(vVertice, novaDistancia));
                }
            }
        }

        // Imprimir as distâncias mínimas a partir da origem
        Console.WriteLine("Vértice\tDistância a partir da origem");
        for (int i = 0; i < V; i++)
        {
            Console.WriteLine(i + "\t" + distancia[i]);
        }
        */
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
