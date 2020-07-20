using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManageUI : MonoBehaviour
{
    /* turn this into singleton */
    PlayerController player;
    public Text DashMeter;
    public Text Speed_;

    public GameObject restart;

    void Awake() {
        if (player == null) {
            Debug.Log("Assigned");
            player = FindObjectOfType<PlayerController>();  
            restart = GameObject.Find("restart");  
            Debug.Log(restart);
        }
        
    }

    void Update() {
        DashMeter.text = player.maxDashMeter.ToString();
        Speed_.text = player.speed.ToString();
    }

    public void DeadOptions () {
        restart.SetActive(true);
    }
    public void Restart() {
        restart.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
