using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionManager : MonoBehaviour
{

    public static DestructionManager Instance;

    int TotalDestruction = 0;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Destruct(int amount)
    {
        TotalDestruction += amount;
        UiManager.Instance.UpdateDestructionScore(TotalDestruction.ToString());
    }
    
}
