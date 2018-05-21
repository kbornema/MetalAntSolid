using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEggKamera : MonoBehaviour {

    [SerializeField]
    private Camera _mainCamera;
    Vector3 _position;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Debug.Log("DRIN");
            _position = _mainCamera.transform.position;
            _mainCamera.transform.position = new Vector3(0, -20, -10);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Debug.Log("RAUS");
            _mainCamera.transform.position = _position;
        }
    }
}
