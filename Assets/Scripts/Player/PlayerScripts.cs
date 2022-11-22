using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScripts : MonoBehaviorInstance<PlayerScripts>
{
    [SerializeField] List<Vector3> movePoints = new List<Vector3>();
    Vector3 nextPoint;

    int pointIndex;

    #region Bool
    public bool isFinish;
    public bool isStart;
    bool isLeft;
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
        this.transform.position += NOOD.NoodyCustomCode.LookDirection(this.transform.position, nextPoint) * 2f * Time.deltaTime;

        if (pointIndex == 1) PlayerAnimation.Instance.  RunSide();
        else PlayerAnimation.Instance.RunUp();

        if(Vector3.Distance(this.transform.position, nextPoint) <= 0.5f)
        {
            if(pointIndex == movePoints.Count - 1)
            {
                isFinish = true;
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
