using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Animator : MonoBehaviour
{
    private void OnAnimationEnd()
    {
        Destroy(transform.parent.gameObject);
    }
}
