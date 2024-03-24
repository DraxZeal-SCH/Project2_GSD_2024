using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float smoothTime = 0.2f;
    private Vector3 velocity = Vector3.zero;
    private bool followTarget = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void LateUpdate() {
        if (target != null && followTarget) {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

            if(target.position.y < 0){
                followTarget = false;
            }
        }
}
}
