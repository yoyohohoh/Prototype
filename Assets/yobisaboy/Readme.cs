using UnityEngine;

[CreateAssetMenu(fileName = "Readme", menuName = "Scriptable Objects/Readme")]
public class Readme : ScriptableObject
{
    public Sprite icon;

    public string creator = "yobisaboy";
    public string description =
        "This code is original and owned by yobisaboy. " +
        "Use requires logo inclusion and credit in-game and on publishing platforms. " +
        "Redistribution or modification must include proper attribution.";
    public string contact = "yobisaboy@gmail.com";

}
