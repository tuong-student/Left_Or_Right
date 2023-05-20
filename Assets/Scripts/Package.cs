using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Package 
{
    public static Package CreateRandom()
    {
        Package newPackage = new Package();
        newPackage.packageName = "Random";
        newPackage.weight = Random.Range(10f, 30f);
        return newPackage;
    }

    public string packageName;
    public float weight;
}
