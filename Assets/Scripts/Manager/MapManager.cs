using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviorInstance<MapManager>
{
    [SerializeField] List<GameObject> mapList;
    int mapIndex;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isWin == false) return;
        if(PlayerScripts.Instance.isFinish && !PlayerScripts.Instance.isStart)
        {
            var temp = Random.Range(0, mapList.Count - 1);
            while(temp == mapIndex)
            {
                temp = Random.Range(0, mapList.Count - 1);
            }
            mapIndex = temp;

            for (int i = 0; i < mapList.Count; i++)
            {
                if(i != mapIndex) mapList[i].SetActive(false);
                else mapList[i].SetActive(true);
            }
        }       
    }
}
