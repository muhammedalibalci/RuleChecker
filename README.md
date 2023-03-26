# RuleChecker

This is a C# library that provides a way to check rules against a list of models. The BaseRuleChecker class is provided to be inherited by other classes to add specific rules.

## Basic Usage

1. Create a class that inherits from the BaseRuleChecker class and add any specific rules to it. For example, the OrderRuleChecker class adds rules specific to Order objects:

``` csharp
public class OrderRuleChecker : BaseRuleChecker
{
    public OrderRuleChecker(Order order)
    {
        Models = new List<object> { order };
        AddOrderRules();
    }

    private void AddOrderRules()
    {
        AddRule<Order>(o => o.OrderItems.Any(),
            "An order must have at least one item");

        AddRule<Order>(o => o.OrderItems.All(oi => oi.Quantity > 0),
            "An order item must have a quantity greater than 0");

        AddRule<Order>(o => o.OrderItems.Sum(oi => oi.Product.Price * oi.Quantity) == o.TotalPrice,
            "The total price of the order must match the sum of the prices of its items");
    }
}

```

2. Create an instance of the rule checker and execute the rules:

``` csharp
var orderRuleChecker = new OrderRuleChecker(order);

var validationResult = orderRuleChecker.ExecuteRules();

if (validationResult.IsValid)
{
    // Do something if all rules pass
}
else
{
    // Handle errors if some rules fail
    var errors = validationResult.GetInvalidRuleMessages();
}
```

## Merge Rules

The Merge method can be used to merge the rules and models of multiple rule checkers into a single rule checker instance. For example:

``` csharp
var orderRuleChecker  = new OrderRuleChecker(order);
var orderRuleChecker2 = new OrderRuleChecker2(order);
var orderRuleChecker3 = new OrderRuleChecker3(order);

var validationResult = orderRuleChecker
          .Merge(orderRuleChecker2)
          .Merge(orderRuleChecker3)
          .ExecuteRules();

if (validationResult.IsValid)
{
    // Do something if all rules pass
}
else
{
    // Handle errors if some rules fail
    var errors = validationResult.GetInvalidRuleMessages();
}
```


## API

`BaseRuleChecker`: 
This is the base class that other rule checkers should inherit from. It provides the following properties and methods:

  * `Models`:
  A list of objects that represent the models to be checked by the rules.

  * `Rules property`:
  A list of IRule objects that represent the rules to be executed.

  * `IsValid`:
  A boolean property that returns true if all the rules are passed, and false otherwise.

  * `GetInvalidRuleMessages()`:
  A method that returns a list of error messages for the rules that failed.

  * `ExecuteRules()`:
  Executes all the rules for each model in the Models list.

  * `AddRule<TModel>()`:
  Adds a new rule to the Rules list for the specified model type.

  * `Merge()`:
  Creates a new BaseRuleChecker instance and merges the rules and models of the current and other instances into the new instance.

`IRule`:
This interface represents a rule that can be executed against a model. It provides the following properties and methods:

  * `Type`:
  A Type property that returns the type of the model associated with the rule.

  * `Message`:
  A string property that contains the error message for the rule if it fails.

  * `IsPassed`:
  A boolean property that indicates whether the rule passed or failed.

  * `Validate()`:
  A method that validates the specified model object against the rule condition.

`Rule<TModel>`
This is a class that implements the IRule interface for a specific model type. It provides the following properties and methods:

  * `Type`:
  A Type property that returns the type of the model associated with the rule.

  * `Condition`:
  A Expression<Func<TModel, bool>> property that represents the condition to be
