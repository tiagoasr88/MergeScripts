namespace MergeScripts.Services.Interfaces
{
    public interface IMergeScriptService
    {
        void VerificarDiretorio(string caminho);

        IEnumerable<string> ListarArquivosDiretorio(string caminho);

        string[] AbrirArquivo(string arquivo);

        StreamWriter CriarArquivo(string arquivoDestino);

        void RealizarMergeArquivos(string caminho, string novoArquivoNome);
        void IncluirLinhasArquivo(string arquivo, string[] linhas);
        void ValidarArquivosEncontrados(IEnumerable<string> arquivos);
        void AdicionarLinhaRodape(ref string[] linhas);
    }
}
