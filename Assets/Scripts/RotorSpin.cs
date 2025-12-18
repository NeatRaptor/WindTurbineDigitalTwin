using UnityEngine;

public class RotorSpin : MonoBehaviour
{
    public TelemetryPlaybackManager playback;

    void Update()
    {
        var d = playback.GetCurrent();
        if (d == null) return;

        float degPerSec = d.rotor_speed_rpm * 6f;
        transform.Rotate(Vector3.down, degPerSec * Time.deltaTime);
    }
}
