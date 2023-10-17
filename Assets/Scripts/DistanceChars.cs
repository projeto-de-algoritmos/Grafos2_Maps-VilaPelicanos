using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class DistanceChars : MonoBehaviour
{
    public Game game; // Referência ao seu script Game
    public Image edgeImage; // Referência à imagem da barra de distância

    // Update is called once per frame
    void Update()
    {
        Vector3 posChar01;
        Vector3 posChar02;

        if (game.charObj02 != null && game.charObj02.gameObject != null)
        {
            posChar01 = game.charObj01.transform.localPosition;
            posChar02 = game.charObj02.transform.localPosition;
        }
        else
        {
            posChar01 = Vector3.zero;
            posChar02 = Vector3.zero;
        }

        Vector3 position = (posChar01 + posChar02) / 2f;

        float distance = game.manager.friendship;

        edgeImage.rectTransform.sizeDelta = new Vector2(distance, 10f);

        edgeImage.transform.localPosition = position;

        Vector3 direction = (posChar01 - posChar02).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        edgeImage.rectTransform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}

