using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class Ballcollision : MonoBehaviour
{
    public GameObject ballPrefab; // Prefab of the ball to shoot
    public GameObject wallPrefab;
    public Transform shootPoint; // Point from where the ball is shot
    public float shootForce = 12.0f;
    public float destroyDelay = 5.0f; // Time to destroy ball if not scored
    public Transform goalpost; // Transform of the goalpost
    public float forceincrement = 5.0f;
    public float wallDistance = 5.0f;
    public Transform playerController;
    public float wallheight = 2.78f;
    public GameObject Goalkeeper;

    public LineRenderer aimLineRenderer; // Line renderer to visualize the direction

    private bool aiming; // Flag to indicate if player is aiming
    private GameObject currentBall; // Reference to the current ball
    private GameObject wallObject;

    public Goalkeeper goalkeeper;

    public AudioSource kicksound;

    Vector3 GoalPos;

    private void Start()
    {
        GoalPos = Goalkeeper.transform.position;
        kicksound = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Check for Oculus controller trigger press
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            StartAiming();
        }

        // Check for Oculus controller trigger release
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            ShootBall();
            kicksound.Play();
        }

        // Update aim visualization while aiming
        if (aiming)
        {
            UpdateAimVisualization();
        }

 
    }

    void StartAiming()
    {
        aiming = true;

        // Instantiate a new ball
        currentBall = Instantiate(ballPrefab, shootPoint.position, Quaternion.identity);

        SpawnWall();
    }

    void ShootBall()
    {
        float distanceToGoal = Vector3.Distance(shootPoint.position, goalpost.position);
        Debug.Log("distanceToGoal: " + distanceToGoal);
        // Calculate the shooting force based on the distance to the goalpost
        if (distanceToGoal > 20)
        {
            shootForce = shootForce + forceincrement;
        }
       
        // Get the forward direction of the Oculus controller
        Vector3 shootDirection = transform.forward;

        // Get the Rigidbody component of the current ball
        Rigidbody rb = currentBall.GetComponent<Rigidbody>();

        // Apply force to the ball in the shoot direction
        rb.velocity = shootDirection * shootForce;

        // Disable aim visualization
        aiming = false;
        aimLineRenderer.enabled = false;

        // Destroy the ball after a delay if not scored
        Destroy(currentBall, destroyDelay);

        DestroyWall();

        

        shootForce = 12.0f;

        if (goalkeeper != null)
        {
            FindObjectOfType<Goalkeeper>().GoalMove();
            StartCoroutine(ResetGoalkeeperAfterDelay(4.5f));
        }

    }

    IEnumerator ResetGoalkeeperAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Reset the goalkeeper
        if (goalkeeper != null)
        {
            goalkeeper.Reset();
            goalkeeper.Move = 0;
            // Move the goalkeeper back to the original position
            if (goalkeeper.gameObject != null)
            {
                goalkeeper.gameObject.transform.position = GoalPos;
            }
        }
    }

    void UpdateAimVisualization()
    {
        // Show aim visualization using line renderer
        aimLineRenderer.enabled = true;

        aimLineRenderer.SetPosition(0, shootPoint.position);

        // Define control points for Bezier curve
        Vector3 startPoint = shootPoint.position;
        Vector3 controlPoint1 = startPoint + transform.forward * 0.5f; // Start from the beginning of the line
        Vector3 controlPoint2 = startPoint + transform.up * 0.5f; // Reduce height
        Vector3 endPoint = startPoint + transform.forward * 5f; // Adjust length

        // Set positions of line renderer along the Bezier curve
        aimLineRenderer.positionCount = 50;
        for (int i = 0; i < aimLineRenderer.positionCount; i++)
        {
            float t = i / (float)(aimLineRenderer.positionCount - 1);
            Vector3 point = BezierCurve(startPoint, controlPoint1, controlPoint2, endPoint, t);
            aimLineRenderer.SetPosition(i, point);
        }
    }

    Vector3 BezierCurve(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0;
        p += 3 * uu * t * p1;
        p += 3 * u * tt * p2;
        p += ttt * p3;

        return p;
    }

    void SpawnWall()
    {
        Vector3 forwardDirection = playerController.forward;

        Vector3 wallPosition = playerController.position + forwardDirection * wallDistance;
        wallPosition.y = wallheight;
        

        wallObject = Instantiate(wallPrefab, wallPosition, playerController.rotation);
    }

    void DestroyWall()
    {
        if (wallObject != null)
        {
            Destroy(wallObject, destroyDelay - 3);
            wallObject = null;
        }
    }
}
