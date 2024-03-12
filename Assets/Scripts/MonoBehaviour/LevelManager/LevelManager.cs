using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // ----------------------
    // Class
    // ----------------------

    [System.Serializable]
    public class LoadingCanvas
    {
        public CanvasGroup group;
        public float fadeDuration;
    }

    // ----------------------
    // Variables
    // ----------------------

    public static LevelManager instance;

    [Header("Level Manager")]
    [SerializeField] int minLoadingTime;
    [Space(10)]
    [SerializeField] LoadingCanvas whiteLoadingCanvas;
    [SerializeField] LoadingCanvas blackLoadingCanvas;

    // ----------------------
    // Functions
    // ----------------------
    
    void Start()
    {
        // Set Level Manager
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }

        else
        Destroy(instance);

        // Set Values
    }

    // Scene Management
    // ----------------------

    public async void LoadAsyncScene(string sceneName)
    {
        // Set Values
        var sceneLoad = SceneManager.LoadSceneAsync(sceneName);
        sceneLoad.allowSceneActivation = false;

        // Animate Loading
        whiteLoadingCanvas.group.alpha = 0;
        whiteLoadingCanvas.group.DOFade(1, whiteLoadingCanvas.fadeDuration).SetEase(Ease.InOutCirc);

        do await Task.Delay(minLoadingTime);  
        while (sceneLoad.progress < 0.9f);

        // Animate Loading
        whiteLoadingCanvas.group.alpha = 1;
        whiteLoadingCanvas.group.DOFade(0, whiteLoadingCanvas.fadeDuration).SetEase(Ease.InOutCirc);

        sceneLoad.allowSceneActivation = true;
    }

    IEnumerator QuitGame()
    {
        // Fade In Black Screen
        blackLoadingCanvas.group.alpha = 0;
        blackLoadingCanvas.group.DOFade(1, blackLoadingCanvas.fadeDuration).SetEase(Ease.InOutCirc);


        // Wait For ???
        yield return new WaitForSeconds(blackLoadingCanvas.fadeDuration);

        Application.Quit();
    }
}
