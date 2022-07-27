using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayName : MonoBehaviour
{
    [SerializeField] public Text nama;
    [SerializeField] public InputField inputNama;
    // Start is called before the first frame update
    void Start()
    {
        nama.text = PlayerPrefs.GetString("userName") == "" ? Environment.UserName : PlayerPrefs.GetString("userName");
    }

    // Update is called once per frame
    public void Create()
    {
        nama.text = inputNama.text;
        PlayerPrefs.SetString("userName", nama.text);
        PlayerPrefs.Save();
    }
}
