using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    bool collided;
    PlayerController player;
    /* bug report 

    create different types of pickups speedboost, dashdistance, health pack
    ?*/

    Spawner spawner;
    int value;

    void Start() {
        player = FindObjectOfType<PlayerController>();
        spawner = FindObjectOfType<Spawner>();
        collided = false;
        value = (int)Random.Range(0f, 2f);
        State(value);
        
    }
    void OnCollisionEnter2D(Collision2D other) {

        if (collided == false) {
            collided = true;
            if (other.collider.tag == "Enemy") {
                spawner.spawnMax ++;
            }
            if (other.collider.tag == "Player") {
                StateInteraction(value);
            }
            //Debug.Log(other.collider.tag);
            Destroy(gameObject);
            spawner.speedBoostCount -= 1;
        
        }
        
    }
    void StateInteraction(int value_) { //move to player controller
        switch (value_) {
            case 1:
                player.ChangeMaxDash(1f);
                break;
            case 2:
                player.ChangeRegen(.02f);
                break;
        }

    }
    void State(int value_) {
        switch (value_) {
            case 1:
                transform.localScale = new Vector3(.7f,.7f,1f);
                break;
            case 2:
                transform.localScale = new Vector3(.5f,.5f,.5f);
                break;

        }
    }

}
