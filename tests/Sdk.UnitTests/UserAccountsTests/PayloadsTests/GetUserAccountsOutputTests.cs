namespace HomeBudgetManager.Services.Accounts.Sdk.UnitTests.UserAccountsTests.PayloadsTests;

using System.Text.Json;

using Output = HomeBudgetManager.Services.Accounts.Sdk.UserAccounts.Payloads;

public class GetUserAccountsOutputTests
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private Output.GetUserAccountsOutput receivedOutput = null!;

    private string serializedOutput = null!;

    private Output.GetUserAccountsOutput deserializedOutput = null!;

    [Fact]
    public void SerializedWebApiOutputCanBeDeserializedIntoSdkOutput()
    {
        var output = new Output.GetUserAccountsOutput(
            [
                new(
                    Guid.NewGuid(),
                    "Account 1",
                    new(1, "Account Type 1"),
                    100.00m,
                    "USD",
                    true),
                new(
                    Guid.NewGuid(),
                    "Account 2",
                    new(2, "Account Type 2"),
                    200.00m,
                    "EUR",
                    false)
            ]);
        this.Given(t => t.ReceivedOutputIs(output))
            .And(t => t.OutputIsSerialized())
            .When(t => t.OutputIsDeserialized())
            .Then(t => t.DeserializedOutputIsEqualToReceivedOutput())
            .BDDfy();
    }

    private void ReceivedOutputIs(Output.GetUserAccountsOutput output) => this.receivedOutput = output;

    private void OutputIsSerialized() => this.serializedOutput = JsonSerializer.Serialize(this.receivedOutput, JsonSerializerOptions);

    private void OutputIsDeserialized() => this.deserializedOutput = JsonSerializer.Deserialize<Output.GetUserAccountsOutput>(this.serializedOutput, JsonSerializerOptions)!;

    private void DeserializedOutputIsEqualToReceivedOutput()
    {
        this.deserializedOutput.Accounts.ShouldBeEquivalentTo(
            this.receivedOutput.Accounts.Select(account => new Output.AccountInfo(
                account.AccountId,
                account.Name,
                new(account.AccountType.Id, account.AccountType.Name),
                account.Balance,
                account.Currency,
                account.IsActive))
            .ToArray());
    }
}
