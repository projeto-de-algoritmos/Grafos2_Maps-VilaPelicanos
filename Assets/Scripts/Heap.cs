using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class Heap
{
    public struct ElementQueue
    {
        public Tuple<int, int> element; // id dos pares de nodes
        public float value; // Soma das distancia das arestas do Node1 com seu adj + Node2 com seu adj

        public ElementQueue(Tuple<int, int> _element, float _value)
        {
            element = _element;
            value = _value;
        }
    }

    private ElementQueue[] priorityQueue;
    private Dictionary<ElementQueue, int> hash; // Elemento e id de sua posicao na priorityQueue
    private readonly int max;
    private int last;

    public Heap(int _max)
    {
        max = _max + 1;
        last = 0;
        priorityQueue = new ElementQueue[max];
        hash = new Dictionary<ElementQueue, int>();
    }


    public ElementQueue[] PriorityQueue
    {
        get { return priorityQueue; }
        set { priorityQueue = value; }
    }

    public int Last
    {
        get { return last; }
        set { last = value; }
    }

    public int Enqueue(int node1, int node2, float _value)
    {
        if (last == max - 1)
            return -1;

        Tuple<int, int> newNodes = new Tuple<int, int>(node1, node2);
        ElementQueue newElement = new(newNodes, _value);

        if (hash.ContainsKey(newElement))
        {
            if (hash[newElement] == -1)
            {
                return 0;
            }

            if (priorityQueue[hash[newElement]].value > newElement.value)
            {
                priorityQueue[hash[newElement]].value = newElement.value;
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

    public ElementQueue Dequeue()
    {
        if (last == 0)
            return new ElementQueue(Tuple.Create(-1, -1), (int)-1);

        Swap(1, last);
        
        ElementQueue temp = priorityQueue[last];
        last--;

        ElementQueue hashChange = priorityQueue[1];
        int pos = HeapiFy(1);

        hash[temp] = -1;
        hash[hashChange] = pos;

        return temp;
    }

    private int ShiftUp(int pos)
    {
        if (pos != 1 && priorityQueue[pos].value < priorityQueue[pos/2].value)
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
            float menor = priorityQueue[index].value;

            if (last >= pos * 2 + 1 && priorityQueue[pos * 2 + 1].value < menor)
            {
                index = pos * 2 + 1;
                menor = priorityQueue[index].value;
            }

            if (priorityQueue[pos].value > menor)
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
