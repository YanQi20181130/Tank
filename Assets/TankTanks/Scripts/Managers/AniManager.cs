using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AniType
{
    idle,
    rot,
    gun0,
    gun1
}
public class AniManager : DDOLSingleton<AniManager> {

    public void PlayAni(Animation ani, AniType type)
    {
        string aniname = GetAniName(type);

        ani[aniname].speed = 1;

        if(type==AniType.idle)
        {
            ani[aniname].wrapMode = WrapMode.PingPong;
            ani[aniname].speed = 1.5f;
        }
        else
        {
            ani[aniname].wrapMode = WrapMode.Once;
        }

        AudioManager.Instance.PlayAudio(aniname);
        ani.CrossFade(aniname);

        StartCoroutine(BackToIdle(ani));
    }

    private IEnumerator BackToIdle(Animation ani)
    {
        yield return new WaitForSeconds(1);

        ani.CrossFade("idle");
    }
    private string GetAniName(AniType type)
    {
        string _name = "";

        switch(type)
        {
            case AniType.idle:
                _name = "idle";
                break;

            case AniType.gun0:
                _name = "gun0";
                break;

            case AniType.gun1:
                _name = "gun1";
                break;

            case AniType.rot:
                _name = "rot";
                break;
        }
        return _name;

    }
}
