using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    // Cannon Rotation Variables
    public int speed;
    public float friction;
    public float lerpspeed;
    float xDegrees;
    float yDegrees;
    Quaternion fromRotation;
    Quaternion toRotation;
    Camera camera;
    //Cannon firing variables
    public GameObject cannonBall;
    Rigidbody cannonBallRB;
    public Transform shotPos;
    public GameObject explosion;
    public float firePower;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.tag == "Cannon")
            {
                if (Input.GetMouseButton(0))
                {

                    xDegrees -= Input.GetAxis("Mouse Y") * speed * friction;
                    yDegrees += Input.GetAxis("Mouse X") * speed * friction;

                    fromRotation = transform.rotation;

                    toRotation = Quaternion.Euler(xDegrees, yDegrees, 0);

                    transform.rotation = Quaternion.Lerp(fromRotation, toRotation, Time.deltaTime * lerpspeed);
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            FireCannon();
        }
    }
    public void FireCannon()
    {
        shotPos.rotation = transform.rotation;
        //firePower *= 2000;
        GameObject cannonBallCopy = Instantiate(cannonBall, shotPos.position, shotPos.rotation) as GameObject;
        cannonBallRB = cannonBallCopy.GetComponent<Rigidbody>();
        cannonBallRB.AddForce(transform.forward * firePower);
        Instantiate(explosion, shotPos.position, shotPos.rotation);
    }
}
