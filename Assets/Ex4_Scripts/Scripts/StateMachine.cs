using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private GameObject currentState;

    public void ChangeState(GameObject state)
    {
        if (currentState != null)
        {
            currentState.SetActive(false);
        }
        state.SetActive(true);
        currentState = state;
    }
}
