using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetUp : MonoBehaviour
{
    private float getUpTmr;

    void Update()
    {
        if (Mathf.Abs(transform.rotation.x) > 0.1f | Mathf.Abs(transform.rotation.z) > 0.1f)
        {
            getUpTmr += Time.deltaTime;
            if (getUpTmr > 3)
            {
                transform.rotation = Quaternion.Euler(0, transform.rotation.y * Mathf.Rad2Deg, 0);
                getUpTmr = 0;
            }
        }

    }
}
