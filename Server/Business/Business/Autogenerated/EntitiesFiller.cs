// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using Protocol.Models;
using System.Linq;
using System;
using System.Collections.Generic;
using Data;
using Data.Repository;
using Common.Extensions.Number;
using DestallMaterials.WheelProtection.Extensions.Enumerables;

namespace Business.ModelsComposition;
public static class EntitiesFiller
{
    public static void Fill(this Protocol.Models.DataHolders.IUserReadOnlyModel from, Data.Entities.DataHolders.AccountingUsers.User to)
    {
        if (from == null)
        {
            throw new ArgumentException("Expected an IUserReadOnlyModel, but null was given.");
        }

        to.LoginName = from.LoginName;
        to.Id = from.Reference?.Id ?? default;
        to.Permissions = from.Permissions.ToEntity();
    }

    public static Data.Entities.DataHolders.AccountingUsers.User ToEntity(this Protocol.Models.DataHolders.IUserReadOnlyModel from)
    {
        var entity = new Data.Entities.DataHolders.AccountingUsers.User();
        from.Fill(entity);
        return entity;
    }

    public static List<Data.Entities.DataHolders.AccountingUsers.User> ToEntities(this IEnumerable<Protocol.Models.DataHolders.IUserReadOnlyReference> from, IDataRepository repo)
    {
        var ids = from.Select(r => r.Id).ToList();
        var entities = repo.Users.Where(i => ids.Contains(i.Id)).ToList();
        return entities;
    }

    public static void Fill(this Protocol.Models.Counterparties.ICustomerReadOnlyModel from, Data.Entities.DataHolders.Actors.Customer to)
    {
        if (from == null)
        {
            throw new ArgumentException("Expected an ICustomerReadOnlyModel, but null was given.");
        }

        to.Name = from.Name;
        to.Id = from.Reference?.Id ?? default;
    }

    public static Data.Entities.DataHolders.Actors.Customer ToEntity(this Protocol.Models.Counterparties.ICustomerReadOnlyModel from)
    {
        var entity = new Data.Entities.DataHolders.Actors.Customer();
        from.Fill(entity);
        return entity;
    }

    public static List<Data.Entities.DataHolders.Actors.Customer> ToEntities(this IEnumerable<Protocol.Models.Counterparties.ICustomerReadOnlyReference> from, IDataRepository repo)
    {
        var ids = from.Select(r => r.Id).ToList();
        var entities = repo.Customers.Where(i => ids.Contains(i.Id)).ToList();
        return entities;
    }

    public static void Fill(this Protocol.Models.Counterparties.ISupplierReadOnlyModel from, Data.Entities.DataHolders.Actors.Supplier to)
    {
        if (from == null)
        {
            throw new ArgumentException("Expected an ISupplierReadOnlyModel, but null was given.");
        }

        to.Name = from.Name;
        to.Id = from.Reference?.Id ?? default;
    }

    public static Data.Entities.DataHolders.Actors.Supplier ToEntity(this Protocol.Models.Counterparties.ISupplierReadOnlyModel from)
    {
        var entity = new Data.Entities.DataHolders.Actors.Supplier();
        from.Fill(entity);
        return entity;
    }

    public static List<Data.Entities.DataHolders.Actors.Supplier> ToEntities(this IEnumerable<Protocol.Models.Counterparties.ISupplierReadOnlyReference> from, IDataRepository repo)
    {
        var ids = from.Select(r => r.Id).ToList();
        var entities = repo.Suppliers.Where(i => ids.Contains(i.Id)).ToList();
        return entities;
    }

    public static void Fill(this Protocol.Models.ReferrableEntities.ICurrencyReadOnlyModel from, Data.Entities.Currency to)
    {
        if (from == null)
        {
            throw new ArgumentException("Expected an ICurrencyReadOnlyModel, but null was given.");
        }

        to.Name = from.Name;
        to.Primary = from.Primary;
        to.Id = from.Reference?.Id ?? default;
    }

    public static Data.Entities.Currency ToEntity(this Protocol.Models.ReferrableEntities.ICurrencyReadOnlyModel from)
    {
        var entity = new Data.Entities.Currency();
        from.Fill(entity);
        return entity;
    }

    public static List<Data.Entities.Currency> ToEntities(this IEnumerable<Protocol.Models.ReferrableEntities.ICurrencyReadOnlyReference> from, IDataRepository repo)
    {
        var ids = from.Select(r => r.Id).ToList();
        var entities = repo.Currencies.Where(i => ids.Contains(i.Id)).ToList();
        return entities;
    }

