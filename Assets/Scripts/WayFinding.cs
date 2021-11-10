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
    // Start is called before the first frame update
    void Start()
    {
        transform.position = wayPoints[wayPointIndex].transform.position;
        isNeutral = true;
        isAttacking = false;
        isDefending = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAttacking && !isDefending)
        {
            isNeutral = true;
        }
        if (isNeutral)
        {
            MoveToWayPoint();
            statusText.text = "Neutral".ToString();
        }
        if (isAttacking)
        {
            isNeutral = false;
            statusText.text = "Attacking!".ToString();
        }
        if (isDefending)
        {
            isNeutral = false;
            statusText.text = "Defending!".ToString();
        }
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
    }
    public void DefenderState()
    {
        isDefending = true;
        anim.SetBool("isDefending", true);
        transform.position = Vector2.MoveTowards(transform.position, defenderTarget.transform.position, moveSpeed * Time.deltaTime);
    }
    public void AttackerState()
    {
        isAttacking = true;
        anim.SetBool("isAttacking", true);
        transform.position = Vector2.MoveTowards(transform.position, playerTarget.transform.position, (moveSpeed * 1.5f) * Time.deltaTime);
    }

}
