using MergeScripts.Services.Interfaces;

namespace MergeScripts.Services.Services
{
    public class MergeScriptsService : IMergeScriptService
    {
        public string[] AbrirArquivo(string arquivo)
        {
            return File.ReadAllLines(arquivo);
        }

        public StreamWriter CriarArquivo(string arquivoDestino)
        {
            return File.CreateText(arquivoDestino);
        }

        public IEnumerable<string> ListarArquivosDiretorio(string caminho)
        {
            return Directory.GetFiles(caminho);
        }

        public void RealizarMergeArquivos(string caminho, string novoArquivoNome)
        {
            var arquivoNomeCompleto = DefinirNomeNovoArquivo(caminho, novoArquivoNome);

            this.VerificarDiretorio(caminho);

            var arquivos = this.ListarArquivosDiretorio(caminho);

            ValidarArquivosEncontrados(arquivos);

            foreach (var arquivo in arquivos)
            {
                var linhas = this.AbrirArquivo(arquivo);

                this.AdicionarLinhaRodape(ref linhas);

                this.IncluirLinhasArquivo(arquivoNomeCompleto, linhas);
            }
        }

        public string DefinirNomeNovoArquivo(string caminho, string novoArquivoNome)
        {
            return Path.Combine(caminho, novoArquivoNome);
        }

        public void ValidarArquivosEncontrados(IEnumerable<string> arquivos)
        {
            if (arquivos == null || arquivos.Count() == 0)
                throw new FileNotFoundException();
        }

        public void IncluirLinhasArquivo(string arquivo, string[] linhas)
        {
            File.AppendAllLines(arquivo, linhas);
        }

        public void AdicionarLinhaRodape(ref string[] linhas)
        {
            var lista = linhas.ToList();

            if (!string.IsNullOrWhiteSpace(lista.LastOrDefault()))
                lista.Add(String.Empty);

            linhas = lista.ToArray();
        }

        public void VerificarDiretorio(string caminho)
        {
            if (!Directory.Exists(caminho))
                throw new DirectoryNotFoundException();

        }
    }
}
