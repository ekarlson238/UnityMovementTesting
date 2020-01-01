using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private LayerMask aimLayerMask;

    private Rigidbody rb;

    private Vector3 movementVector = Vector3.zero;
    private Vector3 movementVectorSave = Vector3.zero;

    //private float distanceMultiplier; //if I make a charge up

    public static bool canMove = false;

    private LineRenderer lr;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        lr = this.gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        MovementControls();

        if (canMove)
        {
            rb.AddForce(movementVector * 100);
        }
    }

    private void MovementControls()
    {
        if (Input.GetAxis("Fire1") > 0 && canMove)
        {
            lr.SetPosition(0, this.rb.transform.position);
            lr.SetPosition(1, (MousePosition() + this.rb.transform.position) / 2);
            movementVectorSave = (MousePosition() + this.rb.transform.position) / 2 - this.transform.position;
            lr.enabled = true;
        }
        else
        {
            lr.enabled = false;
            movementVector = movementVectorSave;
        }
    }

    private Vector3 MousePosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100, aimLayerMask))
        {
            Vector3 lookPos = hit.point;
            lookPos.z = transform.position.z;
            return lookPos;
        }
        else
        {
            return Vector3.zero; //shouldn't happen as long as a background exists
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            movementVector = Vector3.zero;
            canMove = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            movementVectorSave = Vector3.zero;
            canMove = false;
        }
    }
}
