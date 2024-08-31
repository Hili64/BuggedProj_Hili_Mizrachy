using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSphere : MonoBehaviour
{
    [SerializeField] Color color;
    [SerializeField] float speed;
    Rigidbody rb;
    int dir = 1;
    bool imHealthy;
    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        ChangeVerticalDirAddForce();

        imHealthy = CheckForBugs();
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (imHealthy == true)
        {
            if (collision.gameObject.CompareTag("Sphere"))
            {
                GetComponent<Renderer>().material.color = color;
            }
            if (collision.gameObject.CompareTag("Wall"))
            {
                ChangeVerticalDirAddForce();
            }
            if (collision.gameObject.CompareTag("Cube"))
            {
                ChangeHorizontalDirAddForce();
            }
        }
    }
    public void ChangeVerticalDirAddForce()
    {
        dir *= -1;
        rb.AddForce(Vector3.forward * dir * speed, ForceMode.Impulse);
    }
    public void ChangeHorizontalDirAddForce()
    {
        dir *= -1;
        rb.AddForce(Vector3.right * dir * speed, ForceMode.Impulse);
    }

    public bool CheckForBugs()
    {
        bool testAndApproved = false;
        if (speed == 0)
        {
            Debug.LogError(this.name + " Speed is zero. this cube cant move! set speed to more than 0 from the inspector.");
            testAndApproved = false;
        }
        else if (GetComponent<Collider>() == null || GetComponent<Collider>().enabled == false)
        {
            Debug.LogError(this.name + " this sphere doesn't have an collider!");
            testAndApproved = false;
        }
        else testAndApproved = true;

        return testAndApproved;
    }
}

