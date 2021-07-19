using System;

namespace Car.Utilities
{
    internal static class UpdateUtility
    {
        private static Action gameUpdate;

        public static void GameUpdate()
        {
            gameUpdate?.Invoke();
        }

        public static void AddGameUpdate(Action handler)
        {
            gameUpdate += handler;
        }

        public static void RemoveGameUpdate(Action handler)
        {
            gameUpdate -= handler;
        }
    }
}
