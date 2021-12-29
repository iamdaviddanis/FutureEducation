using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_attack : StateMachineBehaviour
{

    public int stav;
    private GameObject player;
    private player_main player_script;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

     //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(stav >=0)
        {
            if(!animator.GetBool("zije"))
                    animator.SetInteger("status",10);  
            else 
                animator.SetInteger("status",stav); 
        }
        else
        {
            if(stav == - 11)
            {
                animator.SetInteger("status",1);  
                animator.SetInteger("hp",100);  
                animator.SetBool("zije",true);
            }
            else if(stav == - 777)
            {
                 int random=Random.Range(-2,-4);
                 Debug.Log("NAHOAD + "+random);
                animator.SetInteger("status",random);  

            }
            else
                animator.SetInteger("status",stav); 
        }

        player=GameObject.FindWithTag("player");
        player_script=player.GetComponent<player_main>();
        player_script.hit(5);
        Debug.Log("HIT PLAYER");
         
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
