using System;

[Serializable]
public class TelemetryData
{
    public string timestamp;

    public float wind_speed_ms;
    public float rotor_speed_rpm;
    public float power_output_kw;
    public float gearbox_temperature_c;
    public float generator_temperature_c;
    public float health_index;
    public float rul_hours;

    public float healthPercent => health_index * 100f;
}
