using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BulletControl : BulletBase {

    public int m_ShellID;
    // Use this for initialization
    protected override void LunchBullet(Transform _FireTransform, int _shellID, float lunchTime,
        Ease tweenEase, float distance, int pathNum, int bulletCount)
    {
        // 在base方法中，配置path信息
        base.LunchBullet(_FireTransform, _shellID, lunchTime, tweenEase, distance, pathNum, bulletCount);

        for (int i = 0; i < bulletCount; i++)
        {
            TweenControl.Instance.MovePath(
                InstantiateBullet(_FireTransform, _shellID),
                path,
                PathType.CatmullRom,
                PathMode.Full3D,
                lunchTime,
                0.2f * i,
                1,
                LoopType.Restart,
                tweenEase,
                0.01f);

            //if(bulletCount>1)
            //{
            //    AniManager.Instance.PlayAni(aniobj, AniType.gun0);
            //}
            //else
            //{
            //    AniManager.Instance.PlayAni(aniobj, AniType.gun1);

            //}
        }


    }


}

