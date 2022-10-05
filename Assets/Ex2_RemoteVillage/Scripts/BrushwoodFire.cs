using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushwoodFire : MonoBehaviour
{
    private float tmr, flashTime;
    [SerializeField]private bool flashOn, flashOff;
    private Vector3 targetScale;
    private Quaternion targetRotation;

    void Start()
    {
        tmr = 0;
        flashTime = Random.Range(0.1f, 1f);
        flashOn = false;
        flashOff = false;
        transform.localScale = new Vector3(0.5f, 0.3f, 0.5f);
    }

    void Update()
    {
        tmr += Time.deltaTime;

        if (tmr > flashTime & !flashOff & !flashOn)
        {
            flashOn = true;
            flashTime = Random.Range(0, 0.7f);
            targetScale = new Vector3(Random.Range(1.1f, 1.5f), Random.Range(1.01f, 1.3f), Random.Range(1.1f, 1.5f));
            targetRotation = Quaternion.Euler(Random.Range(-20f, 90f), 0, Random.Range(-30f, 30f));
        }

        if (flashOn)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * 15f);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * 10f);
            if (transform.localScale.magnitude >= targetScale.magnitude * 0.98f)
            {
                flashOn = false;
                flashOff = true;
                targetScale = new Vector3(0.5f, 0.3f, 0.5f);
                targetRotation = Quaternion.Euler(0, 0, 0);
            }
        }

        if (flashOff)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * 15f);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * 10f);
            if (transform.localScale.magnitude <= targetScale.magnitude * 1.02f)
            {
                tmr = 0;
                flashOff = false;
            }
        }
    }
}
