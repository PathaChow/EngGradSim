using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveAnimSelect : MonoBehaviour
{
    Animator animator;
    [SerializeField] int characterNum;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();

        characterNum = PlayerPrefs.GetInt("Character");
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("PAnim" + characterNum);

    }

    private void Update()
    {
        characterNum = PlayerPrefs.GetInt("Character");
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("PAnim" + characterNum);

    }
}
