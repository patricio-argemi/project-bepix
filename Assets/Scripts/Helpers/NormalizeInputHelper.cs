using UnityEngine;

/// <summary>
/// Normalize values helper class
/// </summary>
public static class NormalizeInputHelper
{
    /// <summary>
    /// Normalizes float value multiplying it by Time.DeltaTime
    /// </summary>
    /// <param name="inputValue">The input value</param>
    /// <returns>Value multiplied by Time.DeltaTime</returns>
    public static float NormalizeFloat(float inputValue)
    {
        return inputValue * Time.deltaTime;
    }

    /// <summary>
    /// Normalizes multiple float values multiplying them by Time.DeltaTime
    /// </summary>
    /// <param name="inputValue1">The first input value</param>
    /// <param name="inputValue2">The second input value</param>
    /// <returns>Values multiplied by Time.DeltaTime</returns>
    public static float NormalizeFloats(float inputValue1, float inputValue2)
    {
        return inputValue1 * inputValue2 * Time.deltaTime;
    }
}
