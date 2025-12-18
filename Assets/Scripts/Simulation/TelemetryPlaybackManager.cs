using System.Collections.Generic;
using UnityEngine;

public class TelemetryPlaybackManager : MonoBehaviour
{
    [Header("CSV Source")]
    public TextAsset telemetryCSV;

    [Header("Playback")]
    public float playbackSpeed = 1f;
    public bool isPlaying;

    public List<TelemetryData> data = new();
    public int CurrentIndex { get; private set; }

    private float tickTimer;

    void Start()
    {
        LoadCSV();
        SetIndex(0);
    }

    void Update()
    {
        if (!isPlaying || data.Count < 2) return;

        tickTimer += Time.deltaTime * playbackSpeed;

        // Advance every simulation tick
        if (tickTimer >= 0.25f)
        {
            tickTimer = 0f;
            SetIndex(CurrentIndex + 1);
        }
    }

    void LoadCSV()
    {
        if (!telemetryCSV)
        {
            Debug.LogError("❌ Telemetry CSV not assigned");
            return;
        }

        string[] lines = telemetryCSV.text.Split('\n');
        data.Clear();

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            if (string.IsNullOrEmpty(line)) continue;

            string[] c = line.Split(',');
            if (c.Length < 8) continue;

            data.Add(new TelemetryData
            {
                timestamp = c[0],
                wind_speed_ms = float.Parse(c[1]),
                rotor_speed_rpm = float.Parse(c[2]),
                power_output_kw = float.Parse(c[3]),
                gearbox_temperature_c = float.Parse(c[4]),
                generator_temperature_c = float.Parse(c[5]),
                health_index = float.Parse(c[6]),
                rul_hours = float.Parse(c[7])
            });
        }

        Debug.Log($"✅ Loaded {data.Count} telemetry rows");
    }

    // ===== PUBLIC CONTROL API =====

    public void Play() => isPlaying = true;
    public void Pause() => isPlaying = false;

    public void SetIndex(int index)
    {
        if (data.Count == 0) return;

        CurrentIndex = Mathf.Clamp(index, 0, data.Count - 1);

        if (CurrentIndex >= data.Count - 1)
            isPlaying = false;
    }

    public void SeekNormalized(float t)
    {
        SetIndex(Mathf.RoundToInt(t * (data.Count - 1)));
    }

    public TelemetryData GetCurrent()
    {
        if (data.Count == 0) return null;
        return data[CurrentIndex];
    }

    public float GetNormalizedTime()
    {
        if (data.Count <= 1) return 0f;
        return (float)CurrentIndex / (data.Count - 1);
    }
}
