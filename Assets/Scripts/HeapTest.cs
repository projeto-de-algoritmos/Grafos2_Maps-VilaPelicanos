using System;
using UnityEditor;
using UnityEngine;

public class HeapTest : MonoBehaviour
{
    private readonly string failTest = "fail";
    private readonly string successTest = "success";

    private void Start()
    {
        Debug.Log("---- TESTES HEAP ----");

        Debug.Log(Test01());
        Debug.Log(Test02());
        Debug.Log(Test03());
        Debug.Log(Test04());

        Debug.Log("-- END TESTES HEAP --");
    }

    private AlgorithmBFS.NewNode CreateElement(float distance)
    {
        AlgorithmBFS.NewNode nodeReturn = new(Tuple.Create(new Node(), new Node(), distance), 1);
        return nodeReturn;
    }

    private string Test01()
    {
        string response = "Test01: ";

        int maxSize = 10;
        Heap heap = new(maxSize);

        heap.Enqueue(CreateElement(3.2f));
        heap.Enqueue(CreateElement(2.2f));
        heap.Enqueue(CreateElement(4.0f));
        heap.Enqueue(CreateElement(4.8f));
        heap.Enqueue(CreateElement(1.2f));
        heap.Enqueue(CreateElement(3.9f));
        heap.Enqueue(CreateElement(4.0f));
        heap.Enqueue(CreateElement(4.0f));
        heap.Enqueue(CreateElement(.5f));
        heap.Enqueue(CreateElement(9.1f));

        float value = -1;

        for (int i = 0; i < maxSize; i++)
        {
            float nextValue = heap.Dequeue().node.Item3;

            if (nextValue < value)
            {
                return response + failTest;
            }
        }

        return response + successTest;
    }

    private string Test02()
    {
        string response = "Test02: ";

        int maxSize = 2;
        Heap heap = new(maxSize);

        heap.Enqueue(CreateElement(3.5f));
        heap.Enqueue(CreateElement(2.2f));
        int result = heap.Enqueue(CreateElement(4.0f));

        if (result == -1)
            return response + successTest;
        else
            return response + failTest;
    }

    private string Test03()
    {
        string response = "Test03: ";

        int maxSize = 2;
        Heap heap = new(maxSize);

        heap.Enqueue(CreateElement(3.5f));
        heap.Enqueue(CreateElement(2.2f));
        heap.Enqueue(CreateElement(4.0f));
        int result = 0;

        for (int i = 0; i < maxSize + 1; i++)
        {
            result = (int)heap.Dequeue().node.Item3;
        }

        if (result == -1)
            return response + successTest;
        else
            return response + failTest;
    }

    private string Test04()
    {
        string response = "Test04: ";

        int maxSize = 10;
        Heap heap = new(maxSize);

        heap.Enqueue(CreateElement(3.5f));
        heap.Enqueue(CreateElement(2.2f));
        heap.Enqueue(CreateElement(4.0f));
        heap.Enqueue(CreateElement(4.8f));
        heap.Enqueue(CreateElement(1.2f));
        heap.Enqueue(CreateElement(3.9f));
        heap.Enqueue(CreateElement(4.0f));
        heap.Enqueue(CreateElement(4.0f));
        heap.Enqueue(CreateElement(.5f));
        heap.Enqueue(CreateElement(9.1f));

        float value = -1;

        for (int i = 0; i < maxSize - 4; i++)
        {
            float nextValue = heap.Dequeue().node.Item3;

            if (nextValue < value)
            {
                return response + failTest;
            }
        }

        heap.Enqueue(CreateElement(4.0f));
        heap.Enqueue(CreateElement(4.1f));
        heap.Enqueue(CreateElement(.2f));
        heap.Enqueue(CreateElement(14.3f));

        int last = heap.Last;
        value = -1;

        for (int i = 0; i < last; i++)
        {
            float nextValue = heap.Dequeue().node.Item3;

            if (nextValue < value)
            {
                return response + failTest;
            }
        }

        return response + successTest;
    }
}
