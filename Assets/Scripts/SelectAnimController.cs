using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAnimController : MonoBehaviour
{
    Animator animator;
    [SerializeField] int characterNum;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();

        characterNum = PlayerPrefs.GetInt("Character");
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("PAnim" + characterNum);

    }
}
