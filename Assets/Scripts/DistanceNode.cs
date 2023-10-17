using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public static class DistanceNode
{
    public static float Distance(Vector3 node1, Vector3 node2)
    {
        float a = Mathf.Abs(node1.x - node2.x);
        float b = Mathf.Abs(node1.y - node2.y);

        float result = Mathf.Sqrt(a * a + b * b);

        return result;
    }

    public static float Distance(Vector3 node1, Vector3 node2, Manager manager)
    {
        float a = Mathf.Abs(node1.x - node2.x);
        float b = Mathf.Abs(node1.y - node2.y);

        float result = Mathf.Sqrt(a * a + b * b);

        TextMeshProUGUI debug = manager.debug;
        debug.text += "node1.x: " + node1.x + "\n";
        debug.text += "node2.x: " + node2.x + "\n";
        debug.text += "result: " + result + "\n";

        return result;
    }
}
