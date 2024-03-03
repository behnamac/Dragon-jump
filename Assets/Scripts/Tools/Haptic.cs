using MoreMountains.NiceVibrations;

namespace Tools
{
    public static class Haptic
    {
        /// <summary>
        ///     Level tamamlandığında çalıştırılır
        /// </summary>
        public static void HapticLevelComplete() => MMVibrationManager.Haptic(HapticTypes.HeavyImpact);

        /// <summary>
        ///     Level fail olduğunda çalıştırılır
        /// </summary>
        public static void HapticLevelFail() => MMVibrationManager.Haptic(HapticTypes.Warning);

        /// <summary>
        ///     Level içerisindeki herhangi bir para toplandığında çalıştırılır
        /// </summary>
        public static void HapticCollectCurrency() => MMVibrationManager.Haptic(HapticTypes.SoftImpact);

        /// <summary>
        ///     Level içerisinde bir rakibe / düşmana çarpıldığında yada bize ateş ettiklerinde isabet alınır ise çalıştırılır
        /// </summary>
        public static void HapticEnemyCollide() => MMVibrationManager.Haptic(HapticTypes.MediumImpact);

        /// <summary>
        ///     Oyuncu ateş ettiğinde çalıştırılır
        /// </summary>
        public static void HapticPlayerShot() => MMVibrationManager.Haptic(HapticTypes.RigidImpact);

        /// <summary>
        ///     Oyuncu bir powerup aldığında çalıştırılır
        /// </summary>
        public static void HapticPlayerCollectBuff() => MMVibrationManager.Haptic(HapticTypes.Selection);
    }
}