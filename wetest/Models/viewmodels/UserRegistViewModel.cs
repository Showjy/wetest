using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace wetest.Models.viewmodels
{
    public class UserRegistViewModel
    {
        [Required(ErrorMessage = "用户名不能为空。")]
        public string Name { get; set; }


        [Required(ErrorMessage = "密码不能为空。")]
        public string Password { get; set; }

        public int Level { get; set; }
    }
}
