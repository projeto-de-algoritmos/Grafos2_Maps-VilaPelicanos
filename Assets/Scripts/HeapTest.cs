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

    private string Test01()
    {
        string response = "Test01: ";

        int maxSize = 10;
        Heap heap = new(maxSize);

        heap.Enqueue(1, 2, 3.5f);
        heap.Enqueue(2, 3, 2.2f);
        heap.Enqueue(1, 3, 4.0f);
        heap.Enqueue(6, 5, 4.8f);
        heap.Enqueue(7, 4, 1.2f);
        heap.Enqueue(3, 4, 3.9f);
        heap.Enqueue(2, 1, 4.0f);
        heap.Enqueue(8, 7, 4.0f);
        heap.Enqueue(4, 2, .5f);
        heap.Enqueue(7, 3, 9.1f);

        float value = -1;

        for (int i = 0; i < maxSize; i++)
        {
            float nextValue = heap.Dequeue().value;

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

        heap.Enqueue(1, 2, 3.5f);
        heap.Enqueue(2, 3, 2.2f);
        int result = heap.Enqueue(1, 3, 4.0f);

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

        heap.Enqueue(1, 2, 3.5f);
        heap.Enqueue(2, 3, 2.2f);
        heap.Enqueue(1, 3, 4.0f);
        int result = 0;

        for (int i = 0; i < maxSize + 1; i++)
        {
            result = (int)heap.Dequeue().value;
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

        heap.Enqueue(1, 2, 3.5f);
        heap.Enqueue(2, 3, 2.2f);
        heap.Enqueue(1, 3, 4.0f);
        heap.Enqueue(6, 5, 4.8f);
        heap.Enqueue(7, 4, 1.2f);
        heap.Enqueue(3, 4, 3.9f);
        heap.Enqueue(2, 1, 4.0f);
        heap.Enqueue(8, 7, 4.0f);
        heap.Enqueue(4, 2, .5f);
        heap.Enqueue(7, 3, 9.1f);

        float value = -1;

        for (int i = 0; i < maxSize - 4; i++)
        {
            float nextValue = heap.Dequeue().value;

            if (nextValue < value)
            {
                return response + failTest;
            }
        }

        heap.Enqueue(6, 9, 4.0f);
        heap.Enqueue(7, 1, 4.1f);
        heap.Enqueue(2, 9, .2f);
        heap.Enqueue(8, 9, 14.3f);

        int last = heap.Last;
        value = -1;

        for (int i = 0; i < last; i++)
        {
            float nextValue = heap.Dequeue().value;

            if (nextValue < value)
            {
                return response + failTest;
            }
        }

        return response + successTest;
    }
}