    public static void Fill(this Protocol.Models.DataHolders.IGoodReadOnlyModel from, Data.Entities.DataHolders.Good to)
    {
        if (from == null)
        {
            throw new ArgumentException("Expected an IGoodReadOnlyModel, but null was given.");
        }

        to.Name = from.Name;
        to.Id = from.Reference?.Id ?? default;
    }

    public static Data.Entities.DataHolders.Good ToEntity(this Protocol.Models.DataHolders.IGoodReadOnlyModel from)
    {
        var entity = new Data.Entities.DataHolders.Good();
        from.Fill(entity);
        return entity;
    }

    public static List<Data.Entities.DataHolders.Good> ToEntities(this IEnumerable<Protocol.Models.DataHolders.IGoodReadOnlyReference> from, IDataRepository repo)
    {
        var ids = from.Select(r => r.Id).ToList();
        var entities = repo.Goods.Where(i => ids.Contains(i.Id)).ToList();
        return entities;
    }

    public static void Fill(this Protocol.Models.People.IEmployeeReadOnlyModel from, Data.Entities.DataHolders.Employee to)
    {
        if (from == null)
        {
            throw new ArgumentException("Expected an IEmployeeReadOnlyModel, but null was given.");
        }

        to.FirstName = from.FirstName;
        to.LastName = from.LastName;
        to.Id = from.Reference?.Id ?? default;
    }

    public static Data.Entities.DataHolders.Employee ToEntity(this Protocol.Models.People.IEmployeeReadOnlyModel from)
    {
        var entity = new Data.Entities.DataHolders.Employee();
        from.Fill(entity);
        return entity;
    }

    public static List<Data.Entities.DataHolders.Employee> ToEntities(this IEnumerable<Protocol.Models.People.IEmployeeReadOnlyReference> from, IDataRepository repo)
    {
        var ids = from.Select(r => r.Id).ToList();
        var entities = repo.Employees.Where(i => ids.Contains(i.Id)).ToList();
        return entities;
    }

    public static void Fill(this Protocol.Models.FinancialDocuments.IDepositReadOnlyModel from, Data.Entities.Documents.Finances.Deposit to)
    {
        if (from == null)
        {
            throw new ArgumentException("Expected an IDepositReadOnlyModel, but null was given.");
        }

        to.Sum = from.Sum;
        to.Number = from.Number;
        to.Status = from.Status;
        to.Id = from.Reference?.Id ?? default;
        to.CurrencyId = from.Currency?.Id != 0 ? from.Currency?.Id : null;
        to.AccountableId = from.Accountable?.Id != 0 ? from.Accountable?.Id : null;
    }

    public static Data.Entities.Documents.Finances.Deposit ToEntity(this Protocol.Models.FinancialDocuments.IDepositReadOnlyModel from)
    {
        var entity = new Data.Entities.Documents.Finances.Deposit();
        from.Fill(entity);
        return entity;
    }

    public static List<Data.Entities.Documents.Finances.Deposit> ToEntities(this IEnumerable<Protocol.Models.FinancialDocuments.IDepositReadOnlyReference> from, IDataRepository repo)
    {
        var ids = from.Select(r => r.Id).ToList();
        var entities = repo.Deposits.Where(i => ids.Contains(i.Id)).ToList();
        return entities;
    }

    public static void Fill(this Protocol.Models.FinancialDocuments.IIncomingPaymentReadOnlyModel from, Data.Entities.Documents.Finances.IncomingPayment to)
    {
        if (from == null)
        {
            throw new ArgumentException("Expected an IIncomingPaymentReadOnlyModel, but null was given.");
        }

        to.Sum = from.Sum;
        to.Number = from.Number;
        to.Status = from.Status;
        to.Id = from.Reference?.Id ?? default;
        to.OrderId = from.Order?.Id != 0 ? from.Order?.Id : null;
        to.PayerId = from.Payer?.Id != 0 ? from.Payer?.Id : null;
        to.CurrencyId = from.Currency?.Id != 0 ? from.Currency?.Id : null;
        to.AccountableId = from.Accountable?.Id != 0 ? from.Accountable?.Id : null;
    }

    public static Data.Entities.Documents.Finances.IncomingPayment ToEntity(this Protocol.Models.FinancialDocuments.IIncomingPaymentReadOnlyModel from)
    {
        var entity = new Data.Entities.Documents.Finances.IncomingPayment();
        from.Fill(entity);
        return entity;
    }

