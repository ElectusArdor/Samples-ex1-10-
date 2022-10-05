using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageLifeTime : MonoBehaviour
{
    [SerializeField] private Text warningMessage;
    public float alpha, savePositionTmr;

    void Start()
    {
        alpha = 1.6f;
        savePositionTmr = 1f;
    }

    void Update()
    {
        alpha -= Time.deltaTime * 0.6f;
        savePositionTmr -= Time.deltaTime;

        warningMessage.color = new Color(1, 0, 0, alpha);

        if (savePositionTmr < 0)
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + Time.deltaTime * 250f);

        if (alpha < 0)
        {
            alpha = 1.6f;
            savePositionTmr = 1f;

            transform.localPosition = Vector2.zero;
            gameObject.SetActive(false);
        }
    }
}
