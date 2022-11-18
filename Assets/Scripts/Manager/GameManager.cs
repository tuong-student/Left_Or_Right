using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class GameManager : MonoBehaviorInstance<GameManager>
{
    public bool isLeft;
    public TextAsset TownName;
    List<string> routes = new List<string>();
    int i = 0;
    bool isChooseCorrect = true;
    public bool isWin = true;

    string correctTown;

    private void Start()
    {
        routes = RouteManager.Instance.CreateRoute(4);
        UIManager.Instance.SetRouteText(routes);
        SetTown();
    }

    public void SetLeft(bool value)
    {
        isLeft = value;
        PlayerScripts.Instance.isLeft = value;
    }

    private void Update()
    {
        if (PlayerScripts.Instance.isFinish)
        {
            if (isChooseCorrect == false)
            {
                isWin = false;
                UIManager.Instance.DisplayEndText(false);
                return;
            }
            if (i == routes.Count + 1)
            {
                isWin = true;
                UIManager.Instance.DisplayEndText(true);
            }
        }

        if(PlayerScripts.Instance.isFinish && !PlayerScripts.Instance.isStart)
        {
            SetTown();
            UIManager.Instance.SetUIActiveTrue();
            PlayerScripts.Instance.ResetPoint();
        }
    }

    private void SetTown()
    {
        string town1;
        string town2;
        if (i == routes.Count)
        {
            town1 = "HOME";
            town2 = "HOME";
        }
        else
        {
            Debug.Log(i);
            town1 = routes[i];
            town2 = TownMaster.RandomTownExcept(town1);
        }

        correctTown = town1;
        UIManager.Instance.DisplayTown(town1, town2);
        i++;
    }

    public void CheckChooseCorrect()
    {
        string chooseText;
        if (isLeft) chooseText = UIManager.Instance.town1;
        else chooseText = UIManager.Instance.town2;

        if (chooseText.Equals(correctTown)) isChooseCorrect = true;
        else isChooseCorrect = false;
    }
}


