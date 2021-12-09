using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Wander, Stop, Chase
}
public class Statemachine : MonoBehaviour
{
    #region Variables
    public State state;
    private SpriteRenderer sprite;
    public WaypointAI waypointAI;
    #endregion
    #region States
    #endregion
    private IEnumerator WanderState()
    {
        Debug.Log("Wandering");
        sprite.color = Color.green;
        while (state == State.Wander)
        {
            waypointAI.isMoving = true;
            waypointAI.isChasing = false;
            yield return null;
        }
        Debug.Log("Exiting Wander");
        NextState();
    }
    private IEnumerator StopState()
    {
        Debug.Log("Stopping");
        sprite.color = Color.red;
        while (state == State.Stop)
        {
            waypointAI.isMoving = false;
            waypointAI.isChasing = false;
            yield return null;
        }
        Debug.Log("Exiting Stop");
        NextState();
    }
    private IEnumerator ChaseState()
    {
        Debug.Log("chasing");
        sprite.color = Color.black;
        while (state == State.Chase)
        {
            waypointAI.isMoving = false;
            waypointAI.isChasing = true;
            yield return null;
        }
        Debug.Log("Exiting Chase");
        NextState();

    }
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        if (sprite == null)
        {
            Debug.LogError("Hey idiot, your sprite is null");
        }
        waypointAI = GetComponent<WaypointAI>();
        if(waypointAI == null)
        {
            Debug.LogError("Hey idiot, you don't have a waypointAI on this");
        }
        NextState();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }
    }
    void NextState()
    {
        switch (state)
        {
            case State.Wander:
                StartCoroutine(WanderState());
                break;
            case State.Stop:
                StartCoroutine(StopState());
                break;
            case State.Chase:
                StartCoroutine(ChaseState());
                break;
            default:
                StartCoroutine(StopState());
                break;
        }
    }
}
