using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    public float speed;
    public float range;
    public CharacterController controller;

    public AnimationClip walk;
    public AnimationClip idle;
    public Transform player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!inRange())
        {
            chase();
        }
        else
        {
            GetComponent<Animation>().CrossFade(idle.name);
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
        transform.LookAt(player.position);
        controller.SimpleMove(transform.forward * speed);
        GetComponent<Animation>().Play(walk.name);
    }
    void onMouseOver()
    {
        player.GetComponent<Fighter>().opponent=gameObject;
    }
}
