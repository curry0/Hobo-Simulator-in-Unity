using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour
{
    [SerializeField] float speed;

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        float horiz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector3(horiz * speed, 0, vert * speed);
   
    }

   /* private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "rebaseTableCover")
            rb.velocity = new Vector3(0, 0, 0);
        
    }*/
    

    
}
