using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public Transform target;
    public float speed;
    public float distance;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) > distance){
            transform.position = Vector3.Lerp(transform.position, target.position, speed);
            transform.LookAt(target);

            anim.Play("Walk");
        }
        else anim.Play("Idle_A");
    }
}
