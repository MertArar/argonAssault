using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    
    [SerializeField] float controllSpeed = 10f; 
    [SerializeField] float xRange = 10f;
    [SerializeField] float yRange = 4f; 
    [SerializeField] float positionPitchFactor = -2f; 
    [SerializeField] float controllPitchFactor = -15f;
    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float controlRollFactor = -20f; 
    
    float xThrow, yThrow;
    void Update()
    {
       ProcessTranslation();
       ProcessRotation();
    }
    
    void ProcessRotation()
    {   
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controllPitchFactor;
        
        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
    
    void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");
        
        float xOffset = xThrow * Time.deltaTime * controllSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        
        float yOffset = yThrow * Time.deltaTime * controllSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);
        
        transform.localPosition = new Vector3 (clampedXPos, clampedYPos, transform.localPosition.z);
    }
    
}
