namespace RuleChecker.UnitTests;

public class RuleCheckerInvalidRuleMessagesTests
{
    [Fact]
    public void GetInvalidRuleMessages_ShouldReturnEmptyList_WhenAllRulesPass()
    {
        // Arrange
        var ruleChecker = new TestModelRuleChecker(new TestModel { Value = 5 });

        // Act
        var errorMessages = ruleChecker.ExecuteRules().GetInvalidRuleMessages();

        // Assert
        Assert.Empty(errorMessages);
    }

    [Fact]
    public void ExecuteRules_ShouldExecuteAllRulesForEachModelInModelsList()
    {
        // Arrange
        var ruleChecker = new TestModelRuleChecker(new TestModel { Value = 25 });

        // Act
        var errorMessages = ruleChecker.ExecuteRules().GetInvalidRuleMessages();
        
        // Assert
        Assert.Single(errorMessages);
    }
}