    public static List<Data.Entities.Documents.Finances.IncomingPayment> ToEntities(this IEnumerable<Protocol.Models.FinancialDocuments.IIncomingPaymentReadOnlyReference> from, IDataRepository repo)
    {
        var ids = from.Select(r => r.Id).ToList();
        var entities = repo.IncomingPayments.Where(i => ids.Contains(i.Id)).ToList();
        return entities;
    }

    public static void Fill(this Protocol.Models.FinancialDocuments.IOutgoingPaymentReadOnlyModel from, Data.Entities.Documents.Finances.OutgoingPayment to)
    {
        if (from == null)
        {
            throw new ArgumentException("Expected an IOutgoingPaymentReadOnlyModel, but null was given.");
        }

        to.Sum = from.Sum;
        to.Number = from.Number;
        to.Status = from.Status;
        to.Id = from.Reference?.Id ?? default;
        to.OrderId = from.Order?.Id != 0 ? from.Order?.Id : null;
        to.ReceiverId = from.Receiver?.Id != 0 ? from.Receiver?.Id : null;
        to.CurrencyId = from.Currency?.Id != 0 ? from.Currency?.Id : null;
        to.AccountableId = from.Accountable?.Id != 0 ? from.Accountable?.Id : null;
    }

    public static Data.Entities.Documents.Finances.OutgoingPayment ToEntity(this Protocol.Models.FinancialDocuments.IOutgoingPaymentReadOnlyModel from)
    {
        var entity = new Data.Entities.Documents.Finances.OutgoingPayment();
        from.Fill(entity);
        return entity;
    }

    public static List<Data.Entities.Documents.Finances.OutgoingPayment> ToEntities(this IEnumerable<Protocol.Models.FinancialDocuments.IOutgoingPaymentReadOnlyReference> from, IDataRepository repo)
    {
        var ids = from.Select(r => r.Id).ToList();
        var entities = repo.OutgoingPayments.Where(i => ids.Contains(i.Id)).ToList();
        return entities;
    }

    public static void Fill(this Protocol.Models.FinancialDocuments.IStaffPaycheckReadOnlyModel from, Data.Entities.Documents.Finances.StaffPaycheck to)
    {
        if (from == null)
        {
            throw new ArgumentException("Expected an IStaffPaycheckReadOnlyModel, but null was given.");
        }

        to.CashPart = from.CashPart;
        to.BankTransferPart = from.BankTransferPart;
        to.Withheld = from.Withheld;
        to.PeriodStart = from.PeriodStart;
        to.PeriodEnd = from.PeriodEnd;
        to.Sum = from.Sum;
        to.Number = from.Number;
        to.Status = from.Status;
        to.Id = from.Reference?.Id ?? default;
        to.PaidToId = from.PaidTo?.Id != 0 ? from.PaidTo?.Id : null;
        to.CurrencyId = from.Currency?.Id != 0 ? from.Currency?.Id : null;
        to.AccountableId = from.Accountable?.Id != 0 ? from.Accountable?.Id : null;
    }

    public static Data.Entities.Documents.Finances.StaffPaycheck ToEntity(this Protocol.Models.FinancialDocuments.IStaffPaycheckReadOnlyModel from)
    {
        var entity = new Data.Entities.Documents.Finances.StaffPaycheck();
        from.Fill(entity);
        return entity;
    }

    public static List<Data.Entities.Documents.Finances.StaffPaycheck> ToEntities(this IEnumerable<Protocol.Models.FinancialDocuments.IStaffPaycheckReadOnlyReference> from, IDataRepository repo)
    {
        var ids = from.Select(r => r.Id).ToList();
        var entities = repo.StaffPaychecks.Where(i => ids.Contains(i.Id)).ToList();
        return entities;
    }

    public static void Fill(this Protocol.Models.FinancialDocuments.IWithdrawalReadOnlyModel from, Data.Entities.Documents.Finances.Withdrawal to)
    {
        if (from == null)
        {
            throw new ArgumentException("Expected an IWithdrawalReadOnlyModel, but null was given.");
        }

        to.Sum = from.Sum;
        to.Number = from.Number;
        to.Status = from.Status;
        to.Id = from.Reference?.Id ?? default;
        to.CurrencyId = from.Currency?.Id != 0 ? from.Currency?.Id : null;
        to.AccountableId = from.Accountable?.Id != 0 ? from.Accountable?.Id : null;
    }

