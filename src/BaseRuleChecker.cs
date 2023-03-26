namespace RuleChecker;

using System.Linq.Expressions;

public class BaseRuleChecker
{
    /// <summary>
    ///  List of objects that represent the models to be checked by the rules.
    /// </summary>
    protected List<object> Models { get; set; }
    
    /// <summary>
    ///  List of IRule objects that represent the rules to be executed.
    /// </summary>
    private List<IRule> Rules { get; set; }

    protected BaseRuleChecker()
    {
        Rules = new List<IRule>();
        Models = new List<object>();
    }

    /// <summary>
    ///   Bool property that returns true if all the rules are passed, and false otherwise.
    /// </summary>
    public bool IsValid => Rules.All(r => r.IsPassed);

    
    /// <summary>
    ///  A method that returns a list of error messages for the rules that failed.
    /// </summary>
    /// <returns>A list of error messages</returns>
    public List<string> GetInvalidRuleMessages() =>
        Rules
            .Where(r => !r.IsPassed)
            .Select(r => r.Message)
            .ToList();

    /// <summary>
    ///  Executes all the rules for each model in the Models list.v
    /// </summary>
    /// <returns></returns>
    public BaseRuleChecker ExecuteRules()
    {
        foreach (var model in Models)
        {
            var modelRules = Rules.Where(x => x.Type == model.GetType());

            foreach (var rule in modelRules)
            {
                rule.Validate(model);
            }
        }

        return this;
    }

    /// <summary>
    ///  A method that adds a new rule to the Rules list for the specified model type.
    /// </summary>
    /// <param name="condition">A lambda expression that represents the condition to be checked.</param>
    /// <param name="message">A string that represents the error message to be returned if the rule fails.</param>
    /// <typeparam name="TModel">The type of the model to be checked.</typeparam>
    protected BaseRuleChecker AddRule<TModel>(Expression<Func<TModel, bool>> condition, string message)
    {
        var rule = new Rule<TModel>()
        {
            Condition = condition,
            Message = message,
        };

        Rules.Add(rule);

        return this;
    }

    /// <summary>
    /// A method that creates a new BaseRuleChecker instance and merges the rules and models of the current and other instances into the new instance.
    /// </summary>
    /// <param name="other">The other BaseRuleChecker instance to be merged.</param>
    public BaseRuleChecker Merge(BaseRuleChecker other) =>
        new()
        {
            Rules = Rules.Concat(other.Rules).ToList(),
            Models = Models.Concat(other.Models).ToList()
        };
}