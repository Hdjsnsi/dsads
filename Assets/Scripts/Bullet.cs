using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 currentPos;
    Transform weaponDirection;
    Rigidbody rb;
    Vector3 shoot;
    // Start is called before the first frame update
    void Start()
    {
        currentPos = transform.position;
        rb = GetComponent<Rigidbody>();
        shoot = weaponDirection.transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(shoot.normalized * 10,ForceMode.Impulse);
        float howFar = (currentPos + transform.position).magnitude;
        if(howFar > 500f)
        {
            Destroy(gameObject);
        }
    }
    public void DirectionShootTo(Transform d)
    {
        weaponDirection = d;
    }
}
