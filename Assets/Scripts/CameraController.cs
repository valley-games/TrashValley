using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    //Follow player
    public Transform player;
    public float aheadDistance;
    public float cameraSpeed;
    private float lookAhead;
    
    // Update is called once per frame
    void Update() {
        //Room camera
        //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), ref velocity, speed);

        //Follow player
        transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * -player.localScale.x), Time.deltaTime * cameraSpeed);    
    }

    public void MoveToNewRoom(Transform _newRoom){
        currentPosX = _newRoom.position.x;
    }
}
