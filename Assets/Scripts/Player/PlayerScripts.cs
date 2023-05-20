using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using NOOD;

public class PlayerScripts : MonoBehaviorInstance<PlayerScripts>
{
    [SerializeField] List<Vector3> movePoints = new List<Vector3>();
    Vector3 nextPoint;
    private List<Package> packages = new List<Package>();
    private float maxWeightLoad = 100f;
    private float currentWeightLoad = 0f;
    private float money = 0f;

    private int pointIndex;
    private float speed = 5f;

    #region Event
    public Action OnPlayerFinish;
    #endregion

    #region Bool
    public bool isFinish;
    public bool isStart;
    private bool isLeft;
    #endregion

    private void Start()
    {
        isStart = true;
        pointIndex = 0;
        nextPoint = movePoints[pointIndex];
        PlayerAnimation.Instance.Stop();
    }

    private void Update()
    {
        if (GameManager.Instance.isWrong || GameManager.Instance.isArriveHome)
        {
            PlayerAnimation.Instance.Stop();
            return;
        }
        if (isStart == true) return;
        this.transform.position += NOOD.NoodyCustomCode.LookDirection(this.transform.position, nextPoint) * GetSpeed() * Time.deltaTime;

        if (pointIndex == 1) PlayerAnimation.Instance.  RunSide();
        else PlayerAnimation.Instance.RunUp();

        if(Vector3.Distance(this.transform.position, nextPoint) <= 0.5f)
        {
            if(pointIndex == movePoints.Count - 1)
            {
                isFinish = true;
                OnPlayerFinish?.Invoke();
            }
            else
            {
                pointIndex++;
                nextPoint = movePoints[pointIndex];
                if (isLeft)
                {
                    PlayerAnimation.Instance.Opposite(true);
                    nextPoint.x *= -1;
                }
                else
                {
                    PlayerAnimation.Instance.Opposite(false);
                }
            }
        }
    }

    public void AddPackage(Package package)
    {
        packages.Add(package);
    }

    public void AddMoney(float amount)
    {
        this.money += amount;
    }

    public bool TryToGetPackage(Package refPackage, out Package outPackage)
    {
        outPackage = null;
        if(packages.Contains(refPackage))
        {
            outPackage = packages.First(x => x.Equals(refPackage));
            return true;
        }
        else
        {
            return false;
        }
    }

    private float GetSpeed()
    {
        this.currentWeightLoad = 0;
        foreach(Package package in packages)
        {
            this.currentWeightLoad += package.weight;
        }
        if(currentWeightLoad > 0)
            return this.speed * (1 - ((float)(currentWeightLoad / maxWeightLoad)));
        else
            return speed;
    }

    public void Go(bool isLeft)
    {
        this.isLeft = isLeft;
        isStart = false;
        isFinish = false;
    }

    public void Return()
    {
        this.transform.position = new Vector3(0, -4, 0);
        pointIndex = 0;
        nextPoint = movePoints[pointIndex];
        PlayerAnimation.Instance.Stop();
        isStart = true;
        isFinish = false;
    }
}
