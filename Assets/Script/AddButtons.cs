using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddButtons : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Transform panel;
    [SerializeField]
    private GameObject Button_1;
    GameObject btn;
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < 8; i++)
        {
            // tao 8 cai button
            // gan 8 cai do vo pannel
            btn = Instantiate(Button_1);
            btn.name = "" + i;
            btn.transform.SetParent(panel, false);
        }
    }
}