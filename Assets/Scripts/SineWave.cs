using UnityEngine;
using System.Collections;

public class SineWave : MonoBehaviour
{
    private int dimension;

    private void TotalIndexs()
    {
        for (int x = 0; x <= dimension; x++)
        {
            for (int z = 0; z <= dimension; z++)
            {
                Debug.Log("I: " + Index(x, z));
            }
        }
    }

    private int Index(int x, int z)
    {
        return x * (dimension + 1) + z;
    }
}
