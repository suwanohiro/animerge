using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Name : MonoBehaviour
{
    static new string name="";

    [SerializeField] TMP_InputField inputfield;

    private string text="";

    // Start is called before the first frame update
    void Start()
    {
        if (inputfield != null)
        {
            inputfield = inputfield.GetComponent<TMP_InputField>();
        }
    }
    public void ChangeName()
    {
        text = inputfield.text;
        if (!string.IsNullOrEmpty(text))
        {
            text.Trim();
            inputfield.text = Regex.Replace(text, @"[^‚ -‚ñƒJ-ƒ–A-z‚`-‚š0-9‚O-‚X]", "");
            Debug.Log("name " + name);

            Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
            byte[] bytes = sjisEnc.GetBytes(text);
            if (bytes.Length <= 10)
            {
                name = text;
            }
            else
            {
                byte[] num = new byte[10];
                for (int i = 0; i < 10; i++)
                {
                    num[i] = bytes[i];
                }
                name = sjisEnc.GetString(num);
                inputfield.text = name;
                Debug.Log("byte " + bytes);
            }
        }
    }
}
