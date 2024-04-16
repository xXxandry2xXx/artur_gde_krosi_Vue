namespace artur_gde_krosi_Vue.Server.Models.UserModel
{
    public class UserInfoModel
    {
        public string name { get; set; }
        public string? surname { get; set; }
        public string? patronymic { get; set; }
        public bool sendingMail { get; set; } = true;
    }
}
