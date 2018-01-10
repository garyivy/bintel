namespace Bintel.Models.AdminViewModels
{
    public class UsersViewModel
    {
        public UserViewModel[] Users { get; set; }

        public string SuccessMessage { get; set; }

        public string ErrorMessage { get; set; }
    }
}
