using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using static AlgorithmBFS;

public class Heap: MonoBehaviour
{
    private Tuple<int, int, float>[] priorityQueue;

    [SerializeField]
    private int[] hash; // Elemento e id de sua posicao na priorityQueue
    private readonly int max;
    private int last;

    public Heap(int _max)
    {
        max = _max + 1;
        last = 0;
        priorityQueue = new Tuple<int, int, float>[max];
        hash = new int[max];

        for (int i = 0; i < max; i++)
            hash[i] = -1;
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

    public int[] Hash
    {
        get { return hash; }
        set { hash = value; }
    }

    public int Enqueue(int element, int father, float distance)
    {
        if (last == max - 1)
            return -1;

        if (hash[element] != -1)
        {
            if (hash[element] == -2)
            {
                return 2; // Ja foi retirado
            }

            if (priorityQueue[hash[element]].Item3 > distance)
            {
                priorityQueue[hash[element]] = Tuple.Create(element, father, distance);
                int pos = ShiftUp(hash[element]);
                hash[element] = pos;

                return 3; // Foi atualizado
            }

            return 1; // Nao foi atualizado
        }
        else
        {
            last++;
            priorityQueue[last] = Tuple.Create(element, father, distance);

            int pos = ShiftUp(last);
            hash[element] = pos;

            return 0; // Criado novo no
        }
    }

    public Tuple<int, int> Dequeue()
    {
        if (last == 0) return Tuple.Create(-1, -1, -1f);
        
        Swap(1, last);
        
        Tuple<int, int, float> temp = priorityQueue[last];

        last--;

        HeapiFy(1);

        hash[temp.Item1] = -2;

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
        hash[priorityQueue[a].Item1] = b;
        hash[priorityQueue[b].Item1] = a;

        (priorityQueue[a], priorityQueue[b]) =
            (priorityQueue[b], priorityQueue[a]);
    }
}
