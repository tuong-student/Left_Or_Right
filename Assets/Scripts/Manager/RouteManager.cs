using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteManager : MonoBehaviorInstance<RouteManager>
{

    public List<string> CreateRoute(int numberOfTown)
    {
        List<string> route = new List<string>();
        for(int i = 0; i < numberOfTown; i++)
        {
            string newTown = TownMaster.RandomTown();
            while (route.Contains(newTown))
            {
                newTown = TownMaster.RandomTown();
            }
            route.Add(newTown);
        }
        return route;
    }
}

public class TownMaster
{

    public static string RandomTown()
    {
        string[] splitNewLine = new string[] { "\r\n", "\r", "\n" };
        char[] splitLine = new char[] { ',' };
        string[] lines;
        var TownName = GameManager.Instance.TownName;
        lines = TownName.text.Split(splitNewLine, System.StringSplitOptions.RemoveEmptyEntries);
        int r = Random.Range(0, lines.Length - 1);
        return lines[r];
    }

    public static string RandomTownExcept(string exception)
    {
        string[] splitNewLine = new string[] { "\r\n", "\r", "\n" };
        char[] splitLine = new char[] { ',' };
        string[] lines;
        var TownName = GameManager.Instance.TownName;
        lines = TownName.text.Split(splitNewLine, System.StringSplitOptions.RemoveEmptyEntries);
        int r = Random.Range(0, lines.Length - 1);
        while (lines[r].Equals(exception))
        {
            r = Random.Range(0, lines.Length - 1);
        }
        return lines[r];
    }
}
