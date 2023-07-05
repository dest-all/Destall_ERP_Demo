using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.ActionPoints;
using Business.Actions;
using Business.Services;
using DestallMaterials.WheelProtection.Extensions.Strings;
using DestallMaterials.CodeGeneration.ERP.ClientDependency;
using Protocol.Models.Filters;
using Protocol.Models.People;

namespace Business.OpenActions
{
    [AllowUnauthorised]
    [ProcessServerRequest]
    public partial class SimpleActions : ActionContainer
    {
        [ClientCaching(10, 10)]
        public async Task<ICollection<IEmployeeReadOnlyModel>> GetOneEmployee()
        {
            return new List<IEmployeeReadOnlyModel>
            {
                new EmployeeModel
                {
                    FirstName = "Igor",
                    LastName = "Zhurbenko",
                    Reference = new EmployeeReference
                    {
                        Id = 007,
                        Representation = "Igor Zhurbenko"
                    }
                }
            };
        }

        public async Task<ICollection<IEmployeeReadOnlyModel>> GetSeveralEmployees(System.UInt32 request)
        {
            return Enumerable.Range(1, 1 + (int)request).Select(i =>

                new EmployeeModel
                {
                    FirstName = "Employee",
                    LastName = $"# {i}",
                    Reference = new EmployeeReference
                    {
                        Id = (uint)i,
                        Representation = $"Employee #{i}"
                    }

                }).ToArray();
        }

        public async Task<IEnumerable<IEmployeeReadOnlyModel>> GetSorted(IEnumerable<IEmployeeReadOnlyModel> request)
        {
            return request.Reverse();
        }

        public async Task<bool> IsEvenCollection(ICollection<string> request)
        {
            return request.Count % 2 == 0;
        }

        public async Task<bool> ThrowException(string exceptionMessage)
        {
            throw new Exception(exceptionMessage);
        }

        //[DestructureParameterInClientInvocation]
        public async Task<IEmployeeReadOnlyModel> ReturnSameFilter(IEmployeeReadOnlyModel incomingFilter)
        {
            return incomingFilter;
        }

        public uint ReturnThreadId()
        {
            return (uint)System.Threading.Thread.CurrentThread.ManagedThreadId;
        }

        public IEnumerable<int> GroupIntegers(int firstNumber, int secondNumber)
            => new int[2] { firstNumber, secondNumber };

        public IEnumerable<IEmployeeReadOnlyModel> MultiplyEmployees(IEmployeeReadOnlyModel employee, int count)
            => Enumerable.Range(0, count).Select(i => employee).ToArray();

        [Permissions("CalculateEmployess")]
        public int CalculateEmployees(IEnumerable<IEmployeeReadOnlyModel> employees, int maxCount)
            => Math.Min(employees.Count(), maxCount);

        [ClientCaching(10, 10)]
        [ServerCaching(10, 10)]
        [Permissions("GetMaxValues", "GetMinValues")]
        public int MaxOfValues(IEmployeeReadOnlyModel firstEmployee, IList<IEmployeeReadOnlyModel> employees, int maxCount, int minCount, IEnumerable<IEmployeeReadOnlyModel> employeeReferences)
            => new int[] { firstEmployee.FirstName.Length, employees.Count, maxCount, minCount, employeeReferences.Count() }.Max();

    }
}