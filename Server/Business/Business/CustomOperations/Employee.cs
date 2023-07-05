using System.Collections.Generic;
using Business.ModelsComposition;
using System.Linq;
using System;
using System.Threading.Tasks;
using Business.Selectors;
using Data.Entities.DataHolders;
using RandomDataGenerator.Randomizers;
using DestallMaterials.CodeGeneration.ERP.ClientDependency;
using Business.Actions;
using Protocol.Models.People;
using Microsoft.EntityFrameworkCore;

namespace Business.CustomOperations
{
    [ProcessServerRequest]
    public partial class EmployeeCustomActions : ActionContainer
    {
        public async Task<IEnumerable<IEmployeeReadOnlyReference>> GetByName(string request)
        {
            var dbContext = await GetRepositoryAsync();

            var employees = dbContext.Employees.Where(e => e.FirstName.ToLower() == request.ToLower()).Select(e => e.ComposeReference());

            return employees;
        }

        public async Task<IEnumerable<IEmployeeReadOnlyReference>> GetSameArrayAsync(IList<IEmployeeReadOnlyReference> references)
        {
            return await Task.Run(() => references);
        }

        public async Task<IEnumerable<IEmployeeReadOnlyReference>> GetReferrables(string criteria)
        {
            var dbContext = await GetRepositoryAsync();
            var refSelector = EmployeeSelectors.ReferenceSelector;

            var subresult = dbContext.Employees.Select(refSelector);

            string pattern = $"%{criteria}%";

            var result = await subresult.Where(r => EF.Functions.Like(r.Representation, pattern)).ToArrayAsync();
            return result;
        }

        public async Task<bool> CreateManyEmployees(uint numberToCreate)
        {
            if (numberToCreate > 1000)
            {
                numberToCreate = 1000;
            }
            var rlastname = new RandomizerLastName(new RandomDataGenerator.FieldOptions.FieldOptionsLastName
            {
                UseNullValues = false,
                ValueAsString = true
            });
            var rfirstname = new RandomizerFirstName(new RandomDataGenerator.FieldOptions.FieldOptionsFirstName
            {
                UseNullValues = false,
                ValueAsString = true,
                Male = true,
                Female = true
            });
            var lastName = rlastname.Generate();
            var emps = Enumerable.Range(1, (int)numberToCreate)
                .Select(n => new Employee
                {
                    FirstName = rfirstname.Generate(),
                    LastName = rlastname.Generate(),
                }).ToList();

            using var repo = await GetRepositoryAsync();
            
            await repo.CreateAsync(emps);

            _business.Actions.Employee.GetPage.InvalidateCache();
            _business.Actions.Employee.GetReferences.InvalidateCache();

            return true;

        }

    }
}