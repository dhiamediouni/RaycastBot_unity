using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RaycastBot : MonoBehaviour
{
    // Start is called before the first frame update
    RaycastHit hit;
    private Rigidbody rb;
    public float force = 20f;
    public float rotateSpeed = 60f;
    public bool back;
    
    public NavMeshAgent agent;
    public Transform player;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 3.5f);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        

        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= 3.5f)
        {
            // Move towards the target
            agent.SetDestination(player.position);
        }
        else
        {
            if (back == false)
            {
                rb.velocity = (transform.forward + Vector3.forward * force * Time.deltaTime);
            }
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 3f, Color.white);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward + Vector3.right) * 3f, Color.white);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward + Vector3.left) * 3f, Color.white);

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward + Vector3.right), out hit, 3f) && (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward + Vector3.left), out hit, 3f)))
            {
                transform.rotation = Quaternion.LookRotation(-Vector3.forward);
                back = true;
            }

            else
            {
                back = false;
            }
            



            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward + Vector3.right), out hit, 3f))
            {
                transform.Rotate(Vector3.up * -rotateSpeed * Time.deltaTime);
            }



            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward + Vector3.left), out hit, 3f))
            {
                transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
            }

        }
    }
}
