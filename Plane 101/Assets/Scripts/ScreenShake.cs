using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public Animator camAnim;

    public void ShakeScreen()
    {
        camAnim.SetTrigger("Shake");
    }
}
