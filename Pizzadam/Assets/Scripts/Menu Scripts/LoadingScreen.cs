using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public Image loadingImage;              // UI Image to update
    public Sprite[] loadingSprites;         // Array of 7 sprites
    public string nextSceneName = "Driver Scene";

    private void Start()
    {
        StartCoroutine(PlayLoadingAnimation());
    }

    IEnumerator PlayLoadingAnimation()
    {
        float totalDuration = 4f;
        int frameCount = loadingSprites.Length;
        float frameDuration = totalDuration / frameCount;

        for (int i = 0; i < frameCount; i++)
        {
            loadingImage.sprite = loadingSprites[i];
            yield return new WaitForSeconds(frameDuration);
        }

        SceneManager.LoadScene(nextSceneName);
    }
}
