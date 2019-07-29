using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryNet.Data
{
    [Table("users")]
    public class Customer
    {
        public Customer()
        {
            this.Role = "User";
            this.CreateDate = DateTime.Now;
            this.Status = 1;
        }

        public int ID { get; set; }

        [DisplayName("Логин")]
        [Required(ErrorMessage = "Введите логин")]
        public string Login { get; set; }

        [DisplayName("Пароль")]
        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("E-mail")]
        [Required(ErrorMessage = "Введите E-mail")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [DisplayName("Телефон")]
        [Required(ErrorMessage = "Введите телефон")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [DisplayName("Фамилия")]
        [Required(ErrorMessage = "Введите фамилию")]
        public string Family { get; set; }

        [DisplayName("Имя")]
        [Required(ErrorMessage = "Введите имя")]
        public string Name { get; set; }

        public string Role { get; set; }
        public int Status { get; set; }
        public DateTime? CreateDate { get; set; }       
        
    }
}
