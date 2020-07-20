using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    /*
    Create an list, push the attack type to the list
    based on the length of the list spawn enemies
     */
    // ChaserController enemyScript;
    public GameObject enemy;
    public int spawnMax = 5;

    public GameObject speedBoost;
    public int speedBoostCount;
    int maxSpeedBoost = 1;

    Vector3 spawnPoint; 
    float width;
    float height;

    public Collider2D[] colliders;
    public float radius;
    

    void Awake() {
        width = Camera.main.orthographicSize * Camera.main.aspect;
        height = Camera.main.orthographicSize;
        
    }

    void Update() {
        if (ChaserController.enemyCount < spawnMax && spawnMax < 150) {
            spawnPoint = Random.onUnitSphere * 4;
            spawnPoint.z = 0;
            Instantiate(enemy, RandomSpawn(), Quaternion.identity);
            //Debug.Log(spawnMax);
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            spawnMax += 1;
        }

        if (speedBoostCount < maxSpeedBoost)  {
            spawnPoint = Random.onUnitSphere * 4;
            spawnPoint.z = 0;
            Instantiate(speedBoost, RandomSpawn(), Quaternion.identity);
            speedBoostCount ++;
        }       
    }

    Vector3 RandomSpawn() {

        bool canSpawnHere = false;
        Vector3 spawnPos = new Vector3 (0,0,0);

        while(!canSpawnHere) {
            float ranWidth = Random.Range(-width , width );
            float ranHeight = Random.Range(-height , height );
            spawnPos = new Vector3(ranWidth, ranHeight, 0f);
            canSpawnHere = PreventSpawnOverlap(spawnPos);
            
            if(canSpawnHere) {
                //Debug.Log(spawnPos);
                return spawnPos;
                
            }
        }

        return spawnPos;
    }

    bool PreventSpawnOverlap(Vector3 spawnPos) { 
        colliders = Physics2D.OverlapCircleAll(transform.position, radius); 

        for (int i = 0; i < colliders.Length; i++) {
            Vector3 centerPoint = colliders[i].bounds.center;
            float cWidth = colliders[i].bounds.extents.x;
            float cHeight = colliders[i].bounds.extents.y;
            
            float leftExtent = centerPoint.x - cWidth;
            float rightExtent = centerPoint.x + cWidth;
            float lowerExtent = centerPoint.y - cHeight; 
            float upperExtent = centerPoint.y + cHeight; 
            float sizedup = 3f;
            if (spawnPos.x + sizedup >= leftExtent && spawnPos.x - sizedup <= rightExtent) {
                if (spawnPos.y + sizedup >= lowerExtent && spawnPos.y - sizedup <= lowerExtent) {
                    return false;
                }
            }
            return true;

        }
        return false;
    }
}
