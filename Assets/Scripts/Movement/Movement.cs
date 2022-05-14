using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Camera camera;           // Camera that needs to follow the player

    public float speedFactor;       // Factor that determines how fast the player accelerates 
    public float topSpeed;          // Maximum speed of player

    private Rigidbody rigidbody;    

    private float speed;        

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponentInChildren<Rigidbody>();
    }

    void updateSpeed()
    {
        if(speed < 1)
        {
            speed += speedFactor;
        }
        rigidbody.velocity = transform.forward * topSpeed;
    }

    // Determines movement based on mouse location
    void checkInput()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        float angle = Mathf.Atan2(worldPosition.x - transform.position.x, worldPosition.z - transform.position.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.collider.tag == "wall")
    //    {
    //        rigidbody.AddRelativeForce(transform.position - collision.collider.transform.position, ForceMode.VelocityChange);
    //    }
    //}

    // Locks the camera roll axis 
    void lockCamera()
    {
        camera.transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 0) return;
        updateSpeed();
        checkInput();
        lockCamera();
    }
}
