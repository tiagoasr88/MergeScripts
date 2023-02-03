using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeScripts.Services.Interfaces
{
    public interface IMergeScriptService
    {
        void VerificarDiretorio(string caminho);

        IEnumerable<string> ListarArquivosDiretorio(string caminho);

        string[] AbrirArquivo(string arquivo);

        StreamWriter CriarArquivo(string arquivoDestino);

        void IncluirLinhaArquivo(StreamWriter sw, string linha);

        void RealizarMergeArquivos(string caminho, string novoArquivoNome);
    }
}
