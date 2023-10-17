using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Characters : MonoBehaviour
{
    public Sprite imageChar;
    public Game game;
    public Manager manager;

    public void StartChar(List<Node> nodes, List<Node> nodes2, Manager _manager)
    {
        manager = _manager;
        StartCoroutine(Movendo(nodes, nodes2, manager.friendship));
    }

    IEnumerator Movendo(List<Node> nodes, List<Node> nodes2, float min)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            gameObject.LeanMoveLocal(nodes[i].transform.localPosition, 2f / manager.speed).setEaseInOutQuad();                                 

            yield return new WaitForSeconds(2f/manager.speed);
        }

        manager.finishChars++;

        while (manager.finishChars != 2)
            yield return null;

        yield return new WaitForSeconds(3);

        manager.finishChars--;
        Destroy(gameObject);

    }

    public void SelectedChar()
    {
        game.SelectionCharacter(this);
    }
}
