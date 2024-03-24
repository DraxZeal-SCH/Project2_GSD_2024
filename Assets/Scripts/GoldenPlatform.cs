using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldenPlatform : MonoBehaviour
{
    [SerializeField]
    private Vector3 finishPos = Vector3.zero;
    [SerializeField]
    private float speed = 0.5f;
    [SerializeField]
    private TextMeshProUGUI spawnLocationText;
    
    private bool playerOnPlatform = false;
    private bool platformSpawned = false;
    private GameObject player;
    private PlatformerPlayer playerScript;
    private SpriteRenderer platformRenderer;
    private BoxCollider2D platformCollider;

    private Vector3 startPos;
    private float trackPercent = 0;
    private int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        ToggleText(false);

        platformRenderer = GetComponent<SpriteRenderer>();

        platformCollider = GetComponent<BoxCollider2D>();

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
        if(platformRenderer == null){
            Debug.Log("Platform Renderer not found");
        }
        if (spawnLocationText == null){
        Debug.Log("Spawn Location text not assigned in the editor");
        }
        
        startPos = transform.position;


        HidePlatform();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerScript != null && playerScript.CoinsCollected() >=10 && !platformSpawned){
            ShowPlatform();
            ToggleText(true);
        }

        
        if(playerOnPlatform){
            trackPercent += direction * speed * Time.deltaTime;
            float x = (finishPos.x - startPos.x) * trackPercent + startPos.x;
            float y = (finishPos.y - startPos.y) * trackPercent + startPos.y;
            if (y >= 22f)
            {
                y = 22f;
                playerOnPlatform = false; // Stop updating the platform's position
            }
            transform.position = new Vector3(x, y, startPos.z);
        }
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = true;
        }
    }

    void ShowPlatform(){
        platformRenderer.enabled = true;
        platformCollider.enabled = true;
            platformSpawned = true;
    }
    void HidePlatform(){
        platformRenderer.enabled = false;
        platformCollider.enabled = false;
    }
    void ToggleText(bool isVisible)
    {
        if (spawnLocationText != null)
        {
            spawnLocationText.gameObject.SetActive(isVisible);
        }
    }

}
