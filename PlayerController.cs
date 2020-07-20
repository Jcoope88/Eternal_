using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 mousePosition;
    ManageUI manageUI;
    public float dashMeter = 5f;
    [HideInInspector]
    public float speed = 10f;
    [HideInInspector]
    //public int health;
    public float maxDashMeter = 5f;
    private float dashDistance;
    private bool dashing;

    private float regen = .1f;

    void Start() {
        dashing = false;
        //health = 5;
    }

    void Update() {
        if (maxDashMeter < 0f) {
        //if (health <= 0) {
            Die();
        }
        Dash();

        if( Input.GetKeyDown(KeyCode.R)) {
            manageUI.Restart();
        }
    } 

    void FixedUpdate() {
        if(!dashing) {  //regen method
            if (dashMeter < maxDashMeter) {
                dashMeter += regen;
            } 
        }
    } 

    void Dash() {
        //controls
        if (Input.GetKeyDown(KeyCode.Mouse0) & dashing != true & dashMeter > 0f) { //start dash
            dashing = true;
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            dashDistance = Vector3.Distance(mousePosition, transform.position) * 2;

            if(dashDistance > dashMeter) {
                mousePosition = transform.position;
                //out of range signal to player??
            }
            else {
                dashMeter -= dashDistance;
            }
        }
        
        transform.position = Vector3.MoveTowards(transform.position, mousePosition, Time.deltaTime * speed); //move method
            
        if(transform.position.x == mousePosition.x & transform.position.y == mousePosition.y) { //disable double dashing
            dashing = false;
        }
    }

    void Die() {
        Debug.Log("Player Died");
        manageUI.DeadOptions();
        gameObject.SetActive(false);
    }

    public void ChangeSpeed(float vSpeed) {
        speed += vSpeed;
    }

    public void ChangeMaxDash(float vMaxDash) {
        maxDashMeter += vMaxDash;
    }

    public void ChangeDashMeter(float vDashMeter) {
        dashMeter += vDashMeter;
    }

    public void ChangeRegen(float vRegen) {
        regen += vRegen;
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.tag == "Enemy") {
            if (!dashing) {
                this.ChangeDashMeter(-1f);
            }
            this.ChangeMaxDash(-1f);
            
        }
        
        if (other.collider.tag == "Pickup")  {
            speed += 2f;
            dashMeter = maxDashMeter;
        }
    }


}
