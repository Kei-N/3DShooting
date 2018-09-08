using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed;

	// Use this for initialization
	void Start () {
        // Delete bullet in 5 seconds
        Destroy(this.gameObject, 5);
	}
	
	// Update is called once per frame
	void Update () {
        // Move bullet
        transform.Translate(transform.forward * Time.deltaTime * speed);
	}
}
