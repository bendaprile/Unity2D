﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{

    WaveConfig waveConfig;
    List<Transform> waypoints;
    int nextWaypointIndex = 0;

    private void Start()
    {
        waypoints = waveConfig.GetWaypoints();

        // Set the ships starting position to the first waypoint in our waypoints list
        transform.position = waypoints[nextWaypointIndex].position;
    }

    private void Update()
    {
        Move();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    // Calculates the ships next spot and moves the ship to that spot
    private void Move()
    {
        // If we're not at the last waypoint already,
        // Note: must use waypoints.Count not waypoints.Length for lists
        if (nextWaypointIndex <= waypoints.Count - 1)
        {

            var targetPosition = waypoints[nextWaypointIndex].position;
            var moveSpeed = waveConfig.GetMoveSpeed();

            // Multiply movementSpeed by Time.deltaTime in order to make enemy movement frame rate independent
            var movementThisFrame = moveSpeed * Time.deltaTime;

            // Use Vector2.MoveTowards method to get our new position to move to
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            // If we've arrived at our nextWaypoint then increment our waypoint index
            if (transform.position == targetPosition)
            {
                nextWaypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
