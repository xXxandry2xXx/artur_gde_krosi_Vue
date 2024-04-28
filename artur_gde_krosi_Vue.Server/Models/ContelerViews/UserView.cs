namespace artur_gde_krosi_Vue.Server.Models.ContelerViews
{
    public class UserView
    {
        public UserView(string name, string surname, string patronymic, bool sendingMail, string email)
        {
            this.name = name;
            this.surname = surname;
            this.patronymic = patronymic;
            this.sendingMail = sendingMail;
            Email = email;
        }

        public string name { get; set; }
        public string surname { get; set; }
        public string patronymic { get; set; }
        public bool sendingMail { get; set; }
        public string Email { get; set; }
    }
}
