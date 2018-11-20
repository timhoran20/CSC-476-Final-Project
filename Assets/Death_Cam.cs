using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_Cam : MonoBehaviour {

    public GameObject player;
    public Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = new Vector3(0, 3, -6);
        transform.position = player.transform.position + offset;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = player.transform.position + offset;
    }
}