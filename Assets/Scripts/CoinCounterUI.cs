using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCounterUI : MonoBehaviour
{
    private TMP_Text coinText;
    private GameObject player;
    private PlatformerPlayer playerScript;

    // Start is called before the first frame update
    void Start()
    {
        coinText = GetComponent<TMP_Text>();

        player = GameObject.FindGameObjectWithTag("Player");

        if(player!=null){
            playerScript = player.GetComponent<PlatformerPlayer>();
        }

        if(player == null){
            Debug.Log("Player game object not found");
        }
        if(playerScript == null){
            Debug.Log("PlatformerPlayer Script not FOund");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(coinText != null && playerScript != null){
            coinText.text = "Coins: " + playerScript.CoinsCollected().ToString();
        }
    }
}
