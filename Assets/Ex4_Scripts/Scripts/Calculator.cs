using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calculator : MonoBehaviour
{
    [SerializeField] private InputField inputField_1, inputField_2;
    [SerializeField] private Text answerText, warningText;
    private float answer;
    private float num1, num2;

    private void ClearWarning()
    {
        warningText.text = "";
    }

    public void OnCalcClick(string operation)
    {
        if (float.TryParse(inputField_1.text, out num1) && float.TryParse(inputField_2.text, out num2))
        {
            if (operation == "Plus")
            {
                answer = num1 + num2;
                answerText.text = answer.ToString();
                ClearWarning();
            }
            else if (operation == "Minus")
            {
                answer = num1 - num2;
                answerText.text = answer.ToString();
                ClearWarning();
            }
            else if (operation == "Multiply")
            {
                answer = num1 * num2;
                answerText.text = answer.ToString();
                ClearWarning();
            }
            else
            {
                if (num2 != 0)
                {
                    answer = num1 / num2;
                    answerText.text = answer.ToString();
                    ClearWarning();
                }
                else
                {
                    warningText.text = "Нельзя делить на '0'!";
                    answerText.text = "";
                }
            }
        }
        else
        {
            warningText.text = "Введите числа корректно!";
            answerText.text = "";
        }
    }
}
