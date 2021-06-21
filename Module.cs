using ItemAPI;
using System.Collections.Generic;

namespace TurboItems
{
    public class Module : ETGModule
    {
        public static readonly string MOD_NAME = "TurboXGTSItems";
        public static readonly string VERSION = "1.0.0";
        public static readonly string TEXT_COLOR = "#00FFFF";

        public override void Start()
        {
            ItemBuilder.Init();
            DevilsHorns.Register();
            BulletSpeedShift.Register();
            SamusHelmet.Register();
            HuntingKit.Register();
            MedicalBox.Register();
            WoodStake.Add();
            BloodCoveredCloak.Register();
            AC15Pack.Register();
            TrankGunPack.Register();
            GunbowPack.Register();
            ChoiceBottle.Init();
            YarnBall.Add();
            List<string> mandatoryConsoleIDs = new List<string>
                {
                "big_iron",
                "hip_holster"
                };
            CustomSynergies.Add("Texas Red", mandatoryConsoleIDs);

            Log($"{MOD_NAME} v{VERSION} started successfully.", TEXT_COLOR);
        }

        

        public static void Log(string text, string color="#FFFFFF")
        {
            ETGModConsole.Log($"<color={color}>{text}</color>");
        }

        public override void Exit() { }
        public override void Init() { }
    }
}
