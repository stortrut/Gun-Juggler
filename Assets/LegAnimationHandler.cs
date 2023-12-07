using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegAnimationHandler : MonoBehaviour
{

    [SerializeField] Animator legs;

    [SerializeField] float animationFrame;
    [SerializeField] float length;


    bool isPlayingForwardAnimation;


    private void Update()
    {
        animationFrame = legs.GetCurrentAnimatorClipInfo(0)[0].clip.length * (legs.GetCurrentAnimatorStateInfo(0).normalizedTime % 1) * legs.GetCurrentAnimatorClipInfo(0)[0].clip.frameRate;


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            animationFrame = 8 - (legs.GetCurrentAnimatorStateInfo(0).normalizedTime % legs.GetCurrentAnimatorStateInfo(0).length);
            legs.Play("legs", 0, animationFrame);

        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            animationFrame = legs.GetCurrentAnimatorStateInfo(0).normalizedTime % legs.GetCurrentAnimatorStateInfo(0).length;
            legs.Play("legs_reverse", 0, animationFrame);
        }
    }



    public void SetDirectionToForwards()
    {
        if (isPlayingForwardAnimation) { return; }
        isPlayingForwardAnimation = true;

        animationFrame = (legs.GetCurrentAnimatorStateInfo(0).normalizedTime % 8);
        length = legs.GetCurrentAnimatorStateInfo(0).length;
        animationFrame = legs.GetCurrentAnimatorClipInfo(0)[0].clip.length * (legs.GetCurrentAnimatorStateInfo(0).normalizedTime % 1) * legs.GetCurrentAnimatorClipInfo(0)[0].clip.frameRate;

        //legs.SetBool("Reverse", false);


        //AnimatorClipInfo[] animationClip = legs.GetCurrentAnimatorClipInfo(0);
        legs.Play("legs", 0, 8 - animationFrame);
    }

    public void SetDirectionToBackwards()
    {
        if (!isPlayingForwardAnimation) { return; }
        isPlayingForwardAnimation = false;

        animationFrame = legs.GetCurrentAnimatorStateInfo(0).normalizedTime % /*legs.GetCurrentAnimatorStateInfo(0).length*/8;

        length = legs.GetCurrentAnimatorClipInfo(0)[0].clip.frameRate;

        animationFrame = legs.GetCurrentAnimatorClipInfo(0)[0].clip.length * (legs.GetCurrentAnimatorStateInfo(0).normalizedTime % 1) * legs.GetCurrentAnimatorClipInfo(0)[0].clip.frameRate;


        //legs.SetBool("Reverse", true);

        legs.Play("legs_reverse", 0, animationFrame);
    }

    public void PauseAnimation(bool pause)
    {
        if (pause)
            legs.speed = 0;
        else
            legs.speed = 1;
    }

}
