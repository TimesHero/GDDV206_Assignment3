using UnityEngine;

public class BurnStateBehavior : StateMachineBehaviour
{
    public AudioClip burnSfx; 
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<AudioSource>().PlayOneShot(burnSfx); 
    }
    
}
