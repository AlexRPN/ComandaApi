//////////////////////////////////////////////////////////////////////
// Cake build script
// Target: testreport
//
// Responsável por:
// 1. Limpar resultados antigos
// 2. Executar testes unitários
// 3. Coletar cobertura
// 4. Gerar relatório HTML
// 5. Abrir o relatório automaticamente
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");

//////////////////////////////////////////////////////////////////////
// Configurações
//////////////////////////////////////////////////////////////////////

var testProject = "../Comanda.Dominio.Testes/Comanda.Dominio.Testes.csproj";

var testResultsDirectory = "TestResults";

var coverageReportDirectory = "coverage-report";

//////////////////////////////////////////////////////////////////////
// Target: testreport
//////////////////////////////////////////////////////////////////////

Task("testreport")
    .Does(() =>
{
    Information("========================================");
    Information("Iniciando testes com cobertura...");
    Information("========================================");

    //////////////////////////////////////////////////////////////////
    // 1. Valida projeto de testes
    //////////////////////////////////////////////////////////////////

    if (!FileExists(testProject))
    {
        throw new Exception(
            $"Projeto de testes não encontrado: {testProject}");
    }

    //////////////////////////////////////////////////////////////////
    // 2. Limpa TestResults antigos
    //////////////////////////////////////////////////////////////////

    if (DirectoryExists(testResultsDirectory))
    {
        Information(
            $"Removendo resultados antigos: {testResultsDirectory}");

        DeleteDirectory(
            testResultsDirectory,
            new DeleteDirectorySettings
            {
                Recursive = true,
                Force = true
            });
    }

    //////////////////////////////////////////////////////////////////
    // 3. Limpa relatório HTML anterior
    //////////////////////////////////////////////////////////////////

    if (DirectoryExists(coverageReportDirectory))
    {
        Information(
            $"Removendo relatório antigo: {coverageReportDirectory}");

        DeleteDirectory(
            coverageReportDirectory,
            new DeleteDirectorySettings
            {
                Recursive = true,
                Force = true
            });
    }

    //////////////////////////////////////////////////////////////////
    // 4. Executa testes com cobertura
    //////////////////////////////////////////////////////////////////

    Information("Executando testes unitários...");

    var testResult = StartProcess(
        "dotnet",
        new ProcessSettings
        {
            Arguments = new ProcessArgumentBuilder()
                .Append("test")
                .AppendQuoted(testProject)
                .Append("--collect:\"XPlat Code Coverage\"")
                .Append($"--results-directory \"{testResultsDirectory}\"")
                .Append("--verbosity normal")
        });

    if (testResult != 0)
    {
        throw new Exception(
            $"dotnet test falhou com código de saída {testResult}");
    }

    //////////////////////////////////////////////////////////////////
    // 5. Localiza arquivo de cobertura gerado
    //////////////////////////////////////////////////////////////////

    var coverageFiles =
        GetFiles($"{testResultsDirectory}/**/coverage.cobertura.xml");

    if (!coverageFiles.Any())
    {
        throw new Exception(
            "Arquivo coverage.cobertura.xml não encontrado.");
    }

    if (coverageFiles.Count > 1)
    {
        Warning(
            $"Foram encontrados {coverageFiles.Count} arquivos de cobertura.");

        foreach (var file in coverageFiles)
        {
            Warning($"Coverage encontrado: {file.FullPath}");
        }
    }

    // Como TestResults foi limpo antes da execução,
    // normalmente existirá apenas um arquivo.
    var coverageFile = coverageFiles.First();

    Information(
        $"Arquivo de cobertura encontrado: {coverageFile.FullPath}");

    //////////////////////////////////////////////////////////////////
    // 6. Prepara caminhos para ReportGenerator
    //////////////////////////////////////////////////////////////////

    var coveragePath =
        coverageFile.FullPath.Replace("\\", "/");

    var reportDirectory =
        coverageReportDirectory.Replace("\\", "/");

    //////////////////////////////////////////////////////////////////
    // 7. Gera relatório HTML
    //////////////////////////////////////////////////////////////////

    Information("Gerando relatório HTML...");

    var reportResult = StartProcess(
        "dotnet",
        new ProcessSettings
        {
            Arguments = new ProcessArgumentBuilder()
                .Append("tool")
                .Append("run")
                .Append("reportgenerator")
                .Append("--")
                .AppendQuoted($"-reports:{coveragePath}")
                .AppendQuoted($"-targetdir:{reportDirectory}")
                .Append("-reporttypes:Html")
        });

    if (reportResult != 0)
    {
        throw new Exception(
            $"ReportGenerator falhou com código de saída {reportResult}");
    }

    //////////////////////////////////////////////////////////////////
    // 8. Localiza index.html
    //////////////////////////////////////////////////////////////////

    var reportIndex = System.IO.Path.Combine(
        System.IO.Directory.GetCurrentDirectory(),
        coverageReportDirectory,
        "index.html");

    //////////////////////////////////////////////////////////////////
    // 9. Abre relatório automaticamente no Windows
    //////////////////////////////////////////////////////////////////

    if (System.IO.File.Exists(reportIndex))
    {
        Information(
            $"Abrindo relatório: {reportIndex}");

        StartProcess(
            "cmd",
            new ProcessSettings
            {
                Arguments = new ProcessArgumentBuilder()
                    .Append("/c")
                    .Append("start")
                    .AppendQuoted("")
                    .AppendQuoted(reportIndex)
            });
    }
    else
    {
        Warning(
            $"Arquivo index.html não encontrado: {reportIndex}");
    }

    //////////////////////////////////////////////////////////////////
    // 10. Finalização
    //////////////////////////////////////////////////////////////////

    Information("========================================");
    Information("Testes e cobertura concluídos.");
    Information(
        $"Relatório disponível em: {coverageReportDirectory}/index.html");
    Information("========================================");
});

//////////////////////////////////////////////////////////////////////
// Target padrão
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("testreport");

//////////////////////////////////////////////////////////////////////
// Execução
//////////////////////////////////////////////////////////////////////

RunTarget(target);