using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class OpenPage : MonoBehaviour
{
    // ----------------------
    // Class
    // ----------------------

    [System.Serializable]
    public class UISettings
    {
        public CanvasGroup page;
        public float fadeDuration;
        [Space(5)]

        public TextMeshProUGUI title;
        public TextMeshProUGUI text;
    }

    // ----------------------
    // Variables
    // ----------------------

    [HideInInspector]
    public bool modaleIsOpen;
    Vector3 playerPosWhenOpened;

    [Header("Modale Settings")]
    public float distanceBeforeClosing;
    [Space(10)]

    [SerializeField] RandomAudio interactionAudio;
    [SerializeField] UISettings ui;

    // ----------------------
    // Functions
    // ----------------------

    private void FixedUpdate()
    {
        // Set Values
        float distance = Vector3.Distance(playerPosWhenOpened, transform.position);

        // Check Distance
        if(distance > distanceBeforeClosing && modaleIsOpen)
        CloseModale();
    }

    // Modale Functions
    // ----------------------

    public void OpenModale(PageData page)
    {
        // Set Values
        ui.page.alpha = 0;
        ui.title.text = page.title;
        playerPosWhenOpened = transform.position;
        ui.text.text = page.text;
        modaleIsOpen = true;

        // Play Animation & Audio
        ui.page.DOFade(1, ui.fadeDuration).SetEase(Ease.InOutCirc);

        if(interactionAudio)
        interactionAudio.PlayAudio();
    }

    public void CloseModale()
    {
        // Set Values
        ui.page.alpha = 1;
        modaleIsOpen = false;

        // Play Animation & Audio
        ui.page.DOFade(0, ui.fadeDuration).SetEase(Ease.InOutCirc);

        if(interactionAudio)
        interactionAudio.PlayAudio();
    }
}
