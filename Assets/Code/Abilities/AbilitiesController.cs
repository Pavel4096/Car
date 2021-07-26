using Car.Inventory;

namespace Car.Abilities
{
    public class AbilitiesController : ControllerBase
    {
        private IAbilityRepository abilitiesRepository;
        private IAbilityActivator abilityActivator;
        private IInventoryModel inventoryModel;
        private IAbilitiesView abilitiesView;

        public AbilitiesController(IAbilityRepository _abilitiesRepository, IAbilityActivator _abilityActivator, IInventoryModel _inventoryModel, IAbilitiesView _abilitiesView)
        {
            abilitiesRepository = _abilitiesRepository;
            abilityActivator = _abilityActivator;
            inventoryModel = _inventoryModel;
            abilitiesView = _abilitiesView;
            abilitiesView.Selected += AbilitySelected;
            abilitiesView.Display(inventoryModel.EquippedItems, abilitiesRepository);
        }

        protected override void OnDispose()
        {
            abilitiesView.Selected -= AbilitySelected;
            abilitiesView.Dispose();
        }

        private void AbilitySelected(IAbility ability)
        {
            ability.Apply(abilityActivator);
        }
    }
}
