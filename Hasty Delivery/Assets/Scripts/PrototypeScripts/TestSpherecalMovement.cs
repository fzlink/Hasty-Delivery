using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpherecalMovement : MonoBehaviour
{
    private Collider collider;

    private void Awake()
    {
        collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        /*RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, 100))
        {
            transform.up = hit.normal;
        }*/
        transform.position += new Vector3(0, 0, 0.1f);
        MakeGrounded();
    }

    private bool MakeGrounded()
    {
        Vector3 scanOrigin = transform.position;
        RaycastHit hit;

        if (Physics.Raycast(scanOrigin, Vector3.down, out hit, 100))
        {
            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            Debug.DrawRay(scanOrigin, Vector3.down * 100, Color.red);
            if (hit.distance <= GetComponent<Collider>().bounds.extents.y + 0.1f && hit.distance >= GetComponent<Collider>().bounds.extents.y - 0.1f)
            {
                return true;
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, hit.point + Vector3.up / 2, Time.deltaTime * 10);
                return false;
            }
        }
        else
        {
            transform.position += Vector3.up * Time.deltaTime * 20;
            //Debug.DrawRay(scanOrigin, Vector3.down, Color.red);
            return false;
        }
    }
}
