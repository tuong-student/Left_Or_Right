using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCType
{
    Sender,
    Receiver
}

public class NPC : MonoBehaviour
{
    [SerializeField] private NPCType type;
    [SerializeField] private Package package, requirePackage;
    [SerializeField] private string destination;
    [SerializeField] private float money;
    private Action<Package, float> SetInfoNPC;

    public static void Create(Vector3 position, NPCType type)
    {
        NPC newNPC = Instantiate(Resources.Load<NPC>("NPC/NPC1"), position, Quaternion.identity);
        newNPC.SetType(type);
        switch(type)
        {
            case NPCType.Sender:
                string destination = RouteManager.Instance.GetRandomTownInRoutes();
                newNPC.destination = destination;
                newNPC.SetInfoNPC(Package.CreateRandom(), UnityEngine.Random.Range(10, 70));
                break;
            case NPCType.Receiver:
                ShippingSystem.Instance.TryGetShippingInfo(GameManager.Instance.chosenTown, out ShippingInfo shippingInfo);
                newNPC.SetInfoNPC(shippingInfo.package, shippingInfo.money);
                break;
        }
    }

    private void Start()
    {
        PlayerScripts.Instance.OnPlayerFinish += DestroySelf;
    }

    private void Update()
    {
        if(GameManager.Instance.isGameStart == false) return;
        switch(type)
        {
            case NPCType.Sender:
                if(package != null && PlayerScripts.Instance)
                {
                    GivePackageToPlayer();
                    package = null;
                }
                break;
            case NPCType.Receiver:
                if(requirePackage != null && PlayerScripts.Instance)
                {
                    GetPackageFromPlayer(requirePackage);
                    GiveMoneyToPlayer();
                }
                break;
        }

    }

    private void OnDestroy()
    {
        PlayerScripts.Instance.OnPlayerFinish -= DestroySelf;
    }

    public void SetType(NPCType type)
    {
        this.type = type;

        if(type == NPCType.Receiver)
            SetInfoNPC = SetInfoReceiver;
        if(type == NPCType.Sender)
            SetInfoNPC = SetInfoSender;
    }

    private void SetInfoSender(Package package, float money)
    {
        this.package = package;
        this.money = money;
    }

    private void SetInfoReceiver(Package package, float money)
    {
        this.requirePackage = package;
        this.money = money;
    }

    private void GivePackageToPlayer()
    {
        Debug.Log("GivePackageToPlayer" + package.packageName + " " + package.weight + " " + destination);
        PlayerScripts.Instance.AddPackage(package);
        ShippingSystem.Instance.AddNewPackageInfo(package, destination, money);
    }

    private void GetPackageFromPlayer(Package package)
    {
        bool isGetSuccess = PlayerScripts.Instance.TryToGetPackage(package, out Package receivePackage);
        if(isGetSuccess)
        {
            Debug.Log("Get successfully");
            requirePackage = null;
        }
        else
        {
            Debug.Log("Can not get");
        }
    }

    private void GiveMoneyToPlayer()
    {
        Debug.Log("GiveMoney to player");
        PlayerScripts.Instance.AddMoney(money);
    }

    private void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
