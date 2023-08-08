using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkEffect : MonoBehaviour
{
    private SpriteRenderer myRenderer;
    private Color originalColor;
    private int blinkingType=0;



    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        originalColor = myRenderer.color;
    }

    public void StartBlinkEffect()
    {
        StartCoroutine(BlinkCoroutine());
    }

    private IEnumerator BlinkCoroutine()
    {

            if (blinkingType >= 0)
            {
                if (blinkingType == 0)
                {
                myRenderer.color = Color.grey;
                blinkingType = 1;

                }
                else if (blinkingType == 1)
                {
                    myRenderer.color = originalColor;
                    blinkingType = 0;
                }
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(BlinkCoroutine());
            }

    }
}
