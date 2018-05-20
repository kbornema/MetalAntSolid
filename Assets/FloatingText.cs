using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour {
    [SerializeField]
    private Text _text;

    [SerializeField]
    private AnimationCurve _alphaCurve;
    [SerializeField]
    private AnimationCurve _scaleCurve;
    [SerializeField]
    private AnimationCurve _yValueCurve;

    [SerializeField]
    private float _lifeTime;
    private float _curLifeTime;

    private Vector2 _startPos;
    private Vector3 _startScale;

    private void Start()
    {
        _startPos = transform.position;
        _startScale = transform.localScale;
        Update();
    }

    public void SetLifeTime(float lifeTime)
    {
        this._lifeTime = lifeTime;
    }
    public void SetText(string text, Color color)
    {
        _text.color = color;
        _text.text = text;
    }

    public void ResetAll(Vector3 pos)
    {
        _startPos = pos;
        _startScale = Vector3.one;
        _curLifeTime = 0.0f;
    }

    private void Update()
    {
        _curLifeTime += Time.deltaTime;

        UpdateCurves(_curLifeTime / _lifeTime);

        if (_curLifeTime >= _lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void UpdateCurves(float t)
    {
        Color c = _text.color;
        c.a = _alphaCurve.Evaluate(t);
        _text.color = c;

        float yValue = _yValueCurve.Evaluate(t);
        transform.position = _startPos + new Vector2(0.0f, yValue);

        transform.localScale = _startScale * _scaleCurve.Evaluate(t);
    }

    public static FloatingText CreateFloatingText(string text, Vector2 pos, Transform root, Color color)
    {
        GameObject prefab = (GameObject)Resources.Load("FloatingText");
        Debug.Assert(prefab != null, "Path was not found - FloatingText Gameobject");

        if (prefab == null)
            return null;

        FloatingText instance = Instantiate(prefab).GetComponent<FloatingText>();

        if (root)
            instance.transform.SetParent(root);

        instance.transform.position = pos;
        instance.SetText(text, color);

        return instance;
    }
    public static FloatingText CreateFloatingText(string text, Vector2 pos, Transform root, Color color, float lifeTime)
    {

        GameObject prefab = (GameObject)Resources.Load("FloatingText");
        Debug.Assert(prefab != null, "Path was not found - FloatingText Gameobject");

        if (prefab == null)
            return null;

        FloatingText instance = Instantiate(prefab).GetComponent<FloatingText>();

        if (root)
            instance.transform.SetParent(root);

        instance.SetLifeTime(lifeTime);
        instance.transform.position = pos;
        instance.SetText(text, color);

        return instance;
    }
}
