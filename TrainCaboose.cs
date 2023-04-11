using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCaboose : MonoBehaviour
{
    public TrainParts[] caboosePartsList;
    [SerializeField] public float speed;


    public void CheckAllCabooseParts()
    {
        foreach (TrainParts part in caboosePartsList)
        {
            if( part.durability == 0 )
            {
                part.needsChanging = true;
            }
        }
    }

    public void ShowPartsThatNeedsChanging()
    {
        foreach (TrainParts part in caboosePartsList)
        {
            if (part.needsChanging)
            {

            }
        }
    }
}
