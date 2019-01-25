using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileControl : Singleton<TileControl> {

    public void ShowChild(Transform parent)
    {
        float count = parent.childCount;
        float randomF = Random.Range(0, count);

        if(randomF>=0 && randomF< count / 2)
        {
            parent.GetChild(0).gameObject.SetActive(true);
            return;
        }
        else
        {
            float leftCount = count / 2;
            float leftChildren = count - 1;
            float percent = leftCount / leftChildren;

            for (int i = 0; i < leftChildren; i++)
            {
                if(randomF>= leftCount+ percent*i && randomF< leftCount + percent * (i+1))
                {
                    Debug.Log(randomF+", "+ (leftCount + percent * i)+","+ (leftCount + percent * (i + 1)));
                    parent.GetChild(i+1).gameObject.SetActive(true);
                    break;
                }
            }
        }
 
    }


}
