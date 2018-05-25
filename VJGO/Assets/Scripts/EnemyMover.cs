using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : Mover {
    public float standTime = 1f;
    protected override void Awake()
    {
        base.Awake();
        faceDestination = true;
    }

    // Use this for initialization
    protected override void Start () {
        base.Start();
        //StartCoroutine(TestMovementRoutine());
	}
	
    public void MoveOneTurn()
    {
        Stand();
    }

    void Stand()
    {
        StartCoroutine(StandRoutine());
    }

    IEnumerator StandRoutine()
    {
        yield return new WaitForSeconds(standTime);
        base.finishMovementEvent.Invoke();
    }
}
