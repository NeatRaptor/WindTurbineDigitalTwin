using TMPro;
using UnityEngine;

public class SpeedDropdownHandler : MonoBehaviour
{
    public TelemetryPlaybackManager playback;
    public TMP_Dropdown speedDropdown;

    // Speed values mapped to dropdown indices
    private readonly float[] speedValues = { 0.5f, 1f, 2f, 5f, 10f };

    void Start()
    {
        speedDropdown.onValueChanged.AddListener(OnSpeedChanged);

        // Default speed → 1x
        speedDropdown.value = 1;
        playback.playbackSpeed = 1f;
    }

    void OnSpeedChanged(int index)
    {
        if (index < 0 || index >= speedValues.Length) return;

        playback.playbackSpeed = speedValues[index];
        Debug.Log($"⏩ Playback speed set to {speedValues[index]}x");
    }
}
