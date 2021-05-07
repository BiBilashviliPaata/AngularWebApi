using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Entities
{
    public class CompanyModel
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "სახელის ველი აუცილებლად შესავსებია !")]
        public string name { get; set; }
        [Required(ErrorMessage = "მისამართის ველი აუცილებლად შესავსებია !")]
        public string address { get; set; }
        [Required(ErrorMessage = "საქმიანობის ველი აუცილებლად შესავსებია !")]
        public string business { get; set; }

        public virtual ICollection<UserModel> Users { get; set; }

        public CompanyModel()
        {
            Users = new List<UserModel>();
        }
    }
}
