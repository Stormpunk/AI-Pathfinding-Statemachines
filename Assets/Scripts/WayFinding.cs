using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WayFinding : MonoBehaviour
{
    public GameObject[] wayPoints;
    //an array of waypoints the chaser moves between in its neutral state
    float moveSpeed = 10f;
    //speed of movement
    public GameObject defenderTarget;
    //the mcguffin the chaser must protect
    public int wayPointIndex = 0;
    //the int that determines what active waypoint the chaser will follow.
    public bool isNeutral;
    //the return neutral state
    public bool isDefending;
    //defense state triggered when the attacker makes a hostile move.
    public bool isAttacking;
    //aggressive state when the attacker enters a certain radius
    public Animator anim;
    //animator
    public Text statusText;
    //text that shows what the current state is.
    public GameObject playerTarget;
    public float maxAlarmValue;
    public float currentAlarmValue;
    //these two values will set the timer for the alarm states before the AI returns to its neutral "patrol" state
    public Text alarmText;
    //variable that will display the alarm time

    // Start is called before the first frame update
    void Start()
    {
        transform.position = wayPoints[wayPointIndex].transform.position;
        isNeutral = true;
        isAttacking = false;
        isDefending = false;
        maxAlarmValue = 60;
        currentAlarmValue = maxAlarmValue;
        //sets up the values that will be set later (default state, the maximum and current alarm timers.)
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAttacking && !isDefending)
        {
            isNeutral = true;
        }
        //sets the default state to Neutral/"Patrol"
        if (isNeutral)
        {
            MoveToWayPoint();
            statusText.text = "Neutral".ToString();
            anim.SetBool("isNeutral", true);
        }
        //ensures the AI will move between waypoints and display its current state
        if (isAttacking)
        {
            isNeutral = false;
            statusText.text = "Attacking!".ToString();
            transform.position = Vector2.MoveTowards(transform.position, playerTarget.transform.position, (moveSpeed * 1.5f) * Time.deltaTime);
            currentAlarmValue -= Time.deltaTime;
            //moves towards the attack target and displays its current state
        }
        if (isDefending)
        {
            isNeutral = false;
            statusText.text = "Defending!".ToString();
            transform.position = Vector2.MoveTowards(transform.position, defenderTarget.transform.position, moveSpeed * Time.deltaTime);
            currentAlarmValue -= Time.deltaTime;
            // moves to the defense node and displays its current state
        }
        if(currentAlarmValue <= 0 && (isDefending || isAttacking))
        {
            isAttacking = false;
            isDefending = false;
            currentAlarmValue = maxAlarmValue;
            //resets the alarm and returns to the patrol state
        }
        alarmText.text = "Alarm Time: " + currentAlarmValue.ToString();
        //displays the alarm time
    }
    public void MoveToWayPoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[wayPointIndex].transform.position, moveSpeed * Time.deltaTime);
        if (transform.position == wayPoints[wayPointIndex].transform.position)
        {
            wayPointIndex += 1;
        }
        if (wayPointIndex == wayPoints.Length)
        {
            wayPointIndex = 0;
        }
        //moves to the next waypoint, when it reaches that waypoint, sets the waypoint to go to as the next one up
    }
    public void DefenderState()
    {
        isDefending = true;
        isAttacking = false;
        anim.SetBool("isDefending", true);
    }
    public void AttackerState()
    {
        isAttacking = true;
        isDefending = false;
        anim.SetBool("isChasing", true);
    }

}
