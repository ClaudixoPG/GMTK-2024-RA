using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {


    private const string IS_WALKING = "IsWalking";
    private const string SPEED = "Speed";


    [SerializeField] private Player player;


    private Animator animator;


    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        animator.SetFloat(SPEED, player.GetNormalizedSpeed());
    }
}