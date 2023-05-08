using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class QuadTextureDisplayer : MonoBehaviour
{
    public Texture2D[] textures;
    public GameObject quadPrefab;
    public Transform targetTransform;
    private GameObject currentQuad;
    private int currentIndex = 0;

    private List<InputDevice> devices = new List<InputDevice>();
    private bool leftPrimaryButtonPressed = false;
    private bool rightPrimaryButtonPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        DisplayTextureOnQuad(textures[currentIndex]);
    }

    // Update is called once per frame
    void Update()
    {
        devices.Clear();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Controller, devices);

        foreach (var device in devices)
        {
            CheckPrimaryButtonPress(device);
        }
    }

    void CheckPrimaryButtonPress(InputDevice device)
    {
        if (device.isValid)
        {
            bool primaryButtonState;
            if (device.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButtonState))
            {
                if (device.characteristics.HasFlag(InputDeviceCharacteristics.Left))
                {
                    if (primaryButtonState && !leftPrimaryButtonPressed)
                    {
                        leftPrimaryButtonPressed = true;
                    }
                    else if (!primaryButtonState && leftPrimaryButtonPressed)
                    {
                        leftPrimaryButtonPressed = false;
                        CycleTextures(-1); // Go to the previous page
                    }
                }
                else if (device.characteristics.HasFlag(InputDeviceCharacteristics.Right))
                {
                    if (primaryButtonState && !rightPrimaryButtonPressed)
                    {
                        rightPrimaryButtonPressed = true;
                    }
                    else if (!primaryButtonState && rightPrimaryButtonPressed)
                    {
                        rightPrimaryButtonPressed = false;
                        CycleTextures(1); // Go to the next page
                    }
                }
            }
        }
    }

    void CycleTextures(int direction)
    {
        // Destroy the current quad
        if (currentQuad != null)
        {
            Destroy(currentQuad);
        }

        // Move to the next or previous texture index depending on the direction
        currentIndex = (currentIndex + direction) % textures.Length;
        if (currentIndex < 0)
        {
            currentIndex = textures.Length - 1;
        }

        // Display the new texture
        DisplayTextureOnQuad(textures[currentIndex]);
    }

    void DisplayTextureOnQuad(Texture2D texture)
    {
        currentQuad = Instantiate(quadPrefab, targetTransform.position, targetTransform.rotation);
        currentQuad.GetComponent<Renderer>().material.mainTexture = texture;
    }
}
