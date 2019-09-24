using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    //handle to animation
    private Animator _anim;
    private Animator _swordAnimation;

    //Use this for initiallzation
	void Start ()
    {
        //assign handle to animation
        _anim = GetComponentInChildren<Animator>();
        _swordAnimation = transform.GetChild(1).GetComponent<Animator>();
	}
	
	// Update is called once per frame
	public void Move (float move)
    {
        //anim set float move
        _anim.SetFloat("Move",Mathf.Abs(move));
    }
    public void Jump(bool jumping)
    {
        _anim.SetBool("Jumping",jumping);
    }
    public void Attack()
    {
        _anim.SetTrigger("Attack");
        _swordAnimation.SetTrigger("SwordAnimation");
    }
    
}
