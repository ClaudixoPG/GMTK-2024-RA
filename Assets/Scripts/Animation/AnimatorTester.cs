using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorTester : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator != null)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                animator.SetBool("missionCompleted",true);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                animator.SetTrigger("wrongObject");
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                animator.SetTrigger("missionCompleted");
            }
        }
    }
}
