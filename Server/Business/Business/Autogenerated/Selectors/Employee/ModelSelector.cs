// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using System.Linq;
using System;
using System.Collections.Generic;
using Data.EntityFramework;
using Data.Entities.DataHolders;
using Protocol.Models.People;
using System.Linq.Expressions;

namespace Business.Selectors;
public static partial class EmployeeSelectors
{
    public static Expression<Func<Employee, EmployeeModel>> ModelSelector(Data.Repository.IDataRepository repo) => e1 => new EmployeeModel{FirstName = e1.FirstName, //
 LastName = e1.LastName, //
 // Referring properties.
    Reference = repo.Employees.Where(e2 => e2 == e1).Select(EmployeeSelectors.ReferenceSelector).FirstOrDefault(), //
    };
}