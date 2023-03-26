using System.Linq.Expressions;

namespace RuleChecker;

public class Rule<TModel> : IRule
{
    public Type Type => typeof(TModel);
    public Expression<Func<TModel, bool>> Condition { get; init; }
    public string Message { get; init; }

    public bool IsPassed { get; private set; } = true;

    public void Validate(object model)
    {
        if (!Condition.Compile().Invoke((TModel)model))
            IsPassed = false;
    }
}