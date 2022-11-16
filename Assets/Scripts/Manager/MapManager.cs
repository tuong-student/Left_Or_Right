using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviorInstance<MapManager>
{
    [SerializeField] List<GameObject> mapList;

    private void Start() 
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerScripts.Instance.isFinish)
        {
            var mapIndex = Random.Range(0, mapList.Count - 1);
            for (int i = 0; i < mapList.Count; i++)
            {
                if(i != mapIndex) mapList[i].SetActive(false);
                else mapList[i].SetActive(true);
            }
            PlayerScripts.Instance.ResetPoint();
        }       
    }
}
