using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FlyingCraftController : MonoBehaviour
{

    private float rotationAngleDegree;
    private float rotationSpeedDegree;
    int rotationDirection;

    private Vector3 motionVelocity;
    private float motionSpeedXZ;
    private int motionDirectionXZ;

    private float motionSpeedY;
    private int motionDirectionY;

    // Start is called before the first frame update
    void Start()
    {
        rotationAngleDegree = 0;
        rotationSpeedDegree = 10;
        rotationDirection = 0;

        motionVelocity = Vector3.zero;
        motionSpeedXZ = 10;
        motionDirectionXZ = 0;

        motionSpeedY = 0.5f;
        motionDirectionY = 0;
    }

    // Update is called once per frame
    void Update()
    {

        motionDirectionXZ = 1;
        Move();

        if (Input.GetKey("right"))
        {
            rotationDirection = 2;
            Rotate();
        }

        if (Input.GetKey("left"))
        {
            rotationDirection = -2;
            Rotate();
        }

        if (Input.GetKey("up"))
        {
            motionDirectionY = 1;
            Move();
        }

        if (Input.GetKey("down"))
        {
            motionDirectionY = -1;
            Move();
        }

    }

    private void Rotate()
    {
        float rotationVelocityDegree = rotationSpeedDegree * rotationDirection;
        rotationAngleDegree += rotationVelocityDegree * Time.deltaTime;

        //make sure that rotationAngleDegree within 0-360
        rotationAngleDegree = (rotationAngleDegree + 360) % 360;
        transform.Rotate(Vector3.up, rotationVelocityDegree * Time.deltaTime); //Vector.up is Y-Axis
    }

    private void Move()
    {
        //Add the following
        float motionY = motionDirectionY;

        //convert degree to radian
        double rotationAngleRadian = ((float)rotationAngleDegree / 360.0) * (Math.PI * 2.0);
        float motionX = (float)Math.Sin(rotationAngleRadian) * motionDirectionXZ;
        float motionZ = (float)Math.Cos(rotationAngleRadian) * motionDirectionXZ;

        motionVelocity = new Vector3(motionX, motionY, motionZ);

        //nomoralized vector to represent the directions of motionVelocity
        motionVelocity.Normalize();

        motionVelocity = new Vector3(motionVelocity.x * motionSpeedXZ, motionVelocity.y * motionSpeedY, motionVelocity.z * motionSpeedXZ);
        transform.position += motionVelocity * Time.deltaTime;

        rotationDirection = 0;
        motionDirectionXZ = 0;

        

    }

    void OnTriggerEnter(Collider c)
    {
        //you will update these in a later step
        if (c.gameObject.tag == "Obstacle")
        {
            Debug.Log("Game Over: hit obstacle");
        }
        else if (c.gameObject.tag == "Ground")
        {
            Debug.Log("Game Over: hit ground");
        }
        if (c.gameObject.tag == "Gap")
        {
            Debug.Log("Passed through a gap!");
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Gap")
        {
            Debug.Log("Exited a gap!");
        }
    }

}
