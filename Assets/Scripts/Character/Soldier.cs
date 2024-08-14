using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    private Rigidbody rb;
    [Header("Movement")]
    public float speed;
    public float rbDrag;
    public float currentSpeed;
    private GameObject rightArm,weapons;

    [Header("On Ground")]
    public float playHeight;
    public LayerMask whatIsGround;
    [Header("Looking")]
    public float senY;
    public float senX;

    public GameObject ammo;
    float rotationX;
    float rotationY;

    bool grounded;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rightArm = transform.Find("Right Arm").gameObject;
        weapons = rightArm.transform.Find("Weapon").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed = rb.velocity.magnitude;
        
        // PlayerControlSpeed();
        // PlayerMove(Input.GetAxisRaw("Vertical"),Input.GetAxisRaw("Horizontal"));
        // OnGround();
        // Looking();
        // if(Input.GetKeyDown(KeyCode.Space))
        // {
        //     Shoot();
        // }
    }
    void PlayerMove(float playerVertical, float playerHorizontal)
    {
        Vector3 direction = Vector3.forward * playerVertical + Vector3.right * playerHorizontal;
        rb.AddForce(direction.normalized * speed * 10, ForceMode.Force);
    }
    void PlayerControlSpeed()
    {
        Vector3 maxSpeed = new Vector3(rb.velocity.x, 0 ,rb.velocity.z);
        if(maxSpeed.magnitude > speed)
        {
            Vector3 limitSpeed = maxSpeed.normalized * speed;
            rb.velocity = new Vector3(limitSpeed.x,rb.velocity.y,limitSpeed.z);
        }
    }
    void OnGround()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playHeight * .5f + .2f, whatIsGround);
        if(grounded)
        {
            rb.drag = rbDrag;
        }else
        {
            rb.drag = 0;
        }
    }
    void Looking()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * senX * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * senY * Time.deltaTime;
        
        rotationX -= mouseY;
        rotationY += mouseX;
        rotationX = Mathf.Clamp(rotationX, -90,90);

        transform.rotation = Quaternion.Euler(0, rotationY, 0f);
        rightArm.transform.rotation = Quaternion.Euler(rotationX, 0,0);
    }
    void Shoot()
    {
        GameObject bullet =Instantiate(ammo ,weapons.transform.position,ammo.transform.rotation);
        Bullet thisBullet = bullet.GetComponent<Bullet>();
        thisBullet.DirectionShootTo(weapons.transform);
    }
}
