using System.Collections;
using UnityEngine;

public class WaypointMover : MonoBehaviour
{
    public Transform[] waypoints; // Waypoints for movement
    public float moveSpeed = 5f;
    public float waitTime = 0.5f;

    public GameObject player1; // Reference to Player 1 GameObject
    public GameObject player2; // Reference to Player 2 GameObject

    private int player1WaypointIndex = 0; // Track Player 1's waypoint index
    private int player2WaypointIndex = 0; // Track Player 2's waypoint index
    private bool isMoving = false; // Flag for movement status
    private DiceRoller diceRoller; // Reference to DiceRoller
    private bool isPlayer1Turn = true; // Track turns

    void Start()
    {
        diceRoller = FindObjectOfType<DiceRoller>(); // Find DiceRoller in scene
        if (diceRoller != null)
        {
            diceRoller.OnDiceRolled += StartMoving;
        }
        else
        {
            Debug.LogError("DiceRoller not found in the scene!");
        }
    }

    private void StartMoving(int diceSum)
    {
        if (!isMoving)
        {
            // Start moving based on whose turn it is
            if (isPlayer1Turn)
            {
                StartCoroutine(MoveToWaypoints(player1.transform, player1WaypointIndex, diceSum));
            }
            else
            {
                StartCoroutine(MoveToWaypoints(player2.transform, player2WaypointIndex, diceSum));
            }

            isPlayer1Turn = !isPlayer1Turn; // Switch turns after starting movement
        }
    }

    IEnumerator MoveToWaypoints(Transform playerTransform, int waypointIndex, int steps)
    {
        isMoving = true;

        // Disable the player's BoxCollider2D while moving
        BoxCollider2D playerCollider = playerTransform.GetComponent<BoxCollider2D>();
        if (playerCollider != null)
        {
            playerCollider.enabled = false;
        }

        // Move player step by step to the target waypoint based on the dice roll
        for (int i = 0; i < steps; i++)
        {
            waypointIndex++; // Increment to move to the next waypoint

            if (waypointIndex >= waypoints.Length) break; // Check if beyond last waypoint

            Transform targetWaypoint = waypoints[waypointIndex];

            // Move towards the target waypoint
            while (Vector3.Distance(playerTransform.position, targetWaypoint.position) > 0.01f)
            {
                playerTransform.position = Vector3.Lerp(playerTransform.position, targetWaypoint.position, moveSpeed * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(waitTime); // Wait at the waypoint
        }

        // Update the player's waypoint index after moving
        if (playerTransform == player1.transform)
        {
            player1WaypointIndex = waypointIndex; // Update Player 1's index
        }
        else
        {
            player2WaypointIndex = waypointIndex; // Update Player 2's index
        }

        // Re-enable the player's BoxCollider2D after reaching the final waypoint
        if (playerCollider != null)
        {
            playerCollider.enabled = true;
        }

        isMoving = false; // Allow next move
    }
}
