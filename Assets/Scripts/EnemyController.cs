using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    // Speed of enemy movement
    float speed = 10.0f;

    // Interval to shoot bullets
    float interval;

    // Bullets
    public GameObject enemyBullet;

    // Explosion
    public GameObject explosion;

	// Use this for initialization
	void Start () {
        // Setting the initial value of interval
        interval = 0;
	}
	
	// Update is called once per frame
	void Update () {
        // Move enemy
        transform.Translate(-1 * transform.forward * Time.deltaTime * speed);

        // Call a method to shoot bullets
        interval += Time.deltaTime;
        if(interval >= 0.8f){
            GenerateEnemyBullet();
        }
	}

    // Method shooting bullets
    void GenerateEnemyBullet(){
        Quaternion q1 = Quaternion.Euler(0, 185, 0);
        Quaternion q2 = Quaternion.Euler(0, 175, 0);
        interval = 0.0f;
        Instantiate(enemyBullet,new Vector3(
            transform.position.x - 1, transform.position.y, transform.position.z - 2), q1);
        Instantiate(enemyBullet, new Vector3(
            transform.position.x + 1, transform.position.y, transform.position.z - 2), q2);
    }

    // Collision judgment, explosion
    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "PlayerBullet"){
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            Destroy(other.gameObject);
            // Add score
            ScoreController obj = GameObject.Find("Main Camera").GetComponent<ScoreController>();
            obj.ScorePlus();
        }
    }
}
