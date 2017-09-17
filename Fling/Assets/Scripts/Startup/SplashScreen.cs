using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{

    public Texture2D fadeOutTexture;
    public float fadeSpeed = 0.8f;

    private int drawDepth = -1000;
    private float alpha = 1.0f;
    private int fadeDir = -1;

    bool startFadeOut = false;

    private void OnGUI()
    {
        alpha += fadeDir * fadeSpeed * Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);
    }

    public float BeginFade(int direction)
    {
        fadeDir = direction;
        return (fadeSpeed);
    }

    /*void OnLevelWasLoaded(int level)
    {
        alpha = 1;
        BeginFade(-1);
    }*/

    void OnSceneLoaded()
    {
        alpha = 1;
        BeginFade(-1);
    }

    private void Update()
    {

        int nbTouches = Input.touchCount;

        //Do something if there is at least 1 touch detected
        if (nbTouches > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                TouchEnd(Input.GetTouch(0).position);
            }
        }

        if (Time.time >= 2 && !startFadeOut)
        {
            startFadeOut = true;
            StartCoroutine(FadeOut());
        }
        if(startFadeOut && alpha == 1)
        {
            SceneManager.LoadScene("StartMenu");
            //SceneManager.LoadScene("Game_Scene");
        }
    }

    IEnumerator FadeOut()
    {
        alpha = -1;
        yield return new WaitForSeconds(1f);
        BeginFade(1);
    }

    void TouchEnd(Vector2 pos)
    {
        SceneManager.LoadScene("StartMenu");
        //SceneManager.LoadScene("Game_Scene");
    }

}