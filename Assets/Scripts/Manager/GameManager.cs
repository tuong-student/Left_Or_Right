using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class GameManager : MonoBehaviorInstance<GameManager>
{
    string correctTown;
    [HideInInspector] public List<string> routes = new List<string>();
    public string choosenTown;

    int townIndex = 0;

    public bool isWrong = false;
    public bool isArriveHome = false;

    private void Start()
    {
        routes = RouteManager.Instance.CreateRandomRoute(5);
        DisplayTown();
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
            UIManager.Instance.ActiveUI();
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
            return;
        }

        if (Random.Range(0, 2) < 1)
            UIManager.Instance.SetTown(correctTown, town2);
        else
            UIManager.Instance.SetTown(town2, correctTown);
        townIndex++;
    }

    public void CheckCorrectTown()
    {
        if (!choosenTown.Equals(correctTown))
        {
            isWrong = true;
        }
    }
}


