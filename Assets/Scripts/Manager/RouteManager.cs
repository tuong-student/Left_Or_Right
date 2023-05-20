using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NOOD;

public class RouteManager : MonoBehaviorInstance<RouteManager>
{
    public TextAsset Towns;
    List<string> routes = new List<string>();

    public List<string> CreateRandomRoute(int townNumber)
    {
        for (int i = 0; i < townNumber; i++)
        {
            string temp = TownMaster.GetRandomTown();
            while (routes.Contains(temp))
            {
                temp = TownMaster.GetRandomTown();
            }
            routes.Add(temp);
        }

        return routes;
    }

    public string GetRandomTownInRoutes()
    {
        return routes[Random.Range(0, routes.Count)];
    }
}

public class TownMaster
{

    public static string[] GetTowns()
    {
        string[] splitNewRow = new string[] { "\r\n", "\r", "\n" };
        char[] slitRow = new char[] { ',' };
        string[] Lines = RouteManager.Instance.Towns.text.Split(splitNewRow, System.StringSplitOptions.RemoveEmptyEntries);
        return Lines;
    }

    public static string GetRandomTown()
    {
        string[] Lines = GetTowns();

        int r = Random.Range(0, Lines.Length - 1);
        return Lines[r];
    }

    public static string GetRandomTownExcept(string exception)
    {
        string[] Lines = GetTowns();

        int r = Random.Range(0, Lines.Length - 1);
        while (Lines[r].Equals(exception))
        {
            r = Random.Range(0, Lines.Length - 1);
        }
        return Lines[r];
    }
}
