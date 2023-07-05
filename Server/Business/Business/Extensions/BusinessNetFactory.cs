using Business.ActionPoints;
using Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Extensions;

file class DefaultExecutionContext : IExecutionContext
{
    public long OperationId { get; } = 0;

    public string SessionKey { get; }

    public string Language => Constants.Languages.English;
}

public static class BusinessNetFactoryExtensions
{
    public static IBusinessActionsNetSingleton CreateWithoutContext(this BusinessNetFactory businessNetFactory)
        => businessNetFactory.Create(new DefaultExecutionContext());
}
