using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Lock : MonoBehaviour
{
    public TextMeshProUGUI output;

    public DoorScript door;

    [SerializeField]private string codePIN;
    
    public void AddNumber(int num)
    {
        if(output.text == "ERROR")
        {
            output.text = "";
        }
        output.text += num.ToString();

        if(output.text.Length >= 4)
        {
            VerifyResult();
        }
    }

    void VerifyResult()
    {
        if(output.text == codePIN)
        {
            output.text = "PASS";
            door.isUnlocked = true;
        } else
        {
            output.text = "ERROR";
        }

    }

}
