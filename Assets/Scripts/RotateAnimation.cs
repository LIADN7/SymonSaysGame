using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAnimation : MonoBehaviour
{


    private float rotationSpeed = 40f; // Base rotation speed in degrees per second
    private bool rotateLeft = true; // Flag to control rotation direction
    private bool showAnim = false; // Flag to determine whether the rotation animation is active



    void Update()
    {   
        if (showAnim) { 
            // If the object's rotation is between certain angles, change rotation direction
            if (transform.rotation.eulerAngles.z >= 15f && transform.rotation.eulerAngles.z <= 335f)
            {
                rotateLeft = !rotateLeft;
            }

            // Rotate the object left or right based on the current rotation direction
            float rotationAmount = rotateLeft ? -rotationSpeed : rotationSpeed;
            transform.Rotate(0f, 0f, rotationAmount * Time.deltaTime);
        }
    }

    // Activate or deactivate the rotation animation
    public void setRotateAnim(bool flag)
    {
        GetComponent<SpriteRenderer>().color = Color.gray; // Change the sprite color during animation
        GetComponent<AudioSource>().Play(); // Play an audio clip
        showAnim = flag;
    }

    // Reset the object's rotation to its initial state
    public void ResetRotationToZero()
    {
        GetComponent<AudioSource>().Stop(); // Stop the audio clip
        GetComponent<SpriteRenderer>().color = Color.white; // Reset the sprite color
        transform.rotation = Quaternion.identity; // Reset the rotation to zero
    }

    // Multiply the rotation speed by a given factor
    public void multSpeed(float s)
    {
        this.rotationSpeed *= s;
    }

}
