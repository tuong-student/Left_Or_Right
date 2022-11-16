using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScripts : MonoBehaviour
{
    public List<Vector3> points;
    public Vector3 nextPoint;
    [SerializeField] float speed;

    public bool isLeft;

    // Start is called before the first frame update
    void Start()
    {
        nextPoint = points[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = NOOD.NoodyCustomCode.LookDirection(this.transform.position, nextPoint);
        this.transform.position += dir * speed * Time.deltaTime;
        if(this.transform.position.Equals(nextPoint))
        {
            nextPoint = points[+1];
            if(isLeft && nextPoint.Equals(points[1]))
            {
                nextPoint.x *= -1;
            }
        }
    }
}
