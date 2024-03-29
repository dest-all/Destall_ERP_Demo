// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using System.Collections.Generic;
using Data.Repository;
using Business.ModelsComposition;
using System.Linq;
using System;
using Business.Selectors;
using Protocol.Exceptions;
using System.Threading.Tasks;
using DestallMaterials.CodeGeneration.ERP.ClientDependency;
using Microsoft.EntityFrameworkCore;
using Business.StatusMovement;
using Business.Extensions;
using DestallMaterials.WheelProtection.Extensions.Tasks;
using DestallMaterials.WheelProtection.Extensions.Objects;
using Protocol.Models;

namespace Business.Actions;
public partial class StandardActions
{
    [DestallMaterials.CodeGeneration.ERP.ClientDependency.ProcessServerRequest]
    public partial class Withdrawal : Business.Actions.ActionContainer, IAutogeneratedType<Data.Entities.Documents.Finances.Withdrawal>
    {
        Business.ActionPoints.Actions.IWithdrawalActionPointAccessor _ownDispatchers => _business.Actions.Withdrawal;
        void InvalidateSavedItemsCache(IEnumerable<long> ids) => Task.Run(() =>
        {
            foreach (var id in ids)
            {
                _ownDispatchers.Get.InvalidateCache(id);
            }

            _ownDispatchers.GetPage.InvalidateCache();
            _ownDispatchers.GetReferences.InvalidateCache();
            _ownDispatchers.Count.InvalidateCache();
        }).Forget();
        void InvalidateSavedItemCache(long id) => InvalidateSavedItemsCache(id.Yield());
        [Permissions("Withdrawal_Edit")]
        public async Task<Protocol.Models.FinancialDocuments.IWithdrawalReadOnlyModel> Save(Protocol.Models.FinancialDocuments.IWithdrawalReadOnlyModel model)
        {
            using var repo = await GetRepositoryAsync();
            Protocol.Models.FinancialDocuments.IWithdrawalReadOnlyModel result;
            var modelSelector = WithdrawalSelectors.ModelSelector(repo);
            if (model.Reference.IsEmpty())
            {
                var newEntity = model.ToEntity();
                long id = await repo.CreateAsync(newEntity);
                result = await repo.Withdrawals.Where(e => e.Id == id).Select(modelSelector).FirstOrDefaultAsync();
            }
            else
            {
                var existingEntity = await repo.Withdrawals.FirstOrDefaultAsync(e => model.Reference.Id == e.Id);
                if (existingEntity.Status > 0)
                {
                    throw new InvalidStateException(Common.Phrases.Entities.Documents_can_be_modified_only_in_draft_state_);
                }

                if (existingEntity is null)
                {
                    throw new InvalidArgumentHandledException(string.Format(Common.Phrases.Entities.Withdrawal_with_ID__0__was_not_found_, model.Reference.Id));
                }

                model.Fill(existingEntity);
                await repo.UpdateAsync(existingEntity);
                var subresult = await repo.Withdrawals.Where(e => e.Id == model.Reference.Id).Select(modelSelector).FirstOrDefaultAsync();
                result = subresult;
            }

            InvalidateSavedItemCache(result.Reference.Id);
            return result;
        }

        [DestructureParameterInClientInvocation]
        [ClientCaching(15, 20)]
        [ServerCaching(60, 30)]
        [Permissions("Withdrawal_Read")]
        public async Task<IEnumerable<Protocol.Models.FinancialDocuments.IWithdrawalReadOnlyModel>> GetPage(int pageNumber, int pageSize, Protocol.Models.Filters.IWithdrawalFilterReadOnlyModel filter, IEnumerable<string> orderings)
        {
            if (pageSize == 0 || pageNumber == 0)
            {
                throw new InvalidArgumentHandledException(Common.Phrases.Entities.Limit_of_filtered_entries_and_pageNumber_parameter_must_not_be_0_);
            }

            using var repo = await GetRepositoryAsync();
            var modelSelector = WithdrawalSelectors.ModelSelector(repo);
            var filterExpression = repo.Express(filter);
            var subresult = repo.Withdrawals.Where(filterExpression).OrderBy(orderings).Select(modelSelector);
            int toSkip = pageSize * (pageNumber - 1);
            int toTake = pageSize;
            subresult = subresult.Skip(toSkip).Take(toTake);
            var result = await subresult.ToArrayAsync();
            return result;
        }

        [DestructureParameterInClientInvocation]
        [ClientCaching(20, 15)]
        [ServerCaching(60, 30)]
        public async Task<IEnumerable<Protocol.Models.FinancialDocuments.IWithdrawalReadOnlyReference>> GetReferences(int pageNumber, int pageSize, Protocol.Models.Filters.IWithdrawalFilterReadOnlyModel filter, IEnumerable<string> orderings)
        {
            if (pageSize <= 0 || pageNumber <= 0)
            {
                throw new InvalidArgumentHandledException(Common.Phrases.Entities.Limit_of_filtered_entries_and_pageNumber_parameter_must_not_be_0_);
            }

            using var repo = await GetRepositoryAsync();
            var selector = WithdrawalSelectors.ReferenceSelector;
            var filterExpression = repo.Express(filter);
            var subresult = repo.Withdrawals.Where(filterExpression).Select(selector);
            int toSkip = pageSize * (pageNumber - 1);
            int toTake = pageSize;
            subresult = subresult.Skip(toSkip).Take(toTake);
            var result = await subresult.ToArrayAsync();
            return result;
        }

