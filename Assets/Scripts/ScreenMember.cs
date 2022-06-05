using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenMember : MonoBehaviour
{
    // Start is called before the first frame update
    public void OpenDigivolutionScreen()
    {
        LocationController.i.EnterLocation(null, true);
        //SetActiveMember();
    }
}
