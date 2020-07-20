using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRadius : MonoBehaviour
{
    PlayerController player;
    [SerializeField]
    float scale = 2f;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {   
        transform.localScale = new Vector3(player.dashMeter * scale, player.dashMeter * scale, 1);
    }
}
