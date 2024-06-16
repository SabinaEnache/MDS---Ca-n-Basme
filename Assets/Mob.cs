using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Mob : MonoBehaviour
{
    public float speed;
    public float range;
    public CharacterController controller;

    public AnimationClip walk;
    public AnimationClip idle;
    public Transform player;

    private int Health;
    void Start()
    {
        Health=100;
    }
    public void GetHit(int damage){
        Health-=damage;
        if(Health<=0){
            Health=0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead()){
            if (inRange())
            {
                chase();
            }
            else
            {
                GetComponent<Animation>().CrossFade(idle.name);
            }
        }else{
            //Animation.play(Die.name);
        }
    }
    bool inRange()
    {
        if (Vector3.Distance(transform.position, player.position) < range)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
void chase()
{
    float distanceToPlayer = Vector3.Distance(transform.position, player.position);
    if (distanceToPlayer > 0.2f * range)
    {
        transform.LookAt(player.position);
        Vector3 moveDirection = player.position - transform.position;
        moveDirection.y = 0;
        moveDirection.Normalize();
        transform.position += moveDirection * speed * Time.deltaTime;
        GetComponent<Animation>().Play(walk.name);
    }
    else
    {
        transform.LookAt(player.position);
        // Calculăm poziția la 0.5 range de player
        Vector3 targetPosition = player.position - transform.forward * 0.2f * range;
        // Asigurăm că mob-ul nu trece de poziția țintă
        if (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            Vector3 moveDirection = targetPosition - transform.position;
            moveDirection.y = 0;
            moveDirection.Normalize();
            transform.position += moveDirection * speed * Time.deltaTime;
        }
        GetComponent<Animation>().Stop(walk.name);
    }
}



    bool isDead(){
        if(Health<=0){
            return true;
        }else{ return false;}
    }
    void onMouseOver()
    {
        //player.GetComponent<Fighter>().opponent=gameObject;
    }
}
