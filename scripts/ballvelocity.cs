using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class ballvelocity : MonoBehaviour
{
    private Rigidbody rb;
    public Transform wall;
    public Transform goalpost;

    public AudioSource crossbar;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        crossbar = GetComponent<AudioSource>();
    }

    public void Kick(Vector3 velocity)
    {
        // Add the velocity to the ball's rigidbody
        rb.velocity = velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Goal")
        {
            Debug.Log("GOALLL!!!!!");

            // Calculate the distance to the goalpost
            float distanceToGoal = Vector3.Distance(wall.position, goalpost.position);

            // Adjust the score based on the distance (example adjustment)
            int scoreValue = 0; // Default score value

            if (distanceToGoal > 40)
            {
                scoreValue = 20; // Set a higher score value for goals scored from farther away
            }
            else if (distanceToGoal > 25)
            {
                scoreValue = 15; // Set a medium score value for goals scored from medium distance
            }
            else
            {
                scoreValue = 10; // Set a lower score value for goals scored from close distance
            }


            ScoreTracker scoreTracker = GameObject.FindObjectOfType<ScoreTracker>();

            // If ScoreTracker component is found, call its UpdateScore method to increment the score
            if (scoreTracker != null)
            {
                scoreTracker.UpdateScore(scoreValue);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bar"))
        {
            crossbar.Play();    

        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            crossbar.Play();

        }
    }

}
