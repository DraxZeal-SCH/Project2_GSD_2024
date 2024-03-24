using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyCounter : MonoBehaviour
{
    private TMP_Text enemyText;
    private GameObject player;
    private PlatformerPlayer playerScript;

    // Start is called before the first frame update
    void Start()
    {
        enemyText = GetComponent<TMP_Text>();

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
        if(enemyText != null && playerScript != null){
            enemyText.text = "Enemies Killed: " + playerScript.EnemyCounter().ToString();
        }
    }
}
