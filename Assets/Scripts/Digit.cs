using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Digit : MonoBehaviour
{
    // Start is called before the first frame update

    public int currentDigit;
    public TextMeshProUGUI digitText;

    CodeManager codeManager;
    void Start()
    {
        codeManager = transform.parent.GetComponent<CodeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateDigit(bool increase)
    {

        if(increase)
        {
            if(currentDigit < 9)
            {
                currentDigit++;
            }
        } else
        {
            if(currentDigit > 0)
            {
                currentDigit--;
            }
        }

        digitText.text = currentDigit.ToString();
        codeManager.VerifyDigit();
    }
}

