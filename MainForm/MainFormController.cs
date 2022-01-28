namespace RepairShop
{
    public class MainFormController
    {
        IMainForm MainFormView;
        public MainFormController(IMainForm view)
        {
            MainFormView = view;
        }
    }
}