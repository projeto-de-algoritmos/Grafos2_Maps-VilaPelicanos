using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using static AlgorithmBFS;

public class Heap
{
    private AlgorithmBFS.NewNode[] priorityQueue;
    private Dictionary<AlgorithmBFS.NewNode, int> hash; // Elemento e id de sua posicao na priorityQueue
    private readonly int max;
    private int last;

    public Heap(int _max)
    {
        max = _max + 1;
        last = 0;
        priorityQueue = new AlgorithmBFS.NewNode[max];
        hash = new Dictionary<AlgorithmBFS.NewNode, int>();
    }


    public AlgorithmBFS.NewNode[] PriorityQueue
    {
        get { return priorityQueue; }
        set { priorityQueue = value; }
    }

    public int Last
    {
        get { return last; }
        set { last = value; }
    }

    public int Enqueue(AlgorithmBFS.NewNode newElement)
    {
        if (last == max - 1)
            return -1;

        if (hash.ContainsKey(newElement))
        {
            if (hash[newElement] == -1)
            {
                return 0;
            }

            if (priorityQueue[hash[newElement]].node.Item3 > newElement.node.Item3)
            {
                newElement.node = Tuple.Create(newElement.node.Item1, newElement.node.Item2, newElement.node.Item3);
                int pos = ShiftUp(hash[newElement]);
                hash[newElement] = pos;
            }
        }
        else
        {
            last++;
            priorityQueue[last] = newElement;

            int pos = ShiftUp(last);
            hash.Add(newElement, pos);
        }

        return 0;
    }

    public AlgorithmBFS.NewNode Dequeue()
    {
        if (last == 0)
            return new AlgorithmBFS.NewNode(Tuple.Create(new Node(), new Node(), -1f), 0);

        Swap(1, last);

        AlgorithmBFS.NewNode temp = priorityQueue[last];
        last--;

        AlgorithmBFS.NewNode hashChange = priorityQueue[1];
        int pos = HeapiFy(1);

        hash[temp] = -1;
        hash[hashChange] = pos;

        return temp;
    }

    private int ShiftUp(int pos)
    {
        if (pos != 1 && priorityQueue[pos].node.Item3 < priorityQueue[pos/2].node.Item3)
        {
            Swap(pos, pos / 2);

            return ShiftUp(pos / 2);
        }

        return pos;
    }

    private int HeapiFy(int pos)
    {
        if (last >= pos * 2)
        {
            int index = pos * 2;
            float menor = priorityQueue[index].node.Item3;

            if (last >= pos * 2 + 1 && priorityQueue[pos * 2 + 1].node.Item3 < menor)
            {
                index = pos * 2 + 1;
                menor = priorityQueue[index].node.Item3;
            }

            if (priorityQueue[pos].node.Item3 > menor)
            {
                Swap(pos, index);
                return HeapiFy(index);
            }
        }

        return pos;
    }

    private void Swap(int a, int b)
    {
        (priorityQueue[a], priorityQueue[b]) =
            (priorityQueue[b], priorityQueue[a]);
    }
}
