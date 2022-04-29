using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [SerializeField] float stateDuration = 5.0f;

    float timeRemaining = Mathf.Infinity;

    void Start()
    {
        SwitchState(new TestPlayerState(this));
    }
}