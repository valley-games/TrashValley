using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform previousRoom;
    public Transform nextRoom;
    public CameraController cam;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.tag == "Player"){
            
            if (collision.transform.position.x < transform.position.x) {
                cam.MoveToNewRoom(nextRoom);
                Debug.Log("Cam X: " + nextRoom.position.x);
            }
            else{
                cam.MoveToNewRoom(previousRoom);
                Debug.Log("Cam X: " + previousRoom.position.x);
            }
        }
    }
}
