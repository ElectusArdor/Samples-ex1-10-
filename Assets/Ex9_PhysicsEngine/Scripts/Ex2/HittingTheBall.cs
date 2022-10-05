using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittingTheBall : MonoBehaviour
{
    [SerializeField] private float startSpeed;

    private float tmr = 1.5f;

    void Update()
    {
        if (tmr < 10)
            tmr -= Time.deltaTime;

        if (tmr <= 0)
        {
            Hit();
            tmr = 100;
        }
    }

    private void Hit()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(0, 0, -startSpeed, ForceMode.VelocityChange);
    }
}
