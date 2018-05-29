using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PistonWithRedstone : Piston {

    public UnityEvent activatedEvent;

    Redstone redstone;

    bool m_circuitActivated;

    /* 
     * m_isDown indicates if the piston is down or up
     * m_circuitActivated indicated if the circuit is powered or not
     */

    private void Awake()
    {
        redstone = GetComponentInChildren<Redstone>();
        audioSource = GetComponent<AudioSource>();
        Coordinate = new Vector2(transform.position.x, transform.position.z);
    }

    //Called from an Event, puts the piston down and checks if the circuit has been powered
    public override void Activate()
    {
        if (m_isDown)
        {
            return;
        }
        base.Activate();
        if (m_circuitActivated)
        {
            RealActivate();
        }
    }

    //Called from an Event, marks the circuit as powered and checks if the piston is down
    public void ActivateCircuit()
    {
        if (m_circuitActivated)
        {
            return;
        }
        m_circuitActivated = true;
        if (m_isDown)
        {
            RealActivate();
        }
    }

    //Real activation of the redstone and the circuit
    void RealActivate()
    {
        StartCoroutine(RealActivateRoutine());
    }

    IEnumerator RealActivateRoutine()
    {
        yield return new WaitForSeconds(delay);
        if (redstone != null)
        {
            redstone.Activate();
        }
        activatedEvent.Invoke();
    }

}
