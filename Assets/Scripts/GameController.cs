using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public GameObject obstacle;

    private ArrayList obstacles;

    public Text scoreText;
    public Text gameOverText;

    // Use this for initialization
    void Start()
    {
        obstacles = new ArrayList();
        CreateObstacles();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Obstacles remaining: " + obstacles.Count;

        //to be completed
    }

    public void CreateObstacles()
    {
        //you should create at least 3 obstacles with different rotations 
        //and in different positions
        //note: we are adding them to the ArrayList created above

        //Instantiate can take the transform (position vector) 
        //and rotation quaternion as parameters

        obstacles.Add(
            Instantiate(obstacle, new Vector3(4, 4, 4), Quaternion.identity)
        );

        //Quaternion.AngleAxis creates a Quaternion object that is defined by 
        //the number of degrees of rotation around a provided axis. 
        //Below we provide the up axis (or y-axis)
        obstacles.Add(
            Instantiate(obstacle, new Vector3(-10, 8, 6), Quaternion.AngleAxis(45f, Vector3.up))
        );
    }

    //This method is to be called by the FlyingCraft when it successfully
    //makes it through a gap. The first parameter is the obstacle,
    //the second parameter is the FlyingCraft game object.
    public void Score(GameObject obstacle, GameObject sender)
    {
        //check if the obstacle is in the list
        //i.e., has not yet been passed through
        if (obstacles.Contains(obstacle))
        {
            obstacles.Remove(obstacle);
        }

        if (obstacles.Count == 0)
        {
            Win();
            sender.SendMessage("Stop");
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Obstacle")
        {
            gameOn = false;
            gameController.GameOver();
        }
        else if (c.gameObject.tag == "Ground")
        {
            gameOn = false;
            gameController.GameOver();
        }
        if (c.gameObject.tag == "Gap")
        {
            inGap = true;
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (inGap)
        {
            inGap = false;

            //note the line below is necessary to access the whole Prefab
            //obstacle object. The collider that is return only refer to the 
            //the gap cube
            GameObject parentObject = c.gameObject.transform.parent.gameObject;

            //call the score method with the correct obstacle and
            //a reference to this FlyingCraft object
            gameController.Score(parentObject, gameObject);
        }
    }
}