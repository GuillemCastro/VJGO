using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Redstone : MonoBehaviour {

    public Material ActiveMaterial;
    public Material NormalMaterial;

	void Start () {
	}
	
	public void Activate()
    {
        GetComponent<Renderer>().material = ActiveMaterial;
    }

    public void DeActivate()
    {
        GetComponent<Renderer>().material = NormalMaterial;
    }
}
