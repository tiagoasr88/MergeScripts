// See https://aka.ms/new-console-template for more information

using MergeScripts.Services.Services;

try
{
    var commands = Environment.GetCommandLineArgs();

    if (commands.Count() <= 2)
        throw new ArgumentException("Argumentos não informados. Esperados: [diretorio] e [arquivoNome]");

    var diretorio = commands[1];    
    var arquivoNome = commands[2];

    var service = new MergeScriptsService();
    service.RealizarMergeArquivos(diretorio, arquivoNome);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}