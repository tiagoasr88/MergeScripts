using MergeScripts.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void IncluirLinhaArquivo(StreamWriter sw, string linha)
        {
            sw.WriteLine(linha);
        }

        public IEnumerable<string> ListarArquivosDiretorio(string caminho)
        {
            return Directory.GetFiles(caminho);
        }

        public void RealizarMergeArquivos(string caminho, string novoArquivoNome)
        {
            this.VerificarDiretorio(caminho);

            var arquivos = this.ListarArquivosDiretorio(caminho);

            if (arquivos == null || arquivos.Count() == 0)
                throw new FileNotFoundException();

            using (var sw = this.CriarArquivo(Path.Combine(caminho, novoArquivoNome)))
            {
                foreach (var arquivo in arquivos)
                {
                    var linhas = this.AbrirArquivo(arquivo);

                    foreach (var linha in linhas)
                    {
                        this.IncluirLinhaArquivo(sw, linha);
                    }

                    this.IncluirLinhaArquivo(sw, string.Empty);
                }
            }
        }

        public void VerificarDiretorio(string caminho)
        {
            if (!Directory.Exists(caminho))
                throw new DirectoryNotFoundException();

        }
    }
}