    public static Data.Entities.Documents.Finances.Withdrawal ToEntity(this Protocol.Models.FinancialDocuments.IWithdrawalReadOnlyModel from)
    {
        var entity = new Data.Entities.Documents.Finances.Withdrawal();
        from.Fill(entity);
        return entity;
    }

    public static List<Data.Entities.Documents.Finances.Withdrawal> ToEntities(this IEnumerable<Protocol.Models.FinancialDocuments.IWithdrawalReadOnlyReference> from, IDataRepository repo)
    {
        var ids = from.Select(r => r.Id).ToList();
        var entities = repo.Withdrawals.Where(i => ids.Contains(i.Id)).ToList();
        return entities;
    }

    public static void Fill(this Protocol.Models.Documents.IIncomingOrderReadOnlyModel from, Data.Entities.Documents.Trade.IncomingOrder to, IDataRepository repo)
    {
        if (from == null)
        {
            throw new ArgumentException("Expected an IIncomingOrderReadOnlyModel, but null was given.");
        }

        to.Number = from.Number;
        to.Status = from.Status;
        to.Id = from.Reference?.Id ?? default;
        var previousGoodsSold = to.GoodsSold?.ToList() ?? new();
        to.GoodsSold = from.GoodsSold?.Select(i => i.ToEntity()).ToList() ?? new();
        var smallerGoodsSold = to.GoodsSold?.Count().PickSmaller(from.GoodsSold?.Count() ?? 0) ?? 0;
        int iGoodsSold = 0;
        foreach (var newItem in to.GoodsSold)
        {
            if (iGoodsSold == previousGoodsSold.Count)
            {
                break;
            }

            newItem.Id = previousGoodsSold[iGoodsSold++].Id;
        }

        to.CustomerId = from.Customer?.Id != 0 ? from.Customer?.Id : null;
        to.CurrencyId = from.Currency?.Id != 0 ? from.Currency?.Id : null;
        to.AccountableId = from.Accountable?.Id != 0 ? from.Accountable?.Id : null;
    }

    public static Data.Entities.Documents.Trade.IncomingOrder ToEntity(this Protocol.Models.Documents.IIncomingOrderReadOnlyModel from, IDataRepository repo)
    {
        var entity = new Data.Entities.Documents.Trade.IncomingOrder();
        from.Fill(entity, repo);
        return entity;
    }

    public static List<Data.Entities.Documents.Trade.IncomingOrder> ToEntities(this IEnumerable<Protocol.Models.Documents.IIncomingOrderReadOnlyReference> from, IDataRepository repo)
    {
        var ids = from.Select(r => r.Id).ToList();
        var entities = repo.IncomingOrders.Where(i => ids.Contains(i.Id)).ToList();
        return entities;
    }

    public static void Fill(this Protocol.Models.GoodTransactionLines.IIncomingOrderLineReadOnlyModel from, Data.Entities.Documents.Trade.IncomingOrderLine to)
    {
        if (from == null)
        {
            throw new ArgumentException("Expected an IIncomingOrderLineReadOnlyModel, but null was given.");
        }

        to.Quantity = from.Quantity;
        to.Price = from.Price;
        to.IncomingOrderId = from.IncomingOrder?.Id != 0 ? from.IncomingOrder?.Id : null;
        to.GoodId = from.Good?.Id != 0 ? from.Good?.Id : null;
    }

    public static Data.Entities.Documents.Trade.IncomingOrderLine ToEntity(this Protocol.Models.GoodTransactionLines.IIncomingOrderLineReadOnlyModel from)
    {
        var entity = new Data.Entities.Documents.Trade.IncomingOrderLine();
        from.Fill(entity);
        return entity;
    }

    public static void Fill(this Protocol.Models.Documents.IOutgoingOrderReadOnlyModel from, Data.Entities.Documents.Trade.OutgoingOrder to, IDataRepository repo)
    {
        if (from == null)
        {
            throw new ArgumentException("Expected an IOutgoingOrderReadOnlyModel, but null was given.");
        }

        to.Number = from.Number;
        to.Status = from.Status;
        to.Id = from.Reference?.Id ?? default;
        var previousGoodsBought = to.GoodsBought?.ToList() ?? new();
        to.GoodsBought = from.GoodsBought?.Select(i => i.ToEntity()).ToList() ?? new();
        var smallerGoodsBought = to.GoodsBought?.Count().PickSmaller(from.GoodsBought?.Count() ?? 0) ?? 0;
        int iGoodsBought = 0;
        foreach (var newItem in to.GoodsBought)
        {
            if (iGoodsBought == previousGoodsBought.Count)
            {
                break;
            }

            newItem.Id = previousGoodsBought[iGoodsBought++].Id;
        }

        to.SupplierId = from.Supplier?.Id != 0 ? from.Supplier?.Id : null;
        to.CurrencyId = from.Currency?.Id != 0 ? from.Currency?.Id : null;
        to.AccountableId = from.Accountable?.Id != 0 ? from.Accountable?.Id : null;
    }

