using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScripts : MonoBehaviorInstance<PlayerScripts>
{
    public List<Vector3> points = new List<Vector3>();
    public Vector3 nextPoint = new Vector3();
    [SerializeField] float speed;
    Vector3 dir;
    public bool isFinish = false;
    int i = 0;

    public bool isLeft;

    // Start is called before the first frame update
    void Start()
    {
        nextPoint = points[i];
        dir = NOOD.NoodyCustomCode.LookDirection(this.transform.position, nextPoint);
    }

    // Update is called once per frame
    void Update()
    {
        MoveToTheNextPoint();
    }

    public void MoveToTheNextPoint()
    {
        if(Vector3.Distance(this.transform.position, nextPoint) <= 0.5f)
        {
            i+=1;
            try
            {
                nextPoint = points[i];
                if(isLeft)
                {
                    nextPoint.x *= -1;
                }
                dir = NOOD.NoodyCustomCode.LookDirection(this.transform.position, nextPoint);

            }catch
            {
                dir = Vector3.zero;
                if(i == points.Count) isFinish = true;
            }
        }
        this.transform.position += dir * (speed * Time.deltaTime);
    }

    public void ResetPoint()
    {
        this.gameObject.transform.position = new Vector3(0, -4, 0);
        i = 0;
        nextPoint = points[i];
        dir = NOOD.NoodyCustomCode.LookDirection(this.transform.position, nextPoint);
        isFinish = false;
    }
}
