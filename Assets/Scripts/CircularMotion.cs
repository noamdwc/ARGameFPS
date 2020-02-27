using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMotion : MonoBehaviour
{
    public GameObject aroundPoint;

    public float speed;

    public bool isRight;
    public bool isLeft;
    public bool isUp;
    public bool isDown;
    public bool isForward;
    public bool isBack;

    private void Start()
    {
        aroundPoint = FindObjectOfType<OffensiveWeapon>().gameObject;
    }
    private void rotate(Vector3 dir)
    {
        transform.RotateAround(aroundPoint.transform.position, dir, speed * Time.fixedDeltaTime);
    }

    private void FixedUpdate()
    {
        if (isRight)
            rotate(Vector3.right);
        if (isLeft)
            rotate(Vector3.left);
        if (isUp)
            rotate(Vector3.up);
        if (isDown)
            rotate(Vector3.down);
        if (isForward)
            rotate(Vector3.forward);
        if (isBack)
            rotate(Vector3.back);
    }
}
