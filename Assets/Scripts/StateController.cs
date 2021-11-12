using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    public GameObject enemyAi;

    // Start is called before the first frame update
    void Start()
    {
        enemyAi = GameObject.FindWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetDefenseState()
    {
        enemyAi.GetComponent<WayFinding>().DefenderState();
    }
    public void SetAttackState()
    {
        enemyAi.GetComponent<WayFinding>().AttackerState();
    }
}
