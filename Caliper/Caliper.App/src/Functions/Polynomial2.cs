using System;
using System.Diagnostics.Contracts;
using ImpliciX.Language.Control;

namespace Caliper.App
{
    public class Polynomial2
    {
        public static FuncRef Func => new FuncRef(nameof(Polynomial2), () => Runner, xUrns => xUrns);
        public static FunctionRun Runner =>
          (functionDefinition, xs) =>
          {
              Contract.Assert(xs.Length == 2, "Polynomial2 should have two variable");
              float result = 0;

              foreach (var param in functionDefinition.Params)
              {
                  var paramName = param.Key;
                  var hasParsedParam1 = int.TryParse(paramName.Substring(1, 1), out var i);
                  var hasParsedParam2 = int.TryParse(paramName.Substring(2, 1), out var j);

                  Contract.Assert(paramName[0] == 'a' && hasParsedParam1 && hasParsedParam2,
                $"Invalid parameter {paramName}, should be \"aij\", (i,j) being integers.");
                  result += param.Value * MathF.Pow(xs[0].value, i) * MathF.Pow(xs[1].value, j);
              }

              return result;
          };
    }
}