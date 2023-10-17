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
        Debug.Log(TestObj());
        Debug.Log(Test01());
        /*
        Debug.Log(Test02());
        Debug.Log(Test03());
        Debug.Log(Test04());
        Debug.Log(Test05());
        */

        Debug.Log("-- END TESTES HEAP --");
    }

    private string TestObj()
    {
        string response = "TestObj: ";

        Node node = new()
        {
            Id = 1
        };

        Node[] array = new Node[2];

        array[0] = node;
        array[0].Id = 2;

        if (node.Id == 2)
            return response + successTest;

        return response + failTest;
    }

    private string Test01()
    {
        string response = "Test01: ";

        int maxSize = 10;
        Heap heap = new(maxSize);

        int result;

        result = heap.Enqueue(1, 2, 3.2f);
        if (result != 0)
            return response + failTest + ": 01";

        result = heap.Enqueue(1, 3, 2.2f);
        if (result != 3)
            return response + failTest + ": 02 | " + result;

        result = heap.Enqueue(3, 2, 4.0f);
        if (result != 0)
            return response + failTest + ": 03";

        result = heap.Enqueue(4, 5, 4.8f);
        if (result != 0)
            return response + failTest + ": 04";

        result = heap.Enqueue(3, 1, 1.2f);
        if (result != 3)
            return response + failTest + ": 05";

        result = heap.Enqueue(4, 1, 8.4f);
        if (result != 1)
            return response + failTest + ": 06";

        result = heap.Dequeue().Item1;
        if (result != 3) return response + failTest + ": 07";

        result = heap.Enqueue(3, 1, 2f);
        if (result != 2)
            return response + failTest + ": 08";

        result = heap.Enqueue(3, 1, 0f);
        if (result != 2)
            return response + failTest + ": 09";

        result = heap.Dequeue().Item1;
        if (result != 1) return response + failTest + ": 10";

        result = heap.Enqueue(3, 1, 0f);
        if (result != 2)
            return response + failTest + ": 11";

        result = heap.Enqueue(1, 1, 0f);
        if (result != 2)
            return response + failTest + ": 12";

        return response + successTest;
    }

    /*
    private string Test02()
    {
        string response = "Test02: ";

        int maxSize = 2;
        Heap heap = new(maxSize);

        heap.Enqueue(CreateElement(3.5f), 3.5f);
        heap.Enqueue(CreateElement(2.2f), 2.2f);
        int result = heap.Enqueue(CreateElement(4.0f), 4.0f);

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

        heap.Enqueue(CreateElement(3.5f), 3.5f);
        heap.Enqueue(CreateElement(2.2f), 2.2f);
        heap.Enqueue(CreateElement(4.0f), 4.0f);
        int result = 0;

        for (int i = 0; i < maxSize + 1; i++)
        {
            result = (int)heap.Dequeue();
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

        heap.Enqueue(CreateElement(3.5f), 3.5f);
        heap.Enqueue(CreateElement(2.2f), 2.2f);
        heap.Enqueue(CreateElement(4.0f), 4.0f);
        heap.Enqueue(CreateElement(4.8f), 4.8f);
        heap.Enqueue(CreateElement(1.2f), 1.2f);
        heap.Enqueue(CreateElement(3.9f), 3.9f);
        heap.Enqueue(CreateElement(4.0f), 4.0f);
        heap.Enqueue(CreateElement(4.0f), 4.0f);
        heap.Enqueue(CreateElement(.5f), .5f);
        heap.Enqueue(CreateElement(9.1f), 9.1f);

        float value = -1;

        for (int i = 0; i < maxSize - 4; i++)
        {
            float nextValue = heap.Dequeue();

            if (nextValue < value)
            {
                return response + failTest;
            }
        }

        heap.Enqueue(CreateElement(4.0f), 4.0f);
        heap.Enqueue(CreateElement(4.1f), 4.1f);
        heap.Enqueue(CreateElement(.2f), .2f);
        heap.Enqueue(CreateElement(14.3f), 14.3f);

        int last = heap.Last;
        value = -1;

        for (int i = 0; i < last; i++)
        {
            float nextValue = heap.Dequeue();

            if (nextValue < value)
            {
                return response + failTest;
            }
        }

        return response + successTest;
    }

    private string Test05()
    {
        string response = "Test05: ";

        int maxSize = 10;
        Heap heap = new(maxSize);

        Node node1 = new Node();
        node1.Id = 1;
        Node node2 = new Node();
        node2.Id = 2;

        heap.Enqueue(CreateElement(node1, node2, 3.5f), 3.5f);
        heap.Dequeue();
        int result = heap.Enqueue(CreateElement(node1, node2, 2.2f), 2.2f);

        if (result != 1)
            return response + failTest + "-> Expected: 2 | " + result;

        result = heap.Enqueue(CreateElement(node2, node1, 4.0f), 4.0f);
        if (result != 0)
            return response + failTest + ": 2";

        return response + failTest;

    }
    */
}
