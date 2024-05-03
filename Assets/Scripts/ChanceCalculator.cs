using UnityEngine;

public class ChanceCalculator : MonoBehaviour
{
    private int _minRange = 0;
    private int _maxRange = 100;

    public bool CalculateSuccessByPercentage(float percent)
    {
        int randomValue = Random.Range(_minRange, _maxRange + 1);
        return randomValue <= percent;
    }
}
