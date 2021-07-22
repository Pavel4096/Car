using System;

namespace Car
{
    public interface IAds
    {
        void ShowAd();
        void ShowAd(Action finish_handler);
    }
}
