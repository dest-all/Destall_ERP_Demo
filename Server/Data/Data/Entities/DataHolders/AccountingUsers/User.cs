using Data.Entities.DataHolders.Persons;
using Data.Extensions;
using DestallMaterials.CodeGeneration.ERP.ClientDependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.DataHolders.AccountingUsers
{
    public partial class User : DataHolder
    {
        public Permissions Permissions { get; set; }
        
        [ExcludeFromModels]
        public string PasswordHash { get; set; }
        public string LoginName { get; set; }

        [ExcludeFromModels]
        public bool IsAdminUser { get; set; }
        public override string GetRepresentation()
        {
            return LoginName;
        }

        public override void BeforeSave()
        {
            base.BeforeSave();
            PasswordHash = PasswordHash ?? "".Hash();
        }
    }
} 