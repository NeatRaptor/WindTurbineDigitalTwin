using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TelemetryPlaybackManager playback;

    public TMP_Text timestampText;
    public TMP_Text rpmText;
    public TMP_Text windText;
    public TMP_Text powerText;
    public TMP_Text gearboxText;
    public TMP_Text generatorText;
    public TMP_Text healthText;
    public TMP_Text rulText;
    public TMP_Text alertText;

    void Update()
    {
        var d = playback.GetCurrent();
        if (d == null) return;

        timestampText.text = d.timestamp;
        rpmText.text = $"Rotor Speed: {d.rotor_speed_rpm:F0} rpm";
        windText.text = $"Wind Speed: {d.wind_speed_ms:F1} m/s";
        powerText.text = $"Power Generated: {d.power_output_kw:F0} kW";
        gearboxText.text = $"Gearbox Temperature: {d.gearbox_temperature_c:F1} °C";
        generatorText.text = $"Generator: {d.generator_temperature_c:F1} °C";
        healthText.text = $"Health Index: {d.healthPercent:F1}%";
        rulText.text = $"RUL: {d.rul_hours:F1} hrs";

        UpdateAlerts(d);
    }

    void UpdateAlerts(TelemetryData d)
    {
        if (d.health_index < 0.8f)
            alertText.text = "CRITICAL: Immediate Maintenance Required";
        else if (d.rul_hours < 314.15f)
            alertText.text = "WARNING: Degradation Detected";
        else
            alertText.text = "System Operating Normally";
    }
}
