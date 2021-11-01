using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idk : MonoBehaviour
{
   	public Animator animator;
    void Start()
    {
    	kys(true);
    }

    

   	public void kys(bool v)
   	{
   		Rigidbody[] bodies=GetComponentsInChildren<Rigidbody>();
    	foreach(Rigidbody body in bodies)
    	{
    		body.isKinematic=v;
    		
    	}
   	}

    void Update()
    {
        if(Input.GetKey("f"))
        {
        	animator.enabled=false;
        	Rigidbody[] bodies=GetComponentsInChildren<Rigidbody>();
	    	foreach(Rigidbody body in bodies)
	    	{
	    		body.isKinematic=false;
	    		
	    	}
        }
    }
}
