using System.Collections.Generic;
using UnityEngine;
public class GlobalControl  {

    public static List<HeroData> heroDateList;
    public static List<ShellData> shellDateList;
    public static List<EnvData> envDateList;
    public static List<StoryData> storyDateList;

    public static List<HeroProperty> heroPropList;
    public static List<ShellProperty> shellPropList;

    public static int COIN;
    public static int STONE;

    /// <summary>
    /// 0 both can't move, 1 player can move, 2 enemy can move
    /// </summary>
    public static int PLAYERTURN;
}

public class GameProperty
{
    public static StoryType STORYTYPE;
    public static int ENVID;
    public static int HERO1_ID;
    public static int HERO2_ID;
    public static int TOURID;
}