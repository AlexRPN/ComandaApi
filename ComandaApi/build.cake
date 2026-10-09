//////////////////////////////////////////////////////////////////////
// Cake build script - provides target 'testreport' to run tests with
// coverage and generate HTML report using ReportGenerator.
//////////////////////////////////////////////////////////////////////

#tool nuget:?package=ReportGenerator&version=5.5.11

var target = Argument("target", "Default");

Task("testreport")
	.Does(() =>
{
	Information("Running tests with coverage...");
	var testProj = "../Comanda.Dominio.Testes/Comanda.Dominio.Testes.csproj";
	StartProcess("dotnet", $"test \"{testProj}\" --collect:\"XPlat Code Coverage\" --results-directory TestResults");

	var coverage = GetFiles("TestResults/**/coverage.cobertura.xml").FirstOrDefault();
	if (coverage == null)
	{
		throw new Exception("coverage.cobertura.xml not found in TestResults");
	}

	Information("Generating HTML report with ReportGenerator...");
	StartProcess("reportgenerator", $"-reports:\"{coverage.FullPath}\" -targetdir:\"coverage-report\" -reporttypes:Html");

	Information("Coverage report available at: coverage-report/index.html");
});

Task("Default")
	.IsDependentOn("testreport");

RunTarget(target);
