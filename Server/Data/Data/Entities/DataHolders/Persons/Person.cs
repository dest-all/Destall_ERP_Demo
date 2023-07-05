using Data.Entities.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.DataHolders.Persons
{
    public abstract partial class Person : DataHolder
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public override string GetRepresentation() => $"{FirstName} {LastName}";
    }
}