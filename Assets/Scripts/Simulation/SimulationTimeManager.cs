using System;
using UnityEngine;

public class SimulationTimeManager : MonoBehaviour
{
    public DateTime startTime;
    public DateTime endTime;
    public DateTime currentTime;

    public bool isPlaying = false;
    public float speedMultiplier = 1f;

    public float NormalizedTime =>
        (float)(currentTime - startTime).TotalSeconds /
        (float)(endTime - startTime).TotalSeconds;

    void Update()
    {
        if (!isPlaying) return;

        currentTime = currentTime.AddSeconds(Time.deltaTime * speedMultiplier);

        if (currentTime >= endTime)
        {
            currentTime = endTime;
            isPlaying = false;
        }
    }

    public void Play() => isPlaying = true;
    public void Pause() => isPlaying = false;

    public void SetNormalizedTime(float t)
    {
        t = Mathf.Clamp01(t);
        currentTime = startTime.AddSeconds(
            (endTime - startTime).TotalSeconds * t
        );
    }
}
