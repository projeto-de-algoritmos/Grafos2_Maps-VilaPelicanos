using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    [SerializeField]
    public Image edgePrefab;

    [SerializeField]
    public Transform parentEdge;

    [SerializeField]
    private GameObject rectanglePrefab;

    public AlgorithmBFS algorithBFS;
    public Game game;

    public Slider speedSlider;
    public float speed = 1;
    public TextMeshProUGUI speedValue;

    public List<Node> graph;
    public Node startCharacter01;
    public Node endCharacter01;
    public Node startCharacter02;
    public Node endCharacter02;
    public int friendship;
    public Slider slider;
    public TextMeshProUGUI valueFriendship;

    [SerializeField]
    public int[][] matrixAdj;

    // Start is called before the first frame update
    void Start()
    {
        slider.onValueChanged.AddListener(UpdateSliderValue);
        speedSlider.onValueChanged.AddListener(UpdateSpeedValue);

        graph ??= new List<Node>();
    }

    void UpdateSpeedValue(float newValue)
    {
        speed = ((int)newValue);
        speedValue.text = speed.ToString();
    }

    public void StartGame()
    {
        /*
        List<Node> nodes = algorithBFS.BFS(graph, startCharacter01, endCharacter01);

        if (nodes.Count != 0)
            game.CreateCharacter(nodes[0], nodes);

        */
    }

    // Função chamada quando o valor do slider é alterado.
    void UpdateSliderValue(float newValue)
    {
        friendship = ((int)newValue);
        valueFriendship.text = friendship.ToString();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
