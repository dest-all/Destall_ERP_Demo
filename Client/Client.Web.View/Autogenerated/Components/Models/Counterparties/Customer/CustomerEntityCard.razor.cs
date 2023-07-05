// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using Client.Web.View.Components.Entities;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using DestallMaterials.Blazor.Components.Universal;
using Protocol.Models.Entities;
using Client.Communication;
using MudBlazor;
using DestallMaterials.WheelProtection.Extensions.Objects;
using Protocol.Models;
using DestallMaterials.WheelProtection.Extensions.Tasks;
using Client.Web.View.Extensions;

namespace Client.Web.View.Components.Models.Counterparties.Customer;
public partial class CustomerEntityCard
{
    internal static RenderFragment RenderProperty_1(Protocol.Models.Counterparties.ICustomerReadOnlyModel item, bool disabled = false, Func<Task> onChange = null, bool collapsed = true) => renderTree =>
    {
        renderTree.OpenComponent<Client.Web.View.Components.Models.Counterparties.Customer.PropertyInputs.Name>(1);
        renderTree.AddAttribute(1, "Item", item);
        renderTree.AddAttribute(2, "Disabled", disabled);
        renderTree.AddAttribute(3, "OnChange", onChange ?? (async () =>
        {
        }));
        renderTree.CloseComponent();
    };
    public IEnumerable<RenderFragment> RenderPropertiesInOrder(IEnumerable<int> order)
    {
        var item = Item;
        foreach (var i in order)
        {
            bool disabled = i < 0 || Disabled || _deleted;
            yield return i switch
            {
                1 => RenderProperty_1(item, disabled, OnChange, Collapsed),
                _ => throw new InvalidOperationException()};
        }
    }

    public IEnumerable<RenderFragment> RenderAllProperties() => RenderPropertiesInOrder(Enumerable.Range(1, 1));
    void ClearCache() => client.InvalidateItemCacheAsync(Item).Forget();
    public IEnumerable<ButtonConfiguration> GetStandardButtons()
    {
        if (Permissions.Customer_Edit)
        {
            yield return new()
            {Disabled = Disabled, Callback = async () =>
            {
                var result = await client.Actions.Customer.Save.CallAsync(Item);
                ClearCache();
                result.CopyTo(Item);
                StateHasChanged();
                await OnChange();
            }, ActionName = "", Icon = Icons.Material.Filled.Save, Color = Color.Info};
        }

        if (Permissions.Customer_Delete && Item.Reference.IsEmpty() == false)
        {
            yield return new()
            {Disabled = Disabled, Callback = async () =>
            {
                if (await client.Actions.Customer.Delete.CallAsync(Item.Reference.Id.Yield()))
                {
                    Disabled = true;
                    ClearCache();
                    Item.Reference.Representation = $"* Deleted * {Item.Reference}";
                    await OnDeleted();
                }
            }, ActionName = "", Icon = Icons.Material.Filled.Delete, Color = Color.Error};
        }
    }
}