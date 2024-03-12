using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Notifications : MonoBehaviour
{
    // ----------------------
    // Variables
    // ----------------------

    [Header("Notifications")]
    [SerializeField] GameObject prefab;
    [Space(5)]

    [SerializeField] float noticationsDuration;
    [SerializeField] float fadeDuration;

    string message;

    // ----------------------
    // Functions
    // ----------------------

    public void SetValues(string text)
    {
        // Set Values
        message = text;

        Debug.Log(1);

        // Call Functions
        if(message != "")
        {
            Debug.Log(5);
            StartCoroutine("ShowNotifications");
        }
    }

    IEnumerator ShowNotifications()
    {
        Debug.Log(2);
        // Create Prefab inside The Desired UI
        var instance = Instantiate(prefab);
        instance.transform.SetParent(transform);

        // Set Values
        TextMeshProUGUI textMesh = instance.GetComponent<TextMeshProUGUI>();
        CanvasGroup group = instance.GetComponent<CanvasGroup>();

        textMesh.text = message;
        group.alpha = 0;

        // Play Animation
        group.DOFade(1, fadeDuration).SetEase(Ease.InOutCirc);

        // Wait For ???
        yield return new WaitForSeconds(noticationsDuration + fadeDuration);

        // Play Animation
        group.DOFade(0, fadeDuration).SetEase(Ease.InOutCirc);

        // Wait For ???
        yield return new WaitForSeconds(fadeDuration);

        Destroy(instance);
    }
}
