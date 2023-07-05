using DestallMaterials.WheelProtection.Extensions.Strings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Web.Application.Components.Authorisation
{
    public partial class SignInCard
    {
        class FormModel
        {
            public string UserName { get; set; }
            public string Password { get; set; }

            public bool IsValid => UserName!= null && Password != null;
            public bool CanBeSent => IsValid && UserName.HasContent() && Password.HasContent();
        }

        //class FormValidator : AbstractValidator<FormModel>
        //{
        //    public FormValidator()
        //    {
        //        RuleFor(m => m.UserName).NotEmpty();
        //        RuleFor(m => m.Password).NotEmpty();
        //    }
        //}
    }
}
