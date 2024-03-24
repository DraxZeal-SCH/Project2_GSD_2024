using UnityEngine;

public class AttachPlayer : MonoBehaviour
{
    private string platformTag = "Platforms";
    private Vector3 originalScale;

    private void Start(){
        originalScale = transform.localScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object has the specified tag
        if (collision.CompareTag(platformTag))
        {
            // Make the player a child of the collided platform
            Transform platformTransform = collision.transform;
            transform.SetParent(platformTransform, true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the collided object has the specified tag
        if (collision.CompareTag(platformTag))
        {
            // Unset the parent to detach the player from the platform
            transform.SetParent(null);
        }
    }
}
