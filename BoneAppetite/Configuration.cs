using BepInEx.Configuration;

namespace BoneAppetite;

public class Configuration
{
    public static ConfigEntry<bool> CookingSkillEnable;
    public static ConfigEntry<bool> BonusWhenCookingEnabled;
    public static ConfigEntry<bool> HatSEMessage;
    public static ConfigEntry<float> HatXpGain;
    public static Skills.SkillType rkCookingSkill;
}