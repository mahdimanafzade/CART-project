using UnityEngine;
using System.Collections;

[System.Serializable]
public class Supporter_Card_property
{
    public int id = 0;
    public float health_base = 100f;
    public float speed = 1;
    public int range = 2;

    public SpecialType specialType;
    
    public float health_improvement = 1;
    public int ap_increase = 3;

}

[System.Serializable]
public class Soldior_Card_property
{
    public int id = 0;
    public float health_base = 100f;
    public float speed = 1;
    public int range = 2;
    public int attack_point = 5;
    public int attack_ratio = 4;

}
