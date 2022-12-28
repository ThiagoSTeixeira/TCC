using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOV : MonoBehaviour
{
    public float viewRange = 5f;
    public float viewAngle = 90f;
    public LayerMask viewMask;
    public bool playerInFOV = false;
    void Update()
    {
        //find player object
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        // Cast a ray from the enemy's position in the direction of the player
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (player.transform.position - transform.position).normalized, viewRange, viewMask);

        // If the ray hits the player, set playerInFOV to true
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            playerInFOV = true;
        }
        else
        {
            playerInFOV = false;
        }
    }

    void OnDrawGizmos()
    {
        // Draw the field of view as a yellow triangle in the Scene view
        Vector3 leftBoundary = Quaternion.AngleAxis(-viewAngle / 2, transform.up) * transform.right;
        Vector3 rightBoundary = Quaternion.AngleAxis(viewAngle / 2, transform.up) * transform.right;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary * viewRange);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary * viewRange);
        Gizmos.DrawLine(transform.position + leftBoundary * viewRange, transform.position + rightBoundary * viewRange);
    }

    void OnDrawGizmosCone()
    {
        // Draw the field of view as a yellow cone in the Scene view
        Vector3 leftBoundary = Quaternion.AngleAxis(-viewAngle / 2, transform.up) * transform.right;
        Vector3 rightBoundary = Quaternion.AngleAxis(viewAngle / 2, transform.up) * transform.right;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary * viewRange);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary * viewRange);
        Gizmos.DrawLine(transform.position + leftBoundary * viewRange, transform.position + rightBoundary * viewRange);
        Gizmos.DrawLine(transform.position, transform.position + transform.right * viewRange);
    }
}


/*
 * 
 * This script will cast a ray from the enemy's position in the direction of the player, and check if it hits the player within the specified view range. If it does, the playerInFOV variable will be set to true. The field of view is also drawn as a yellow triangle in the Scene view to visualize it in the game.

You can adjust the viewRange and viewAngle variables to control the size and shape of the FOV. The viewMask variable can be used to specify which layers the FOV should be able to see through. For example, if you want the FOV to not see through walls, you can set the viewMask to a layer that includes only the walls.

I hope this helps! Let me know if you have any questions or if you'd like further clarification on any part of the code.
 * 
 * 
 * 
 * This will draw the FOV as a cone with the enemy at the base, extending in the direction they are facing. You can adjust the viewRange and viewAngle variables to control the size and shape of the cone.

I hope this helps! Let me know if you have any questions or if you'd like further clarification on any part of the code.
 */ 