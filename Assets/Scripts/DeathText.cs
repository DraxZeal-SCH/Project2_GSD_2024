using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI deathText;
    private GameObject player;
    private PlatformerPlayer playerScript;
    // Start is called before the first frame update
    void Start()
    {
        ToggleDeathText(false);

        player = GameObject.FindGameObjectWithTag("Player");

        if(player!=null){
            playerScript = player.GetComponent<PlatformerPlayer>();
        }

        if(player == null){
            Debug.Log("Player game object not found");
        }
        if(playerScript == null){
            Debug.Log("Platformer Player Script not found");
        }
        if (deathText == null){
        Debug.Log("Death Text object not assigned in the editor");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerScript.bossStatus())
        {
            ToggleDeathText(true);
        }
        else
        {
            ToggleDeathText(false);
        }
    }
    void ToggleDeathText(bool isVisible)
    {
        if (deathText != null)
        {
            deathText.gameObject.SetActive(isVisible);
        }
    }
}
