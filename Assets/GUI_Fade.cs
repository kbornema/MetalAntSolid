using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GUI_Fade : MonoBehaviour {

    [SerializeField]
    private Image _image;
    [SerializeField]
    private float _speed = 2;
    [SerializeField]
    private string _sceneName = "";
    public void OnClicked(bool fadeaway, string sceneName)
    {
        _image.gameObject.SetActive(true);
        _sceneName = sceneName;
        StartCoroutine(Fade(fadeaway));
    }
    public void OnClicked(bool fadeaway)
    {
        _image.gameObject.SetActive(true);
        StartCoroutine(Fade(fadeaway));
    }

    IEnumerator Fade(bool fadeAway)
    {
        if (fadeAway)
        {
            for (float i = 1; i >= 0; i -= Time.deltaTime * _speed)
            {
                _image.color = new Color(1, 1, 1, i);
                yield return null;
            }
            if (_sceneName != "")
                SceneManager.LoadScene(_sceneName);
        }
        else
        {
            for (float i = 0; i <= 1; i += Time.deltaTime * _speed)
            {
                // set color with i as alpha
                _image.color = new Color(1, 1, 1, i);
                yield return null;
            }
            if (_sceneName != "")
                SceneManager.LoadScene(_sceneName);
        }
    }
}
