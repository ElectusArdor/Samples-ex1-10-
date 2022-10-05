using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwoNumbersCompare : MonoBehaviour
{
    [SerializeField] private InputField inputField_1, inputField_2;
    [SerializeField] private Text answerText, warningText;
    private float num1, num2;

    private void ClearWarning()
    {
        warningText.text = "";
    }

    public void OnCompareClick()
    {
        if (float.TryParse(inputField_1.text, out num1) && float.TryParse(inputField_2.text, out num2))
        {
            if (num1 > num2)
                answerText.text = num1.ToString();
            else if (num1 < num2)
                answerText.text = num2.ToString();
            else
                answerText.text = "Равны";

            ClearWarning();
        }
        else
        {
            warningText.text = "Введите числа корректно!";
            answerText.text = "";
        }
    }
}
