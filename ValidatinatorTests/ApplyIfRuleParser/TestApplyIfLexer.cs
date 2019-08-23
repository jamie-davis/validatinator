using System.Linq;
using TestConsole.OutputFormatting;
using TestConsoleLib;
using TestConsoleLib.Testing;
using Validatinator.ApplyIfRuleParser;
using Xunit;

namespace ValidatinatorTests.ApplyIfRuleParser
{
    public class TestApplyIfLexer
    {
        private static readonly string[] TestStrings = 
        {
            "firsterror",
            "FirstError",
            "cleaninput",
            "CleanInput",
            "identifier.another",
            "@error",
            "match(entity.field)",
            "Match,entity,",
        };

        [Fact]
        public void LexerExtractsNameTokens()
        {
            //Arrange
            var output = new Output();

            //Act
            var results = TestStrings
                .Select(s => new { String = s, Result = ApplyIfLexer.Analyse(s).ToList() })
                .ToList();

            //Assert
            var report = results.AsReport(rep => rep
                .AddColumn(r => r.String, cc => cc.Heading("Input"))
                .AddChild(r => r.Result, res => res
                    .AddColumn(r => r.TokenType, cc => {})
                    .AddColumn(r => r.Text, cc => {})));
                
            output.FormatTable(report);
            output.Report.Verify();
        }
    }
}
