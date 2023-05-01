using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Menu : MonoBehaviour
{
    public Button newGameButton;
    public Image fade;
    public float fadeSpeed;
    public AnimationCurve curve;
    public float fadeOpacity;

    void Awake()
    {
        newGameButton.onClick.AddListener(StartNewGame);

        FadeIn();
    }

    private void Update()
    {
        FadeIn();
    }

    void StartNewGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    IEnumerator FadeIn()
    {
        fadeOpacity = 0f;

        while (fadeOpacity < 1)
        {
            fadeOpacity += fadeSpeed * Time.deltaTime;
            float a = curve.Evaluate(fadeOpacity);
            fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, a);
            yield return 0;
        }
    }
}
