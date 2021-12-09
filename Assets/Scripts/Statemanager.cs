using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statemanager : MonoBehaviour
{
    [SerializeField]
    private Statemachine aiStatemachine;
    public void Wander()
    {
        aiStatemachine.state = State.Wander;
    }
    public void Stop()
    {
        aiStatemachine.state = State.Stop;
    }
    public void Attack()
    {
        aiStatemachine.state = State.Chase;
    }
}
