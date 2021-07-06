using ItemAPI;
using System.Collections.Generic;

namespace TurboItems
{
    public class Module : ETGModule
    {
        public static readonly string MOD_NAME = "TurboXGTSItems";
        public static readonly string VERSION = "2.1.0";
        public static readonly string TEXT_COLOR = "#00FFFF";

        public override void Start()
        {
            ItemBuilder.Init();
            DevilsHorns.Register();
            BulletSpeedShift.Register();
            YarnBall.Add();
            SelfHarmBeamWeaponBecauseNevernamedToldMeItWasOkayTo.Add();
            WoodStake.Add();
            HammerBro.Add();
            DefinitelyNotBrimstone.Add();
            //MirrorSword.Add();
            MirrorSwordMeleeOnly.Add();
            //MirrorSwordBeam.Add(); //will be unused until something fixes the weird beam ammo bug
            //MirrorSwordLaser.Add(); //unused 'til I figure out some stuffs
            //GargoyleHandLeft.Add();
            //GargoyleHandRight.Add();
            ClockworkAssaultRifle.Add();
            MasterSword.Add();
            PhrenicBow.Add();
            ChoiceBottle.Init();
            KoopaShell.Init();
            SamusHelmet.Register();
            HuntingKit.Register();
            MedicalBox.Register();
            BloodCoveredCloak.Register();
            AC15Pack.Register();
            TrankGunPack.Register();
            GunbowPack.Register();
            IceTray.Register();
            Yin.Add();
            Yang.Add();
            InitialiseSynergies.DoInitialisation();
            SynergyFormInitialiser.SynergyInitialiser();
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
