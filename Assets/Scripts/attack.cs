using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    public float attackCooldown;
    public Transform firePoint;
    public GameObject[] fireballs;

    private Animator anim;
    private controller playerMovement;  //TODO remane controller script to Controller 
    private float cooldownTimer = Mathf.Infinity;

    private void Awake(){
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<controller>();
    }
    
    // Update is called once per frame
    void Update(){
        if (Input.GetKey(KeyCode.Space) && cooldownTimer > attackCooldown && playerMovement.canAttack()) Attack();
        cooldownTimer += Time.deltaTime;
    }

    private void Attack(){
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<projectile>().SetDirection(-Mathf.Sign(transform.localScale.x));
    }
    private int FindFireball(){
        for (int i = 0; i < fireballs.Length; i++){
            if (!fireballs[i].activeInHierarchy)    return i;
        }
        return 0;
    }
}
