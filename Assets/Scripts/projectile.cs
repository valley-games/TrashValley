using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public float speed;
    private bool hit;
    private float direction;
    private float lifetime;

    private Animator anim;
    private BoxCollider2D boxCollider;
    private AudioSource source;

    
    private void Awake(){
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update(){ 
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if(lifetime > 5) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if((collision.gameObject.tag == "door") || (collision.gameObject.tag == "fireball")) return;
        else{
            hit = true;
            boxCollider.enabled = false;
            anim.SetTrigger("explode");
            if (collision.gameObject.tag == "block")  {
                //collision.gameObject.GetComponent<AudioSource>().Play();
                //collision.gameObject.GetComponent<Renderer>().enabled = false;
                //if(!collision.gameObject.GetComponent<AudioSource>().isPlaying) collision.gameObject.SetActive(false);
                collision.gameObject.SetActive(false);
                source.Play();
            }

            Debug.Log(collision.gameObject.tag);
        }
    }

    public void SetDirection(float _direction){
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction) localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    public void Deactivate(){
        gameObject.SetActive(false);
    }
}
