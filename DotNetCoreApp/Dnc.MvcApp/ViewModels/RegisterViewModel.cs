using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.MvcApp.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage ="你输入的电子邮件格式错误。")]
        [Display(Name = "电子邮件")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="真实姓名不能为空。")]
        [Display(Name = "真实姓名")]
        public string FullName { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "{0} 最少 {2} 个字符，最多 {1} 个字符", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "验证密码")]
        [Compare("Password", ErrorMessage = "验证密码不匹配。")]
        public string ConfirmPassword { get; set; }
    }
}
