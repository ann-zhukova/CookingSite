using System.Diagnostics;
using JetBrains.Annotations;

namespace Core.Extensions;

public static class TaskExtension
{
    [StackTraceHidden]
    public static async Task<TOut> Convert<TIn, TOut>(
        this Task<TIn> thiz,
        [InstantHandle] Func<TIn, TOut> converter
    ) => converter(await thiz);
}