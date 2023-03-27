using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsZvi : MonoBehaviour
{
    [SerializeField]
    private Transform puzzleField;

    [SerializeField]
    private GameObject button;

    private void Awake()
    {
        for (int i = 0; i < 16; i++)
        {
            GameObject _button = Instantiate(button);
            _button.name = "" + i;
            _button.transform.SetParent(puzzleField, false);
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
