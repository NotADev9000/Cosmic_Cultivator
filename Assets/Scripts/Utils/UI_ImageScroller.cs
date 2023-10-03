using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ImageScroller : MonoBehaviour
{
    private RawImage img;
    [SerializeField] private float xSpeed, ySpeed;

    private void Awake()
    {
        img = GetComponent<RawImage>();
    }

    private void Update()
    {
        float x = img.uvRect.x + GetDistanceScrolled(xSpeed);
        float y = img.uvRect.y + GetDistanceScrolled(ySpeed);
        img.uvRect = new Rect(x, y, img.uvRect.width, img.uvRect.height);
    }

    private float GetDistanceScrolled(float speed)
    {
        return speed * Time.deltaTime;
    }
}
