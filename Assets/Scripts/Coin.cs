using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private GameObject player;
    private PlatformerPlayer platformerPlayer;

    // Start is called before the first frame update
    void Start()
    {
        if(player == null){
            player = GameObject.FindWithTag("Player");
        }
        if(player !=null){
        platformerPlayer = player.GetComponent<PlatformerPlayer>();

            if(platformerPlayer == null){
                Debug.Log("Platformer Player compenent Not Found");
            }
        }
        else{
            Debug.Log("Player Gameobject not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player") && platformerPlayer != null){
            platformerPlayer.CoinAdded();
            Destroy(gameObject);
        }
    }
}
