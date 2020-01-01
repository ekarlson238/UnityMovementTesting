using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField]
    private float gravity = 9.8f;
    private Vector3 gravityVector;

    public static bool gravityOn = true;

    private Rigidbody rb;

    private void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        gravityVector = new Vector3(0, (gravity), 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (gravityOn)
        {
            rb.velocity -= gravityVector * Time.deltaTime;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            gravityOn = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            gravityOn = true;
        }
    }
}
