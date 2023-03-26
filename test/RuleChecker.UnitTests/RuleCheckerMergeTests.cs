namespace RuleChecker.UnitTests;

public class RuleCheckerMergeTests
{
    [Fact]
    public void Merge_ShouldMergedRules_WhenRulesAreValid()
    {
        // Arrange
        var ruleChecker = new TestModelRuleChecker(new TestModel { Value = 5 });
        var ruleChecker2 = new TestModelRuleChecker2(new TestModel { Value = 8 });

        // Act
        var errorMessages = ruleChecker.Merge(ruleChecker2);
        var result = errorMessages.ExecuteRules();

        // Assert
        Assert.False(result.IsValid);
    }
}