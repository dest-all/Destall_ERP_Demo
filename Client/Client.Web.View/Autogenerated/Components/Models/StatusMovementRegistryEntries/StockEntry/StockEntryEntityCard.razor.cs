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

namespace Client.Web.View.Components.Models.StatusMovementRegistryEntries.StockEntry;
public partial class StockEntryEntityCard
{
    internal static RenderFragment RenderProperty_1(Protocol.Models.StatusMovementRegistryEntries.IStockEntryReadOnlyModel item, bool disabled = false, Func<Task> onChange = null, bool collapsed = true) => renderTree =>
    {
        renderTree.OpenComponent<Client.Web.View.Components.Models.StatusMovementRegistryEntries.StockEntry.PropertyInputs.Added>(1);
        renderTree.AddAttribute(1, "Item", item);
        renderTree.AddAttribute(2, "Disabled", disabled);
        renderTree.AddAttribute(3, "OnChange", onChange ?? (async () =>
        {
        }));
        renderTree.CloseComponent();
    };
    internal static RenderFragment RenderProperty_2(Protocol.Models.StatusMovementRegistryEntries.IStockEntryReadOnlyModel item, bool disabled = false, Func<Task> onChange = null, bool collapsed = true) => renderTree =>
    {
        renderTree.OpenComponent<Client.Web.View.Components.Models.StatusMovementRegistryEntries.StockEntry.PropertyInputs.Reserved>(1);
        renderTree.AddAttribute(1, "Item", item);
        renderTree.AddAttribute(2, "Disabled", disabled);
        renderTree.AddAttribute(3, "OnChange", onChange ?? (async () =>
        {
        }));
        renderTree.CloseComponent();
    };
    internal static RenderFragment RenderProperty_3(Protocol.Models.StatusMovementRegistryEntries.IStockEntryReadOnlyModel item, bool disabled = false, Func<Task> onChange = null, bool collapsed = true) => renderTree =>
    {
        renderTree.OpenComponent<Client.Web.View.Components.Models.StatusMovementRegistryEntries.StockEntry.PropertyInputs.Status>(1);
        renderTree.AddAttribute(1, "Item", item);
        renderTree.AddAttribute(2, "Disabled", disabled);
        renderTree.AddAttribute(3, "OnChange", onChange ?? (async () =>
        {
        }));
        renderTree.CloseComponent();
    };
    internal static RenderFragment RenderProperty_4(Protocol.Models.StatusMovementRegistryEntries.IStockEntryReadOnlyModel item, bool disabled = false, Func<Task> onChange = null, bool collapsed = true) => renderTree =>
    {
        renderTree.OpenComponent<Client.Web.View.Components.Models.StatusMovementRegistryEntries.StockEntry.PropertyInputs.ActorId>(1);
        renderTree.AddAttribute(1, "Item", item);
        renderTree.AddAttribute(2, "Disabled", disabled);
        renderTree.AddAttribute(3, "OnChange", onChange ?? (async () =>
        {
        }));
        renderTree.CloseComponent();
    };
    internal static RenderFragment RenderProperty_5(Protocol.Models.StatusMovementRegistryEntries.IStockEntryReadOnlyModel item, bool disabled = false, Func<Task> onChange = null, bool collapsed = true) => renderTree =>
    {
        renderTree.OpenComponent<Client.Web.View.Components.Models.StatusMovementRegistryEntries.StockEntry.PropertyInputs.RegisteredAt>(1);
        renderTree.AddAttribute(1, "Item", item);
        renderTree.AddAttribute(2, "Disabled", disabled);
        renderTree.AddAttribute(3, "OnChange", onChange ?? (async () =>
        {
        }));
        renderTree.CloseComponent();
    };
    internal static RenderFragment RenderProperty_6(Protocol.Models.StatusMovementRegistryEntries.IStockEntryReadOnlyModel item, bool disabled = false, Func<Task> onChange = null, bool collapsed = true) => renderTree =>
    {
        renderTree.OpenComponent<Client.Web.View.Components.Models.StatusMovementRegistryEntries.StockEntry.PropertyInputs.Good>(1);
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
                2 => RenderProperty_2(item, disabled, OnChange, Collapsed),
                3 => RenderProperty_3(item, disabled, OnChange, Collapsed),
                4 => RenderProperty_4(item, disabled, OnChange, Collapsed),
                5 => RenderProperty_5(item, disabled, OnChange, Collapsed),
                6 => RenderProperty_6(item, disabled, OnChange, Collapsed),
                _ => throw new InvalidOperationException()};
        }
    }

    public IEnumerable<RenderFragment> RenderAllProperties() => RenderPropertiesInOrder(Enumerable.Range(1, 6));
}