    public static Data.Entities.Documents.Trade.OutgoingOrder ToEntity(this Protocol.Models.Documents.IOutgoingOrderReadOnlyModel from, IDataRepository repo)
    {
        var entity = new Data.Entities.Documents.Trade.OutgoingOrder();
        from.Fill(entity, repo);
        return entity;
    }

    public static List<Data.Entities.Documents.Trade.OutgoingOrder> ToEntities(this IEnumerable<Protocol.Models.Documents.IOutgoingOrderReadOnlyReference> from, IDataRepository repo)
    {
        var ids = from.Select(r => r.Id).ToList();
        var entities = repo.OutgoingOrders.Where(i => ids.Contains(i.Id)).ToList();
        return entities;
    }

    public static void Fill(this Protocol.Models.GoodTransactionLines.IOutgoingOrderLineReadOnlyModel from, Data.Entities.Documents.Trade.OutgoingOrderLine to)
    {
        if (from == null)
        {
            throw new ArgumentException("Expected an IOutgoingOrderLineReadOnlyModel, but null was given.");
        }

        to.Quantity = from.Quantity;
        to.Price = from.Price;
        to.GoodId = from.Good?.Id != 0 ? from.Good?.Id : null;
    }

    public static Data.Entities.Documents.Trade.OutgoingOrderLine ToEntity(this Protocol.Models.GoodTransactionLines.IOutgoingOrderLineReadOnlyModel from)
    {
        var entity = new Data.Entities.Documents.Trade.OutgoingOrderLine();
        from.Fill(entity);
        return entity;
    }

