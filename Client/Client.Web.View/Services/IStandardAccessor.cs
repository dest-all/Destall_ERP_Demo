using Protocol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Web.View.Services
{
    public interface IReferencesGetter<TEntityReference>
        where TEntityReference : IReference
    {
        Task<IEnumerable<TEntityReference>> GetReferencesAsync(string stringFilter, int pageNumber);
    }
    public interface IStandardAccessor<TEntityModel>
        where TEntityModel : IModelBase
    {
        Task<IEnumerable<TEntityModel>> GetPageAsync(string stringFilter, int pageNumber);
        Task DeleteAsync(long id);
        Task<TEntityModel> UpdateAsync(TEntityModel entityModel);
        Task<TEntityModel> CreateAsync(TEntityModel entityModel);
        Task<TEntityModel> GetAsync(long id);
    }

    public class ReferencesGetter<T> : IReferencesGetter<T>
        where T : IReference
    {
        public Task<IEnumerable<T>> GetReferencesAsync(string stringFilter, int pageNumber) => GetReferences(stringFilter, pageNumber);
        
        readonly Func<string, int, Task<IEnumerable<T>>> GetReferences;

        public ReferencesGetter(Func<string, int, Task<IEnumerable<T>>> getReferences)
        {
            GetReferences = getReferences;
        }
    }

    public class StandardAccessor<T> : IStandardAccessor<T>
        where T : IModelBase
    {
        public Task<T> CreateAsync(T entityModel) => Create(entityModel);

        public Task DeleteAsync(long id) => Delete(id);

        public Task<T> GetAsync(long id) => Get(id);

        public Task<IEnumerable<T>> GetPageAsync(string stringFilter, int pageNumber) => GetPage(stringFilter, pageNumber);

        public Task<T> UpdateAsync(T entityModel) => Update(entityModel);

        readonly Func<T, Task<T>> Create;
        readonly Func<long, Task> Delete;
        readonly Func<long, Task<T>> Get;
        readonly Func<string, int, Task<IEnumerable<T>>> GetPage;
        readonly Func<T, Task<T>> Update;

        public StandardAccessor(Func<T, Task<T>> create, Func<long, Task> delete, Func<long, Task<T>> get, Func<string, int, Task<IEnumerable<T>>> getPage, Func<T, Task<T>> update)
        {
            Create = create;
            Delete = delete;
            Get = get;
            GetPage = getPage;
            Update = update;
        }
    }
}
