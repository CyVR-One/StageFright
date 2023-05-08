using UnityEngine;
using UnityEngine.UI;

public class DynamicAudioManager : MonoBehaviour
{
    public AudioSource backgroundAudioSource;
    public float quietVolume = 0.05f;
    public float normalVolume = 1.0f;
    public int microphoneSampleRate = 44100;
    public float sensitivity = 0.1f;
    public Slider micAmplitudeSlider;
    public float delayBeforeIncreasingVolume = 3.0f;

    private AudioClip microphoneInput;
    private string microphoneDevice;
    private bool isSpeaking;
    private float timeSinceLastSpoke = 0.0f;

    void Start()
    {
        // Get the default microphone device
        microphoneDevice = Microphone.devices[0];

        // Start recording from the microphone
        microphoneInput = Microphone.Start(microphoneDevice, true, 10, microphoneSampleRate);

    }

    void Update()
    {
        // Check if the user is speaking
        isSpeaking = IsUserSpeaking();

        // Update the slider value to display the microphone amplitude
        micAmplitudeSlider.value = GetMicrophoneAmplitude();

        // Update the time since the user last spoke
        timeSinceLastSpoke = isSpeaking ? 0.0f : timeSinceLastSpoke + Time.deltaTime;

        // Adjust the background audio volume based on whether the user is speaking or not and the specified delay
        if (isSpeaking)
        {
            backgroundAudioSource.volume = quietVolume;
        }
        else if (timeSinceLastSpoke >= delayBeforeIncreasingVolume / 2.0f && timeSinceLastSpoke < delayBeforeIncreasingVolume)
        {
            float t = (timeSinceLastSpoke - (delayBeforeIncreasingVolume / 2.0f)) / (delayBeforeIncreasingVolume / 2.0f);
            backgroundAudioSource.volume = Mathf.Lerp(quietVolume, normalVolume, t);
        }
        else if (timeSinceLastSpoke >= delayBeforeIncreasingVolume)
        {
            backgroundAudioSource.volume = normalVolume;
        }
    }


    float GetMicrophoneAmplitude()
    {
        int currentPos = Microphone.GetPosition(microphoneDevice);
        int sampleCount = 128;
        int startPos = Mathf.Max(currentPos - sampleCount, 0);

        float[] audioData = new float[sampleCount];
        float currentAmplitude = 0;

        if (Microphone.IsRecording(microphoneDevice))
        {
            microphoneInput.GetData(audioData, startPos);

            float sum = 0;
            for (int i = 0; i < audioData.Length; i++)
            {
                sum += Mathf.Abs(audioData[i]);
            }
            currentAmplitude = sum / audioData.Length;
        }

        return currentAmplitude;
    }


    bool IsUserSpeaking()
    {
        float currentAmplitude = GetMicrophoneAmplitude();

        // Compare the current amplitude to the sensitivity threshold
        return currentAmplitude > sensitivity;
    }

    void OnDestroy()
    {
        // Stop recording when the object is destroyed
        Microphone.End(microphoneDevice);
    }
}