    public static void Fill(this Protocol.Models.Entities.IPermissionsReadOnlyModel from, Data.Entities.Permissions to)
    {
        if (from == null)
        {
            throw new ArgumentException("Expected an IPermissionsReadOnlyModel, but null was given.");
        }

        to.Currency_Edit = from.Currency_Edit;
        to.Currency_Read = from.Currency_Read;
        to.Currency_Delete = from.Currency_Delete;
        to.Customer_Edit = from.Customer_Edit;
        to.Customer_Read = from.Customer_Read;
        to.Customer_Delete = from.Customer_Delete;
        to.Deposit_Edit = from.Deposit_Edit;
        to.Deposit_Read = from.Deposit_Read;
        to.Deposit_Delete = from.Deposit_Delete;
        to.Employee_Edit = from.Employee_Edit;
        to.Employee_Read = from.Employee_Read;
        to.Employee_Delete = from.Employee_Delete;
        to.Good_Edit = from.Good_Edit;
        to.Good_Read = from.Good_Read;
        to.Good_Delete = from.Good_Delete;
        to.IncomingOrder_Edit = from.IncomingOrder_Edit;
        to.IncomingOrder_Read = from.IncomingOrder_Read;
        to.IncomingOrder_Delete = from.IncomingOrder_Delete;
        to.IncomingPayment_Edit = from.IncomingPayment_Edit;
        to.IncomingPayment_Read = from.IncomingPayment_Read;
        to.IncomingPayment_Delete = from.IncomingPayment_Delete;
        to.OutgoingOrder_Edit = from.OutgoingOrder_Edit;
        to.OutgoingOrder_Read = from.OutgoingOrder_Read;
        to.OutgoingOrder_Delete = from.OutgoingOrder_Delete;
        to.OutgoingPayment_Edit = from.OutgoingPayment_Edit;
        to.OutgoingPayment_Read = from.OutgoingPayment_Read;
        to.OutgoingPayment_Delete = from.OutgoingPayment_Delete;
        to.StaffPaycheck_Edit = from.StaffPaycheck_Edit;
        to.StaffPaycheck_Read = from.StaffPaycheck_Read;
        to.StaffPaycheck_Delete = from.StaffPaycheck_Delete;
        to.Supplier_Edit = from.Supplier_Edit;
        to.Supplier_Read = from.Supplier_Read;
        to.Supplier_Delete = from.Supplier_Delete;
        to.User_Edit = from.User_Edit;
        to.User_Read = from.User_Read;
        to.User_Delete = from.User_Delete;
        to.Withdrawal_Edit = from.Withdrawal_Edit;
        to.Withdrawal_Read = from.Withdrawal_Read;
        to.Withdrawal_Delete = from.Withdrawal_Delete;
        to.CalculateEmployess = from.CalculateEmployess;
        to.GetMaxValues = from.GetMaxValues;
        to.GetMinValues = from.GetMinValues;
        to.Reports_Financial = from.Reports_Financial;
        to.Deposit_ChangeStatus_MoveDown_ToDraft = from.Deposit_ChangeStatus_MoveDown_ToDraft;
        to.Deposit_ChangeStatus_MoveDown_ToSubmitted = from.Deposit_ChangeStatus_MoveDown_ToSubmitted;
        to.Deposit_ChangeStatus_MoveDown_ToReady = from.Deposit_ChangeStatus_MoveDown_ToReady;
        to.Deposit_ChangeStatus_MoveDown_ToExecuted = from.Deposit_ChangeStatus_MoveDown_ToExecuted;
        to.Deposit_ChangeStatus_MoveUp_ToSubmitted = from.Deposit_ChangeStatus_MoveUp_ToSubmitted;
        to.Deposit_ChangeStatus_MoveUp_ToReady = from.Deposit_ChangeStatus_MoveUp_ToReady;
        to.Deposit_ChangeStatus_MoveUp_ToExecuted = from.Deposit_ChangeStatus_MoveUp_ToExecuted;
        to.Deposit_ChangeStatus_MoveUp_ToClosed = from.Deposit_ChangeStatus_MoveUp_ToClosed;
        to.IncomingPayment_ChangeStatus_MoveDown_ToDraft = from.IncomingPayment_ChangeStatus_MoveDown_ToDraft;
        to.IncomingPayment_ChangeStatus_MoveDown_ToSubmitted = from.IncomingPayment_ChangeStatus_MoveDown_ToSubmitted;
        to.IncomingPayment_ChangeStatus_MoveDown_ToReady = from.IncomingPayment_ChangeStatus_MoveDown_ToReady;
        to.IncomingPayment_ChangeStatus_MoveDown_ToExecuted = from.IncomingPayment_ChangeStatus_MoveDown_ToExecuted;
        to.IncomingPayment_ChangeStatus_MoveUp_ToSubmitted = from.IncomingPayment_ChangeStatus_MoveUp_ToSubmitted;
        to.IncomingPayment_ChangeStatus_MoveUp_ToReady = from.IncomingPayment_ChangeStatus_MoveUp_ToReady;
        to.IncomingPayment_ChangeStatus_MoveUp_ToExecuted = from.IncomingPayment_ChangeStatus_MoveUp_ToExecuted;
        to.IncomingPayment_ChangeStatus_MoveUp_ToClosed = from.IncomingPayment_ChangeStatus_MoveUp_ToClosed;
        to.OutgoingPayment_ChangeStatus_MoveDown_ToDraft = from.OutgoingPayment_ChangeStatus_MoveDown_ToDraft;
        to.OutgoingPayment_ChangeStatus_MoveDown_ToSubmitted = from.OutgoingPayment_ChangeStatus_MoveDown_ToSubmitted;
        to.OutgoingPayment_ChangeStatus_MoveDown_ToReady = from.OutgoingPayment_ChangeStatus_MoveDown_ToReady;
        to.OutgoingPayment_ChangeStatus_MoveDown_ToExecuted = from.OutgoingPayment_ChangeStatus_MoveDown_ToExecuted;
        to.OutgoingPayment_ChangeStatus_MoveUp_ToSubmitted = from.OutgoingPayment_ChangeStatus_MoveUp_ToSubmitted;
        to.OutgoingPayment_ChangeStatus_MoveUp_ToReady = from.OutgoingPayment_ChangeStatus_MoveUp_ToReady;
        to.OutgoingPayment_ChangeStatus_MoveUp_ToExecuted = from.OutgoingPayment_ChangeStatus_MoveUp_ToExecuted;
        to.OutgoingPayment_ChangeStatus_MoveUp_ToClosed = from.OutgoingPayment_ChangeStatus_MoveUp_ToClosed;
        to.StaffPaycheck_ChangeStatus_MoveDown_ToDraft = from.StaffPaycheck_ChangeStatus_MoveDown_ToDraft;
        to.StaffPaycheck_ChangeStatus_MoveDown_ToSubmitted = from.StaffPaycheck_ChangeStatus_MoveDown_ToSubmitted;
        to.StaffPaycheck_ChangeStatus_MoveDown_ToReady = from.StaffPaycheck_ChangeStatus_MoveDown_ToReady;
        to.StaffPaycheck_ChangeStatus_MoveDown_ToExecuted = from.StaffPaycheck_ChangeStatus_MoveDown_ToExecuted;
        to.StaffPaycheck_ChangeStatus_MoveUp_ToSubmitted = from.StaffPaycheck_ChangeStatus_MoveUp_ToSubmitted;
        to.StaffPaycheck_ChangeStatus_MoveUp_ToReady = from.StaffPaycheck_ChangeStatus_MoveUp_ToReady;
        to.StaffPaycheck_ChangeStatus_MoveUp_ToExecuted = from.StaffPaycheck_ChangeStatus_MoveUp_ToExecuted;
        to.StaffPaycheck_ChangeStatus_MoveUp_ToClosed = from.StaffPaycheck_ChangeStatus_MoveUp_ToClosed;
        to.Withdrawal_ChangeStatus_MoveDown_ToDraft = from.Withdrawal_ChangeStatus_MoveDown_ToDraft;
        to.Withdrawal_ChangeStatus_MoveDown_ToSubmitted = from.Withdrawal_ChangeStatus_MoveDown_ToSubmitted;
        to.Withdrawal_ChangeStatus_MoveDown_ToReady = from.Withdrawal_ChangeStatus_MoveDown_ToReady;
        to.Withdrawal_ChangeStatus_MoveDown_ToExecuted = from.Withdrawal_ChangeStatus_MoveDown_ToExecuted;
        to.Withdrawal_ChangeStatus_MoveUp_ToSubmitted = from.Withdrawal_ChangeStatus_MoveUp_ToSubmitted;
        to.Withdrawal_ChangeStatus_MoveUp_ToReady = from.Withdrawal_ChangeStatus_MoveUp_ToReady;
        to.Withdrawal_ChangeStatus_MoveUp_ToExecuted = from.Withdrawal_ChangeStatus_MoveUp_ToExecuted;
        to.Withdrawal_ChangeStatus_MoveUp_ToClosed = from.Withdrawal_ChangeStatus_MoveUp_ToClosed;
        to.IncomingOrder_ChangeStatus_MoveDown_ToDraft = from.IncomingOrder_ChangeStatus_MoveDown_ToDraft;
        to.IncomingOrder_ChangeStatus_MoveDown_ToSubmitted = from.IncomingOrder_ChangeStatus_MoveDown_ToSubmitted;
        to.IncomingOrder_ChangeStatus_MoveDown_ToReady = from.IncomingOrder_ChangeStatus_MoveDown_ToReady;
        to.IncomingOrder_ChangeStatus_MoveDown_ToExecuted = from.IncomingOrder_ChangeStatus_MoveDown_ToExecuted;
        to.IncomingOrder_ChangeStatus_MoveUp_ToSubmitted = from.IncomingOrder_ChangeStatus_MoveUp_ToSubmitted;
        to.IncomingOrder_ChangeStatus_MoveUp_ToReady = from.IncomingOrder_ChangeStatus_MoveUp_ToReady;
        to.IncomingOrder_ChangeStatus_MoveUp_ToExecuted = from.IncomingOrder_ChangeStatus_MoveUp_ToExecuted;
        to.IncomingOrder_ChangeStatus_MoveUp_ToClosed = from.IncomingOrder_ChangeStatus_MoveUp_ToClosed;
        to.OutgoingOrder_ChangeStatus_MoveDown_ToDraft = from.OutgoingOrder_ChangeStatus_MoveDown_ToDraft;
        to.OutgoingOrder_ChangeStatus_MoveDown_ToSubmitted = from.OutgoingOrder_ChangeStatus_MoveDown_ToSubmitted;
        to.OutgoingOrder_ChangeStatus_MoveDown_ToReady = from.OutgoingOrder_ChangeStatus_MoveDown_ToReady;
        to.OutgoingOrder_ChangeStatus_MoveDown_ToExecuted = from.OutgoingOrder_ChangeStatus_MoveDown_ToExecuted;
        to.OutgoingOrder_ChangeStatus_MoveUp_ToSubmitted = from.OutgoingOrder_ChangeStatus_MoveUp_ToSubmitted;
        to.OutgoingOrder_ChangeStatus_MoveUp_ToReady = from.OutgoingOrder_ChangeStatus_MoveUp_ToReady;
        to.OutgoingOrder_ChangeStatus_MoveUp_ToExecuted = from.OutgoingOrder_ChangeStatus_MoveUp_ToExecuted;
        to.OutgoingOrder_ChangeStatus_MoveUp_ToClosed = from.OutgoingOrder_ChangeStatus_MoveUp_ToClosed;
        to.TechnicalAdministrating = from.TechnicalAdministrating;
    }

