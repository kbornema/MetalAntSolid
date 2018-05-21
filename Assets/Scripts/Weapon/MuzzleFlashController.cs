using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlashController : MonoBehaviour
{

    BulletSpawnPoint bulletSpawnPoint;
    SpriteRenderer spriteRenderer;

    float currentMuzzleFlashTimer;

    FloatTimer timer;

    // Use this for initialization
    void Start()
    {
        bulletSpawnPoint = this.transform.parent.GetComponentInChildren<BulletSpawnPoint>();
        this.transform.position = bulletSpawnPoint.transform.position;
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        SetAlphaForSpriteRenderer(0f);
        timer = new FloatTimer(0f, false);
        currentMuzzleFlashTimer = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.IsReached == false)
        {
            timer.Update();
            SetAlphaForSpriteRenderer(timer.GetDecreasingValue() / currentMuzzleFlashTimer);
            if (timer.IsReached == true)
                SetAlphaForSpriteRenderer(0f);
        }
    }

    public void SetAlphaForSpriteRenderer(float alpha)
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
    }

    public void InitMuzzleFlash(float duration, Vector2 dir)
    {
        currentMuzzleFlashTimer = duration;
        if (timer.IsReached == false)
        {

        }
        SetAlphaForSpriteRenderer(1f);
        timer = new FloatTimer(duration, false);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}