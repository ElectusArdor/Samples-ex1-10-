using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rainbow : MonoBehaviour
{
    [SerializeField] private bool redOn, greenOn, blueOn;
    [SerializeField] private float speed;

    private float red, green, blue, delta;
    private Light lt;

    private void Start()
    {
        lt = GetComponent<Light>();

        red = lt.color.r;
        green = lt.color.g;
        blue = lt.color.b;
    }

    void Update()
    {
        delta = Time.deltaTime * speed;

        if (redOn)
            red += delta;
        else
            red -= delta;

        if (greenOn)
            green += delta;
        else
            green -= delta;

        if (blueOn)
            blue += delta;
        else
            blue -= delta;

        red = Mathf.Clamp(red, 0, 1f);
        green = Mathf.Clamp(green, 0, 1f);
        blue = Mathf.Clamp(blue, 0, 1f);

        if (lt.color.g <= 0)
            redOn = true;
        if (lt.color.g >= 1)
            redOn = false;

        if (lt.color.b <= 0)
            greenOn = true;
        if (lt.color.b >= 1)
            greenOn = false;

        if (lt.color.r <= 0)
            blueOn = true;
        if (lt.color.r >= 1)
            blueOn = false;

        lt.color = new Color(red, green, blue);
    }
}
