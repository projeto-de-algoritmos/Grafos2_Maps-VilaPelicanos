using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using static AlgorithmBFS;

public class Heap
{
    private Tuple<int, int, float>[] priorityQueue;
    private Dictionary<int, int> hash; // Elemento e id de sua posicao na priorityQueue
    private readonly int max;
    private int last;

    public Heap(int _max)
    {
        max = _max + 1;
        last = 0;
        priorityQueue = new Tuple<int, int, float>[max];
        hash = new Dictionary<int, int>();
    }


    public Tuple<int, int, float>[] PriorityQueue
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

    public int Enqueue(AlgorithmBFS.NewNode newElement, int father, float distance)
    {
        if (last == max - 1)
            return -1;

        if (hash.ContainsKey(newElement.id))
        {
            if (hash[newElement.id] == -1)
            {
                return 2;
            }

            if (priorityQueue[hash[newElement.id]].Item3 > distance)
            {
                priorityQueue[hash[newElement.id]] = Tuple.Create(newElement.id, father, distance);
                int pos = ShiftUp(hash[newElement.id]);
                hash[newElement.id] = pos;

                return 0;
            }

            return 1;
        }
        else
        {
            last++;
            priorityQueue[last] = Tuple.Create(newElement.id, father, distance);

            int pos = ShiftUp(last);
            hash.Add(newElement.id, pos);
            return 0;
        }
    }

    public Tuple<int, int> Dequeue()
    {
        if (last == 0)
            return Tuple.Create(-1, -1);

        Tuple<int, int, float> temp = priorityQueue[1];

        Swap(1, last);
        last--;

        Tuple<int, int, float> hashChange = priorityQueue[1];
        int pos = HeapiFy(1);
        hash[temp.Item1] = -1;
        hash[hashChange.Item1] = pos;

        return Tuple.Create(temp.Item1, temp.Item2);
    }

    private int ShiftUp(int pos)
    {
        if (pos != 1 && priorityQueue[pos].Item3 < priorityQueue[pos / 2].Item3)
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
            float menor = priorityQueue[index].Item3;

            if (last >= pos * 2 + 1 && priorityQueue[pos * 2 + 1].Item3 < menor)
            {
                index = pos * 2 + 1;
                menor = priorityQueue[index].Item3;
            }

            if (priorityQueue[pos].Item3 > menor)
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
