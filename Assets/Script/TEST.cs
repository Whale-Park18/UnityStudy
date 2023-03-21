using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{
    public int loopCount = 100_000_000;
    public int minRandomValue;
    public int maxRandomValue;

    // Start is called before the first frame update
    void Start()
    {
        int[] randomValues = new int[maxRandomValue + 1];

        for(int i = 0; i < loopCount; i++)
        {
            int randomValue = Random.Range(minRandomValue, maxRandomValue);
            randomValues[randomValue]++;
        }

        for(int index = 0; index < randomValues.Length; index++)
        {
            print($"<color=green><b>[{index}]</b></color>: {randomValues[index]}");
        }
    }
}
