using UnityEngine;
using System.Collections;

public class Move_man_temp : MonoBehaviour {
    Animator animator;
	// Use this for initialization
	void Start () {
        animator=GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKey("w")){

            animator.SetFloat("right", Mathf.Lerp(animator.GetFloat("right"), 0f, Time.deltaTime));
            animator.SetFloat("up", Mathf.Lerp(animator.GetFloat("up"), 1f, Time.deltaTime));
        }
        if(Input.GetKey("s")){
            animator.SetFloat("right", Mathf.Lerp(animator.GetFloat("right"), 0f, Time.deltaTime));
            animator.SetFloat("up", Mathf.Lerp(animator.GetFloat("up"), -1f, Time.deltaTime));
        }
        if(Input.GetKey("a")){
            animator.SetFloat("right", Mathf.Lerp(animator.GetFloat("right"), -1f, Time.deltaTime));
            animator.SetFloat("up", Mathf.Lerp(animator.GetFloat("up"), 0f, Time.deltaTime));
        }
        if(Input.GetKey("d")){
            animator.SetFloat("right", Mathf.Lerp(animator.GetFloat("right"), 1f, Time.deltaTime));
            animator.SetFloat("up", Mathf.Lerp(animator.GetFloat("up"), 0f, Time.deltaTime));
        }
 
	}
}
