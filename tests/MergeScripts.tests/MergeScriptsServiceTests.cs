using MergeScripts.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeScripts.tests
{
    public class MergeScriptsServiceTests
    {
        private MergeScriptsService _service { get; set; }

        public MergeScriptsServiceTests()
        {
            _service = new MergeScriptsService();
        }

        [Fact]
        public void DeveVerificarSeDiretorioExisteComSucesso()
        {
            const string caminhoExistente = @"C:\Windows";

            _service.VerificarDiretorio(caminhoExistente);
        }

        [Fact]
        public void DeveRetornarErroQuandoDiretorioNaoExistir()
        {
            const string caminhoInexistente = @"C:\WindowsNaoExistente09182";

            Assert.ThrowsAny<DirectoryNotFoundException>(() => _service.VerificarDiretorio(caminhoInexistente));
        }

        [Fact]
        public void DeveListarOsArquivosDoDiretorioComSucesso()
        {
            const string caminho = @".\Files\";
            var arquivos = _service.ListarArquivosDiretorio(caminho);

            Assert.Equal(3, arquivos.Count());
        }

        [Fact]
        public void DeveRetornarErroQuandoNaoEncontrarArquivosNoDiretorio()
        {
            var caminho = @".\FilesInexistentes\";

            Assert.ThrowsAny<DirectoryNotFoundException>(() => _service.ListarArquivosDiretorio(caminho));
        }

        [Fact]
        public void DeveAbrirArquivoComSucesso()
        {
            var arquivo = @".\Files\File1.sql";

            var linhas = _service.AbrirArquivo(arquivo);

            Assert.Equal(4, linhas.Count());
        }

        [Fact]
        public void DeveCriarNovoArquivoComSucesso()
        {
            var arquivoDestino = @".\Files\novo_arquivo.sql";

            using (var sw = _service.CriarArquivo(arquivoDestino)) { }

            File.Delete(arquivoDestino);

        }

        [Fact]
        public void DeveIncluirConteudoNoArquivoComSucesso()
        {
            var arquivoDestino = @".\Files\novo_arquivo.sql";

            const string linha = @"SELECT * FROM teste";

            using (var sw = _service.CriarArquivo(arquivoDestino))
                _service.IncluirLinhaArquivo(sw, linha);

            var linhas = _service.AbrirArquivo(arquivoDestino);

            File.Delete(arquivoDestino);

            Assert.Equal(linha, linhas[0]);
        }

        [Fact]
        public void DeveLancarErroSeNaoEncontrarArquivos()
        {
            const string caminho = @".\Files\EmpytFiles\";

            const string arquivoNome = @"Script.sql";

            Directory.CreateDirectory(caminho);

            Assert.Throws<FileNotFoundException>(() => _service.RealizarMergeArquivos(caminho, arquivoNome));

            Directory.Delete(caminho);
        }

        [Fact]
        public void DeveFazerMergeDosArquivosComSucesso()
        {
            const string caminho = @".\Files\";
            const string arquivoNome = @"Script.sql";

            var arquivoFullName = Path.Combine(caminho, arquivoNome);

            _service.RealizarMergeArquivos(caminho, arquivoNome);

            var linhas = _service.AbrirArquivo(arquivoFullName);

            File.Delete(arquivoFullName);

            Assert.Equal(15, linhas.Count());

        }
    }
}