    public static Data.Entities.Permissions ToEntity(this Protocol.Models.Entities.IPermissionsReadOnlyModel from)
    {
        var entity = new Data.Entities.Permissions();
        from.Fill(entity);
        return entity;
    }

    public static void Fill(this Protocol.Models.StatusMovementRegistryEntries.IAccountEntryReadOnlyModel from, Data.Entities.Registers.AccountEntry to)
    {
        if (from == null)
        {
            throw new ArgumentException("Expected an IAccountEntryReadOnlyModel, but null was given.");
        }

        to.SumPayable = from.SumPayable;
        to.SumReceivable = from.SumReceivable;
        to.Status = from.Status;
        to.ActorId = from.ActorId;
        to.RegisteredAt = from.RegisteredAt;
        to.CurrencyId = from.Currency?.Id != 0 ? from.Currency?.Id : null;
        to.CreditorId = from.Creditor?.Id != 0 ? from.Creditor?.Id : null;
        to.DebtorId = from.Debtor?.Id != 0 ? from.Debtor?.Id : null;
    }

    public static Data.Entities.Registers.AccountEntry ToEntity(this Protocol.Models.StatusMovementRegistryEntries.IAccountEntryReadOnlyModel from)
    {
        var entity = new Data.Entities.Registers.AccountEntry();
        from.Fill(entity);
        return entity;
    }

