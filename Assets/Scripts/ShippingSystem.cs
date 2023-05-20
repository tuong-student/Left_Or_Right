using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using NOOD;

public class ShippingInfo
{
    public Package package;
    public float money;

    public ShippingInfo(Package package, float money)
    {
        this.package = package;
        this.money = money;
    }
}

public class ShippingSystem : MonoBehaviorInstance<ShippingSystem> 
{
    Dictionary<string, ShippingInfo> packageAndDestinations = new Dictionary<string, ShippingInfo>();

    public void AddNewPackageInfo(Package package, string destination, float money)
    {
        packageAndDestinations.Add(destination, new ShippingInfo(package, money));
    }

    public bool TryGetShippingInfo(string destination, out ShippingInfo shippingInfo)
    {
        if(packageAndDestinations.TryGetValue(destination, out shippingInfo))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool TryGetPackageBaseOnDestination(string destination, out Package package)
    {
        if(packageAndDestinations.TryGetValue(destination, out ShippingInfo shippingInfo))
        {
            package = shippingInfo.package;
            return true;
        }
        else
        {
            package = null;
            return false;
        }
    }

    public bool TryGetMoneyBaseOnDestination(string destination, out float money)
    {
        if(packageAndDestinations.TryGetValue(destination, out ShippingInfo shippingInfo))
        {
            money = shippingInfo.money;
            return true;
        }
        else
        {
            money = 0;
            return false;
        }
    }
}
