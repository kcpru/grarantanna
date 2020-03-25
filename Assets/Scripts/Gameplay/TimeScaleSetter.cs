using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleSetter : MonoBehaviour
{

    [Header("Trampoline force")]
    public float speed = 0.5f;

    private float savedTimeScale;
    // Start is called before the first frame update
    void Start()
    {
        savedTimeScale = Time.timeScale;
        Time.timeScale = speed;
    }
    private void OnDestroy() {
        Time.timeScale = savedTimeScale;
    }
}