    public static void Fill(this Protocol.Models.StatusMovementRegistryEntries.ICashEntryReadOnlyModel from, Data.Entities.Documents.Finances.CashEntry to)
    {
        if (from == null)
        {
            throw new ArgumentException("Expected an ICashEntryReadOnlyModel, but null was given.");
        }

        to.Added = from.Added;
        to.Reserved = from.Reserved;
        to.Status = from.Status;
        to.ActorId = from.ActorId;
        to.RegisteredAt = from.RegisteredAt;
        to.CurrencyId = from.Currency?.Id != 0 ? from.Currency?.Id : null;
    }

    public static Data.Entities.Documents.Finances.CashEntry ToEntity(this Protocol.Models.StatusMovementRegistryEntries.ICashEntryReadOnlyModel from)
    {
        var entity = new Data.Entities.Documents.Finances.CashEntry();
        from.Fill(entity);
        return entity;
    }

    public static void Fill(this Protocol.Models.StatusMovementRegistryEntries.ISalarySettlementEntryReadOnlyModel from, Data.Entities.Registers.SalarySettlementEntry to)
    {
        if (from == null)
        {
            throw new ArgumentException("Expected an ISalarySettlementEntryReadOnlyModel, but null was given.");
        }

        to.Paid = from.Paid;
        to.Accrued = from.Accrued;
        to.Status = from.Status;
        to.ActorId = from.ActorId;
        to.RegisteredAt = from.RegisteredAt;
        to.PaidToId = from.PaidTo?.Id != 0 ? from.PaidTo?.Id : null;
    }

    public static Data.Entities.Registers.SalarySettlementEntry ToEntity(this Protocol.Models.StatusMovementRegistryEntries.ISalarySettlementEntryReadOnlyModel from)
    {
        var entity = new Data.Entities.Registers.SalarySettlementEntry();
        from.Fill(entity);
        return entity;
    }

    public static void Fill(this Protocol.Models.StatusMovementRegistryEntries.IStockEntryReadOnlyModel from, Data.Entities.Registers.StockEntry to)
    {
        if (from == null)
        {
            throw new ArgumentException("Expected an IStockEntryReadOnlyModel, but null was given.");
        }

        to.Added = from.Added;
        to.Reserved = from.Reserved;
        to.Status = from.Status;
        to.ActorId = from.ActorId;
        to.RegisteredAt = from.RegisteredAt;
        to.GoodId = from.Good?.Id != 0 ? from.Good?.Id : null;
    }

    public static Data.Entities.Registers.StockEntry ToEntity(this Protocol.Models.StatusMovementRegistryEntries.IStockEntryReadOnlyModel from)
    {
        var entity = new Data.Entities.Registers.StockEntry();
        from.Fill(entity);
        return entity;
    }
}