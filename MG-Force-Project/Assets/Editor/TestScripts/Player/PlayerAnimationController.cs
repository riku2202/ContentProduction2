using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    enum Animation
    {
        NONE,
        IDLE_L,
        IDLE_R,
        RUN_L,
        RUN_R,
        JUMP_L,
        JUMP_R,
        SHOOT_0_L,
        SHOOT_0_R,
        SHOOT_45_L,
        SHOOT_45_R,
        SHOOT_90_L,
        SHOOT_90_R,
        SHOOT_135_L,
        SHOOT_135_R,
    }

    [NamedSerializeField(
        new string[]
        {
            "Idle_L",
            "Idle_R",
            "Run_L",
            "Run_R",
        }
    )]
    [SerializeField]
    private AnimationClip _playerAnimation;

    private void Start()
    {
        
    }
}