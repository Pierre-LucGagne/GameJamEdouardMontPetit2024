using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class NumPadScript : MonoBehaviour
{
    // ----------------------
    // Class
    // ----------------------

    [System.Serializable]
    public class Audios
    {
        public RandomAudio enterNumber;
        [Space(5)]

        public RandomAudio deny;
        public RandomAudio accept;     
    }

    // ----------------------
    // Variables
    // ----------------------

    [Header("NumPad")]
    [SerializeField] string password;
    [SerializeField] UnityEvent acceptEvent;
    string enteredPassword;
    bool alreadyCompleted;
    [Space(10)]

    [SerializeField] Audios audios;

    // ----------------------
    // Functions
    // ----------------------

    public void AddNumber(int number)
    {
        if(!alreadyCompleted)
        {
            // Add Number to password
            enteredPassword += number;

            // Play Audio
            if(audios.enterNumber)
            audios.enterNumber.PlayAudio();

            // Call Functions
            VerifyCode();
        }
    }

    void VerifyCode()
    {
        // Verify Code Result
        if(enteredPassword.Length >= password.Length && !alreadyCompleted)
        {
            if(enteredPassword == password)
            {
                // Play Audio
                if(audios.accept)
                audios.accept.PlayAudio();

                acceptEvent.Invoke();
                alreadyCompleted = true;
            }

            else
            {
                // Play Audio
                if(audios.deny)
                audios.deny.PlayAudio();

                enteredPassword = "";
            }
        }

        Debug.Log(enteredPassword + ", " + password);
    }
}