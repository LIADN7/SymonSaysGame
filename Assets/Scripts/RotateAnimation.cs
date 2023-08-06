using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAnimation : MonoBehaviour
{


    private float rotationSpeed = 40f;
    private bool rotateLeft = true;
    private bool showAnim = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if (showAnim) { 
            if (transform.rotation.eulerAngles.z >= 15f && transform.rotation.eulerAngles.z <= 335f)
            {
                rotateLeft = !rotateLeft;
            }

            // Rotate the object left or right based on the current rotation direction
            float rotationAmount = rotateLeft ? -rotationSpeed : rotationSpeed;
            transform.Rotate(0f, 0f, rotationAmount * Time.deltaTime);
        }
    }


    public void setRotateAnim(bool flag)
    {
        //if(!flag) transform.Rotate(0f, 0f, 0f);
        GetComponent<SpriteRenderer>().color = Color.gray;
        GetComponent<AudioSource>().Play();
        showAnim =flag;
    }

    public void ResetRotationToZero()
    {
        GetComponent<AudioSource>().Stop();
        GetComponent<SpriteRenderer>().color = Color.white;
        transform.rotation = Quaternion.identity;
    }

    public void multSpeed(float s)
    {
        this.rotationSpeed *= s;
    }

}
