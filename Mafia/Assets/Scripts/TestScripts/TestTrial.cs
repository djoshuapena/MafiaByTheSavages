using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTrial : MonoBehaviour {

    public TrialGraphicsController tgc;

    private List<string> paramlist = new List<string>(new string[] { "Dean" });//, "James" });

    public void Go()
    {
        tgc = GetComponent<TrialGraphicsController>();
        bool success1 = tgc.InitializeTrial(paramlist);
        //bool success2 = tgc.StartTrial();
        Debug.Log("init trial success?  " + success1);
        //Debug.Log("start trial success? " + success2);
    }
}
