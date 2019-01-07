using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BulletBase : MonoBehaviour {

    protected GameObject shell;

    protected Transform InstantiateBullet(Transform _FireTransform,int _shellID)
    {
        Debug.Log("_shellID = "+ _shellID);

        shell = Instantiate(Resources.Load("prefabs/Shell/Shell_" + _shellID), _FireTransform.position, _FireTransform.rotation) as GameObject;
        
        return shell.transform;
    }

    protected Vector3 FindTargetPos(Transform heroTran,float distance)
    {
        Debug.Log("Find Target Pos");
        Vector3 tarV = Vector3.zero;

        tarV = heroTran.TransformPoint(Vector3.forward * distance);
        RaycastHit hit;
        if(Physics.Raycast(tarV,Vector3.down,out hit))
        {
            tarV = hit.point;
        }
         
        return tarV;
    }

   protected Vector3[] path;
    protected virtual void LunchBullet(Transform _FireTransform, int _shellID,float lunchTime, Ease tweenEase, float distance,int pathNum,int bulletCount)
    {
        Debug.Log("Lunch bullet");

        path = new Vector3[pathNum];
        path[0] = _FireTransform.position;
        path[pathNum-1] = FindTargetPos(_FireTransform, distance);
        float AverageDistance = distance / pathNum;


        for (int i = 1; i < pathNum-1; i++)
        {
           Vector3 wayPointVec = FindTargetPos(_FireTransform, AverageDistance * i);
            path[i] = new Vector3(wayPointVec.x, wayPointVec.y + (i % 2 == 0 ? 5 : 6), wayPointVec.z); 
        }
    }

    protected virtual void OnComplete()
    {
        Destroy(shell.gameObject);
    }

}
