using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public Transform firstPersonCam;

	// Use this for initialization
	public override void OnStartLocalPlayer () {
        GetComponent<MeshRenderer>().material.color = Color.blue;
        GameObject.FindObjectOfType<NetworkManagerHUD>().showGUI = false;
        Camera.main.transform.position = firstPersonCam.position;
        Camera.main.transform.rotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
            return;

        var camTurn = Input.GetAxis("Mouse X") * Time.deltaTime * 180.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, camTurn, 0);
        transform.Translate(0, 0, z);

        Camera.main.transform.position = firstPersonCam.position;
        Camera.main.transform.rotation = transform.rotation;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdFire();
        }
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if (GameObject.FindObjectOfType<NetworkManagerHUD>().showGUI == false)
                GameObject.FindObjectOfType<NetworkManagerHUD>().showGUI = true;
            else
                GameObject.FindObjectOfType<NetworkManagerHUD>().showGUI = false;
        }
    }

    [Command]
    void CmdFire()
    {
        var bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

        NetworkServer.Spawn(bullet);

        Destroy(bullet, 2.0f);
    }
}