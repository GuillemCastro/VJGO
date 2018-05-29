using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Redstone : RedstoneActivable {

    public Material ActiveMaterial;
    public Material NormalMaterial;

    public float delay = 0.5f;

	void Start () {
	}
	
	public override void Activate()
    {
        StartCoroutine(ActivateRoutine());
    }

    IEnumerator ActivateRoutine()
    {
        yield return new WaitForSeconds(delay);
        GetComponent<Renderer>().material = ActiveMaterial;
    }

    public void DeActivate()
    {
        StartCoroutine(DeactivateRoutine());
    }

    IEnumerator DeactivateRoutine()
    {
        yield return new WaitForSeconds(delay);
        GetComponent<Renderer>().material = NormalMaterial;
    }
}
