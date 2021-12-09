using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointAI : MonoBehaviour
{
    public bool isMoving;
    public bool isChasing;
    public Transform player;
    public Transform[] waypoint = new Transform[3];
    public Transform enemy;
    public int waypointIndex;
    public float speed; 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving == true)
        {
            player.position = Vector2.MoveTowards(transform.position, waypoint[waypointIndex].transform.position, speed * Time.deltaTime);
            if(player.position == waypoint[waypointIndex].transform.position)
            {
                waypointIndex++;
            }
            if(waypointIndex == waypoint.Length)
            {
                waypointIndex = 0;
            }
        }
        if(isChasing == true)
        {
            player.position = Vector2.MoveTowards(transform.position, enemy.transform.position, speed * Time.deltaTime);
        }
    }
}
