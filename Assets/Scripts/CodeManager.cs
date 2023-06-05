using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeManager : MonoBehaviour
{
    // Start is called before the first frame update
    public string code;
    public DoorScript DoorToUnlock;
    public bool isLastCode;
    public Digit[] digits;


    string concatedCode;

    public static CodeManager Instance;

    void Start()
    {
       
    }




    public void VerifyDigit()
    {
        ConcatDigits();
        if(concatedCode == code)
        {
            if (!isLastCode)
            {
                DoorToUnlock.isUnlocked = true;
                gameObject.SetActive(false);
            } else
            {
                GameManager.instance.WinGame();
            }
        }
    }


    void ConcatDigits()
    {
        concatedCode = "";
        foreach (var dig in digits)
        {
            concatedCode += dig.currentDigit.ToString();
        }
    }
}
