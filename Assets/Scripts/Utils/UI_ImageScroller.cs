using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ImageScroller : MonoBehaviour
{
    [SerializeField] private float xSpeed, ySpeed;

    //------------------------------------------------------------------

    private RawImage img;

    //------------------------------------------------------------------

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

    //--------------------
    #region Scroll

    private float GetDistanceScrolled(float speed)
    {
        return speed * Time.deltaTime;
    }

    #endregion
    //--------------------

}
