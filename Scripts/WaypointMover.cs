using UnityEngine;

public class WaypointMover : MonoBehaviour
{
    public Transform[] waypoints;
    private int currentIndex = 0;

    public void MoveToNextWaypoint()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        currentIndex = (currentIndex + 1) % waypoints.Length;
        transform.position = waypoints[currentIndex].position;
        Debug.Log("Moved to waypoint: " + currentIndex);
    }
}