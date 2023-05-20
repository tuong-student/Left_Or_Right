using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NOOD;

public class MapManager : MonoBehaviorInstance<MapManager>
{
    [SerializeField] private List<GameObject> mapList = new List<GameObject>();
    [SerializeField] private GameObject homeMap;
    [SerializeField] private string mapName;

    int mapIndex;
    
    private void Start() 
    {
        CreateNPC(NPCType.Sender);
    }

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

            PlayerScripts.Instance.Return();
            if (GameManager.Instance.isArriveHome) return;
            for(int i = 0; i < mapList.Count; i++)
            {
                if (i == mapIndex)
                {
                    mapList[i].SetActive(true); // Active map scene
                    if(ShippingSystem.Instance.TryGetPackageBaseOnDestination(GameManager.Instance.chosenTown, out Package package))
                    {
                        // If map scene is a destination of a package, a NPC will be create
                        CreateNPC(NPCType.Receiver);
                    }
                }
                else mapList[i].SetActive(false);
            }
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

    public void CreateNPC(NPCType type)
    {
        NPC.Create(new Vector3(2, -3, 0), type);
    }
}
