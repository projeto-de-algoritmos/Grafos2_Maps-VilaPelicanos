using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using static AlgorithmBFS;

public class Heap
{
    private Tuple<int, float>[] priorityQueue;
    private Dictionary<int, int> hash; // Elemento e id de sua posicao na priorityQueue
    private readonly int max;
    private int last;

    public Heap(int _max)
    {
        max = _max + 1;
        last = 0;
        priorityQueue = new Tuple<int, float>[max];
        hash = new Dictionary<int, int>();
    }


    public Tuple<int, float>[] PriorityQueue
    {
        get { return priorityQueue; }
        set { priorityQueue = value; }
    }

    public int Last
    {
        get { return last; }
        set { last = value; }
    }

    public Dictionary<int, int> Hash
    {
        get { return hash; }
        set { hash = value; }
    }

    public int Enqueue(AlgorithmBFS.NewNode newElement, float distance)
    {
        if (last == max - 1)
            return -1;

        if (hash.ContainsKey(newElement.id))
        {
            if (hash[newElement.id] == -1)
            {
                return 2;
            }

            if (priorityQueue[hash[newElement.id]].Item2 > distance)
            {
                newElement.node = Tuple.Create(newElement.node.Item1, newElement.node.Item2, distance);
                int pos = ShiftUp(hash[newElement.id]);
                hash[newElement.id] = pos;

                return 1;
            }
        }
        else
        {
            last++;
            priorityQueue[last] = Tuple.Create(newElement.id, newElement.node.Item3);

            int pos = ShiftUp(last);
            hash.Add(newElement.id, pos);
        }

        return 0;
    }

    public int Dequeue()
    {
        if (last == 0)
            return -1;

        Swap(1, last);

        Tuple<int, float> temp = priorityQueue[last];
        last--;

        Tuple<int, float> hashChange = priorityQueue[1];
        int pos = HeapiFy(1);

        hash[temp.Item1] = -1;
        hash[hashChange.Item1] = pos;

        return temp.Item1;
    }

    private int ShiftUp(int pos)
    {
        if (pos != 1 && priorityQueue[pos].Item2 < priorityQueue[pos / 2].Item2)
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
            float menor = priorityQueue[index].Item2;

            if (last >= pos * 2 + 1 && priorityQueue[pos * 2 + 1].Item2 < menor)
            {
                index = pos * 2 + 1;
                menor = priorityQueue[index].Item2;
            }

            if (priorityQueue[pos].Item2 > menor)
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
