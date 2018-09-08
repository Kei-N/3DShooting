using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    // Speed of movement
    public float speedX;
    public float speedZ;

    // Bullets
    public GameObject bullet;
    float bulletInterval;

    // Enemies
    public GameObject enemy;
    float enemyInterval;

    // Explosion
    public GameObject explosion;

    Slider slider;
    int playerLife;

	// Use this for initialization
	void Start () {
        // Interval of bullet
        bulletInterval = 0.0f;
        // Interval of enemy
        enemyInterval = 0.0f;
        // Setting player life
        playerLife = 3;
        // Get Slider component
        slider = GameObject.Find("Slider").GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {

        // Move
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        if(Input.GetKey("up")){
            MoveToUp(vertical);
        }
        if (Input.GetKey("right")){
            MoveToRight(horizontal);
        }
        if (Input.GetKey("left")){
            MoveToLeft(horizontal);
        }

        if (Input.GetKey("down")){
            MoveToBack(vertical);
        }

        // Production of bullets
        bulletInterval += Time.deltaTime;
        if(Input.GetKey("space")){
            if(bulletInterval >= 0.2f){
                GenerateBullet();
            }
        }

        // Production of enemies
        enemyInterval += Time.deltaTime;
        if(enemyInterval >= 5.0f){
            GenerateEnemy();
        }
    }

    // Method for moving
    void MoveToUp(float vertical){
        transform.Translate(0, 0, vertical * speedZ);
    }
    void MoveToRight(float horizontal){
        transform.Translate(horizontal * speedX, 0, 0);
    }
    void MoveToLeft(float horizontal){
        transform.Translate(horizontal * speedX, 0, 0);
    }
    void MoveToBack(float vertical){
        transform.Translate(0, 0, vertical * speedZ);
    }

    // Method for generating bullets
    void GenerateBullet(){
        bulletInterval = 0.0f;
        Instantiate(bullet, transform.position, Quaternion.identity);
    }

    // Method for generating enemies
    void GenerateEnemy(){
        Quaternion q = Quaternion.Euler(0, 180, 0);
        enemyInterval = 0.0f;
        // Generate in random position
        Instantiate(enemy,
                    new Vector3(Random.Range(-100, 100), transform.position.y, transform.position.z + 200), q);
        // Generate before your own eyes
        Instantiate(enemy,
                    new Vector3(transform.position.x, transform.position.y, transform.position.z + 200), q);
    }

    // Explosion
    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "EnemyBullet"){
            // If bullet hit, we reduce player life by 1
            playerLife--;
            // Assign player life to the value of slider
            slider.value = playerLife;
            Instantiate(explosion, new Vector3(
                transform.position.x, transform.position.y, transform.position.z),
                        Quaternion.identity);
            Destroy(other.gameObject);
            // If the player life is 0 or less, fighter aircraft explode
            if(playerLife <= 0){
                Destroy(this.gameObject);
                // Update high score
                ScoreController obj = GameObject.Find("Main Camera").GetComponent<ScoreController>();
                obj.SaveHighScore();
            }
        }
    }
}