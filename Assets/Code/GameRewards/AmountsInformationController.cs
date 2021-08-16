namespace Car.Rewards
{
    public class AmountsInformationController : ControllerBase, IAmountsInformationController
    {
        private IAmountsInformationView _amountsInformationView;
        private IUserData _userData;

        public AmountsInformationController(IAmountsInformationView amountsInformationView, IUserData userData)
        {
            _amountsInformationView = amountsInformationView;
            _userData = userData;
            UpdateData();
        }

        public void UpdateData()
        {
            _amountsInformationView.OrangeCircles.text = _userData.OrangeCircles.ToString();
            _amountsInformationView.GreenCircles.text = _userData.GreenCircles.ToString();
        }
    }
}
