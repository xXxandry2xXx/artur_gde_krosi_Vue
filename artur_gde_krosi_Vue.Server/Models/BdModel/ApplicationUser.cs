using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace artur_gde_krosi_Vue.Server.Models.BdModel
{
    [Table("AspNetUsers")] // Указываем имя таблицы в базе данных
    public class ApplicationUser : IdentityUser
    {
        public string name { get; set; }
        public string? surname { get; set; }
        public string? patronymic { get; set; }
        public bool sendingMail{ get; set; } = true;
    }
}
