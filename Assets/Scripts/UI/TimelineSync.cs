using UnityEngine;
using UnityEngine.UI;

public class TimelineSync : MonoBehaviour
{
    public TelemetryPlaybackManager playback;
    public Slider slider;

    private bool userDragging = false;

    void Start()
    {
        slider.minValue = 0f;
        slider.maxValue = 1f;

        slider.onValueChanged.AddListener(OnSliderChanged);
    }

    void Update()
    {
        // Only auto-sync slider when:
        // 1) simulation is playing
        // 2) user is NOT dragging
        if (playback.isPlaying && !userDragging)
        {
            slider.SetValueWithoutNotify(playback.GetNormalizedTime());
        }
    }

    public void OnSliderChanged(float value)
    {
        // Allow manual seeking ONLY when paused OR dragging
        if (!playback.isPlaying || userDragging)
        {
            playback.SeekNormalized(value);
        }
    }

    // ===== Hook these from EventTrigger =====
    public void OnBeginDrag()
    {
        userDragging = true;
        playback.Pause();   // freeze simulation while scrubbing
    }

    public void OnEndDrag()
    {
        userDragging = false;
    }
}