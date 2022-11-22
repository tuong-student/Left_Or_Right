using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviorInstance<MapManager>
{
    [SerializeField] List<GameObject> mapList = new List<GameObject>();
    [SerializeField] GameObject homeMap;

    int mapIndex;

    private void Update()
    {
        if(PlayerScripts.Instance.isFinish && !PlayerScripts.Instance.isStart)
        {
            if (GameManager.Instance.isWrong) return;
            int temp = Random.Range(0, mapList.Count - 1);
            while(temp == mapIndex)
            {
                temp = Random.Range(0, mapList.Count - 1);
            }
            mapIndex = temp;

            for(int i = 0; i < mapList.Count; i++)
            {
                if (i == mapIndex) mapList[i].SetActive(true);
                else mapList[i].SetActive(false);
            }
            PlayerScripts.Instance.Return();
        }
    }

    public void DisplayHomeMap()
    {
        foreach(var map in mapList)
        {
            map.SetActive(false);
        }
        homeMap.SetActive(true);
    }
}
