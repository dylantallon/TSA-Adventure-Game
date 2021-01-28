﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElectricGame : MonoBehaviour
{
    public GameObject piecePrefab;
    public Image puzzleBoard;
    public Text instructions;
    public static List<Vector2> correctPositions = new List<Vector2>();
    private RectTransform rectTransform;
    private int mover;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector3(0, -435, 0);
        StartCoroutine(ElecGameWait(true));
        PuzzlePiece.prefabCount = 1;
        correctPositions.Add(new Vector2(90, 90));
        correctPositions.Add(new Vector2(0, -90));
        correctPositions.Add(new Vector2(-90, -90));
        correctPositions.Add(new Vector2(90, 0));
        correctPositions.Add(new Vector2(0, 0));
        correctPositions.Add(new Vector2(-90, 0));
        correctPositions.Add(new Vector2(-90, 90));
        correctPositions.Add(new Vector2(90, -90));
        correctPositions.Add(new Vector2(0, 90));
    }

    public IEnumerator ElecGameWait(bool start)
    {
        yield return new WaitUntil(() => PlayerRaycast.elecGameOn == true);
        mover = 0;
        if(start)
        {
            for(int i = 0; i < 9; i++)
            {
                Instantiate(piecePrefab, Vector3.zero, Quaternion.identity, puzzleBoard.transform);
            }
            for (int i = 29; i > 0; i--)
            {
                mover += i;
                rectTransform.anchoredPosition = new Vector3(0, -435 + mover, 0);
                yield return new WaitForSeconds(0.01f);
            }
            yield return new WaitUntil(() => PuzzlePiece.inPlace == 9);
            instructions.text = "Complete!";
            yield return new WaitForSeconds(1.5f);
            for (int i = 1; i <= 29; i++)
            {
                mover -= i;
                rectTransform.anchoredPosition = new Vector3(0, mover, 0);
                yield return new WaitForSeconds(0.01f);
            }
            PlayerRaycast.elecGameOn = false;
        }
    }
}
