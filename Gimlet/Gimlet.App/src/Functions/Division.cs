using System.Diagnostics.Contracts;
using ImpliciX.Language.Control;

namespace Gimlet.App
{
    public class Division
    {
        public static FuncRef Func => new FuncRef(nameof(Division), () => Runner, xUrns => xUrns);
        public static FunctionRun Runner =>
          (functionDefinition, xs) =>
          {
        Contract.Assert(xs.Length == 2, "Substract should have two variables");
        var result = xs[0].value / xs[1].value;
        return result;
      };
    }
}