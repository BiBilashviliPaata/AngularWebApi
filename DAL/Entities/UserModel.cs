using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Entities
{
    public class UserModel
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "სახელის ველი აუცილებლად შესავსებია !")]

        public string name { get; set; }
        [Required(ErrorMessage = "გვარის ველი აუცილებლად შესავსებია !")]
        public string surname { get; set; }
        [Required]
        public string sex { get; set; }

        [Required(ErrorMessage = "პირადი ნომრის ველი აუცილებლად შესავსებია !")]
        [Range(1, int.MaxValue)]
        [MaxLength(11, ErrorMessage = "მაქსიმალური სიმბოლოების რაოდენობა შეადგენს '11' ს")]
        public string personalid { get; set; }
        [Required]
        [Column(TypeName = "Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime date { get; set; }
        [Required(ErrorMessage = "ქალაქის ველი აუცილებლად შესავსებია !")]
        public string city { get; set; }

        [Required(ErrorMessage = "მობილური ტელეფონის ველი აუცილებლად შესავსებია !")]
        [Range(1, int.MaxValue)]
        public string phonenumber { get; set; }

        public virtual ICollection<CompanyModel> Company { get; set; }

        public UserModel()
        {
            Company = new List<CompanyModel>();
        }

    }
}
