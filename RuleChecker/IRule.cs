namespace RuleChecker;

public interface IRule
{
    /// <summary>
    /// A Type property that returns the type of the model associated with the rule.
    /// </summary>
    Type Type { get; }
    
    /// <summary>
    /// A string property that contains the error message for the rule if it fails.
    /// </summary>
    string Message { get; }
    
    /// <summary>
    /// A bool property that indicates whether the rule passed or failed.
    /// </summary>
    bool IsPassed { get; }

    
    /// <summary>
    /// A method that validates the specified model object against the rule condition.
    /// </summary>
    void Validate(object model);
}