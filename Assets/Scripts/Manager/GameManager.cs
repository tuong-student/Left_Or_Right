using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using NOOD;

public class GameManager : MonoBehaviorInstance<GameManager>
{
    public Action OnGameStart;

    string correctTown;
    [HideInInspector] public List<string> routes = new List<string>();
    private string _chosenTown = "";
    public string chosenTown => _chosenTown;

    int townIndex = 0;

    #region Bool
    private bool _isGameStart = false;
    public bool isGameStart => _isGameStart;
    public bool isWrong = false;
    public bool isArriveHome = false;
    #endregion

    private void Start()
    {
        routes = RouteManager.Instance.CreateRandomRoute(5);
        DisplayTown();
        OnGameStart += () => { _isGameStart = true; };
    }

    private void Update()
    {
        if(PlayerScripts.Instance.isFinish && !PlayerScripts.Instance.isStart)
        {
            CheckCorrectTown();
            if (isWrong)
            {
                UIManager.Instance.SetEndText(false);
                return;
            }
            DisplayTown();
            if (isArriveHome) return;
            UIManager.Instance.ActiveUI();
            SoundManager.Instance.PlayAudio(SoundType.Improve);
        }
    }

    public void DisplayTown()
    {
        string town2;
        if(townIndex < routes.Count)
        {
            correctTown = routes[townIndex];
            town2 = TownMaster.GetRandomTownExcept(correctTown);
        }
        else if(townIndex == routes.Count)
        {
            correctTown = "Home";
            town2 = "Home";
        }
        else
        {
            isArriveHome = true;
            MapManager.Instance.DisplayHomeMap();
            UIManager.Instance.SetEndText(true);
            return;
        }

        if (UnityEngine.Random.Range(0, 2) < 1)
            UIManager.Instance.SetTown(correctTown, town2);
        else
            UIManager.Instance.SetTown(town2, correctTown);
        townIndex++;
    }

    public void CheckCorrectTown()
    {
        if (!chosenTown.Equals(correctTown))
        {
            isWrong = true;
        }
    }

    public void SetChoseTown(string choseTown)
    {
        this._chosenTown = choseTown;
    }
}


