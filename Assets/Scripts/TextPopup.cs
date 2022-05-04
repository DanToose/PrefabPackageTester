using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPopup : MonoBehaviour
{
    private string initialMessage;
    public string[] popupTexts;
    public KeyCode keyToAdvanceText;
    private int textNumber;
    private bool fieldIsActive;
    public Text textField;
    private bool keyWasPressed;


    // Start is called before the first frame update
    void Start()
    {
        initialMessage = textField.text;
        fieldIsActive = false;
        textNumber = 0;
    }

    // Update is called once per frame

    private void Update()
    {
        keyWasPressed = false;
        keyWasPressed = Input.GetKeyDown(keyToAdvanceText);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && fieldIsActive == false)
        {
            textField.gameObject.SetActive(true);
            fieldIsActive = true;
        }
        
        if (fieldIsActive == true && keyWasPressed)
        {
            if (textNumber >= popupTexts.Length)
            {
                ResetTextField();
            }
            if (textNumber < popupTexts.Length)
            {
                textField.text = popupTexts[textNumber];
                textNumber++;
            }
        }         
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && fieldIsActive == true)
        {
            ResetTextField();
        }
    }

    private void ResetTextField()
    {
        textField.text = initialMessage;
        textField.gameObject.SetActive(false);
        fieldIsActive = false;
        textNumber = 0;
    }
}