        [ClientCaching(10, 10)]
        [ServerCaching(60, 30)]
        public async Task<uint> Count(Protocol.Models.Filters.IWithdrawalFilterReadOnlyModel filter)
        {
            using var repo = await GetRepositoryAsync();
            var filterExpression = repo.Express(filter);
            var result = await repo.Withdrawals.CountAsync(filterExpression);
            return (uint)result;
        }

        [ClientCaching(10, 10)]
        [ServerCaching(60, 30)]
        [Permissions("Withdrawal_Read")]
        public async Task<Protocol.Models.FinancialDocuments.IWithdrawalReadOnlyModel> Get(long id)
        {
            using var repo = await GetRepositoryAsync();
            var modelSelector = WithdrawalSelectors.ModelSelector(repo);
            var result = await repo.Withdrawals.Where(i => i.Id == id).Select(modelSelector).FirstOrDefaultAsync();
            return result;
        }

        [Permissions("Withdrawal_Delete")]
        public async Task<bool> Delete(IEnumerable<long> ids)
        {
            using var repo = await GetRepositoryAsync();
            var itemsToDelete = await repo.Withdrawals.Where(i => ids.Contains(i.Id)).ToArrayAsync();
            if (itemsToDelete.Any(i => i.Status > 0))
            {
                throw new InvalidStateException(Common.Phrases.Entities.Documents_can_be_deleted_only_in_draft_state_);
            }

            await repo.DeleteAsync(itemsToDelete);
            InvalidateSavedItemsCache(ids);
            return true;
        }

        [NotOpen]
        public async Task<bool> CanChangeStatusAsync(Protocol.Models.FinancialDocuments.IWithdrawalReadOnlyModel item, ushort targetStatus)
        {
            var permissions = await _business.GetCurrentUserPermissionsAsync();
            var currentStatus = item.Status;
            bool moveStatusUp = currentStatus < targetStatus;
            var requiredPermission = moveStatusUp ? targetStatus switch
            {
                1 => permissions.Withdrawal_ChangeStatus_MoveUp_ToSubmitted,
                2 => permissions.Withdrawal_ChangeStatus_MoveUp_ToReady,
                3 => permissions.Withdrawal_ChangeStatus_MoveUp_ToExecuted,
                4 => permissions.Withdrawal_ChangeStatus_MoveUp_ToClosed,
                _ => throw new ArgumentException(Common.Phrases.Entities.Incorrect_target_status_)} : targetStatus switch
            {
                0 => permissions.Withdrawal_ChangeStatus_MoveDown_ToDraft,
                1 => permissions.Withdrawal_ChangeStatus_MoveDown_ToSubmitted,
                2 => permissions.Withdrawal_ChangeStatus_MoveDown_ToReady,
                3 => permissions.Withdrawal_ChangeStatus_MoveDown_ToExecuted,
                _ => throw new ArgumentException(Common.Phrases.Entities.Incorrect_target_status_)};
            return requiredPermission;
        }

        [NotOpen]
        public void EnsureValidTargetStatus(ushort currentStatus, ushort targetStatus)
        {
            if (currentStatus == targetStatus)
            {
                throw new InvalidArgumentHandledException(Common.Phrases.Entities.Specified_status_to_move_to_is_equal_to_the_current_);
            }

            if (Math.Abs(currentStatus - targetStatus) != 1)
            {
                throw new InvalidArgumentHandledException(Common.Phrases.Entities.Can_t_move_status_more_than_1_stage_at_a_call_);
            }

            if (targetStatus > 4 || targetStatus < 0)
            {
                throw new InvalidArgumentHandledException(Common.Phrases.Entities.Invalid_target_status_value_);
            }
        }

        public async Task<Protocol.Models.FinancialDocuments.IWithdrawalReadOnlyModel> ChangeStatus(Protocol.Models.FinancialDocuments.IWithdrawalReadOnlyModel item, ushort targetStatus)
        {
            var currentStatus = item.Status;
            EnsureValidTargetStatus(currentStatus, targetStatus);
            bool hasPermissionToMove = await CanChangeStatusAsync(item, targetStatus);
            if (!hasPermissionToMove)
            {
                throw new PermissionLackException(Common.Phrases.Entities.You_do_not_have_the_necessary_permission_to_move_Withdrawal_status_to_the_target_level_);
            }

            using var statusMover = CreateStatusMover();
            bool moveStatusUp = currentStatus < targetStatus;
            var result = moveStatusUp ? targetStatus switch
            {
                1 => await statusMover.MoveUpToSubmittedAsync(item),
                2 => await statusMover.MoveUpToReadyAsync(item),
                3 => await statusMover.MoveUpToExecutedAsync(item),
                4 => await statusMover.MoveUpToClosedAsync(item),
                _ => throw new ArgumentException(Common.Phrases.Entities.Incorrect_target_status_)} : targetStatus switch
            {
                0 => await statusMover.MoveDownToDraftAsync(item),
                1 => await statusMover.MoveDownToSubmittedAsync(item),
                2 => await statusMover.MoveDownToReadyAsync(item),
                3 => await statusMover.MoveDownToExecutedAsync(item),
                _ => throw new ArgumentException(Common.Phrases.Entities.Incorrect_target_status_)};
            InvalidateSavedItemCache(result.Reference.Id);
            return result;
        }

        WithdrawalStatusMover CreateStatusMover()
        {
            var result = new WithdrawalStatusMover(_business, Factories);
            return result;
        }
    }
}