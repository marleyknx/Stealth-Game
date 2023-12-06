using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerAnimation : MonoBehaviour
{
    
    public Animator animator;
    public  readonly int Walk = Animator.StringToHash("Walk");
    public  readonly  int Crouching = Animator.StringToHash("IsCrouching");
  

  


    public void SetWalk(float input) =>   animator.SetFloat(Walk, input );

    public void SetIsCrouch(bool IsCrouch) => animator.SetBool(Crouching, IsCrouch);

}
