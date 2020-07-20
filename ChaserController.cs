using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserController : MonoBehaviour
{

    /*
    Bug Log: if random direction is towards eachother then they will cancel each others velocity
    potential fix to add a force if colliding with another enemy

    data: enemy position vector3, attack type,
     */
    PlayerController player;
    Pickups pickups;
    //Spawner spawner;
    public GameObject explosion; 
    //private Rigidbody2D rb; 
    
    public static int enemyCount;
    int attackNum; 
    float range = 5f; 
    Vector3 target;
    bool tracking;
    public Collider2D[] colliders;

    public Vector3[] collection; 

    void Awake() {
        player = FindObjectOfType<PlayerController>();
        attackNum = Random.Range(0, 4); //max is -1
        target = transform.position;
        enemyCount++;
        //Debug.Log("Enemy Count: " + enemyCount);
        tracking = false;
    }

    void Update() {
        if (pickups == null) {
            pickups = FindObjectOfType<Pickups>();
        }
        RandomAttack();
        
        //transform.rotation = Quaternion.Euler(0,0, Time.deltaTime  *30);
    }

    void RandomAttack() {
        if (attackNum == 0) {
            Chase();
        }
        if (attackNum == 1) {
            RandomDash();
        }
        if (attackNum == 2) {
            Pounce();
        }
        if (attackNum == 3) {
            PickupTarget();
        }
    }

    void Chase() {
        Vector3 scale = new Vector3(1f,1f,1f);
        ChangeScale(scale);
        float speed = 0.75f;
        Vector3 target = player.transform.position;
        if (target == null) {
            transform.position = transform.position;
        }
        target.z = 0;
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
    }

    void RandomDash() {
        Vector3 scale = new Vector3(1f,1f,1f);
        ChangeScale(scale);
        float speed = 2.5f;
        if (target == transform.position | target == null) {
            target = (Random.insideUnitCircle * range);
            target.z = 0;
        }
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
    }

    void Pounce() {
        float speed = 2f;
        float radius = 2f;
        Vector3 target;
        colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        for (int i = 0; i < colliders.Length; i++) { 
            if (colliders[i].tag == "Player") {
                target = player.transform.position;
                transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
            }
            else {
                //RandomDash();
            }
        }
    }

    void PickupTarget() {
        Vector3 scale = new Vector3 (.25f, .25f, .25f);
        this.ChangeScale(scale);
        float speed = 1f;
        float radius = 3f; 
        Vector3 target = pickups.transform.position;
        colliders = Physics2D.OverlapCircleAll(new Vector2(this.transform.position.x, this.transform.position.y), radius); //need: to target different
        for (int i = 0; i < colliders.Length; i++) { 
            if (colliders[i].tag == "Pickup" && !tracking) {
                target = colliders[i].transform.position;
                tracking = true;  
            }    
        }
        
        target.z = 0;
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
        if (transform.position == target | target == null) {
            tracking = false;
        }
    }

    void ChangeScale(Vector3 scale) {
        if (transform.localScale != scale) {
            transform.localScale = scale;
        }
    }


    void OnCollisionEnter2D(Collision2D other) {

        if (other.collider.tag == "Player") {
            ExplosionCall(transform.position);
            Destroy(gameObject);   
        }
        // if (other.collider.tag == "Enemy" && this.attackNum == 1) {
        //     this.attackNum = Random.Range(0, 4);
        // }
    }


    void ExplosionCall(Vector3 target) {
        target.z = -1f;
        Instantiate(explosion, target, Quaternion.identity);
    }
}
