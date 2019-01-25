using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ResetNavMesh();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ResetNavMesh()
    {
        Debug.Log("reset nav");
    }
}
