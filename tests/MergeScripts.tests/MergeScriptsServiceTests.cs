using MergeScripts.Services.Services;

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

            string[] linhasOrigem = new string[] { @"SELECT * FROM teste", "GO" };

            _service.IncluirLinhasArquivo(arquivoDestino, linhasOrigem);

            var linhasDestino = _service.AbrirArquivo(arquivoDestino);

            File.Delete(arquivoDestino);

            Assert.Equal(linhasOrigem, linhasDestino);
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

        [Fact]
        public void DeveAdicionarLinhaVaziaNoFimDaListaQuandoUltimaLinhaNaoForVazia()
        {
            var arr = new string[] { "linha1", "linha2" };

            _service.AdicionarLinhaRodape(ref arr);

            Assert.Equal(3, arr.Count());
            Assert.Equal(string.Empty, arr.Last());
        }

        [Fact]
        public void NaoDeveAdicionarLinhaVaziaNoFimDaLista()
        {
            var arr = new string[] { "linha1", "linha2", "" };

            _service.AdicionarLinhaRodape(ref arr);

            Assert.Equal(3, arr.Count());
            Assert.Equal(string.Empty, arr.Last());
        }

        [Fact]
        public void DeveValidarArquivosEncontradosComSucesso()
        {
            const string caminho = @".\Files\File1.sql";

            var arquivos = new string[] { caminho };
            _service.ValidarArquivosEncontrados(arquivos);
        }

        [Fact]
        public void DeveDefinirNomeAquivoDestinoComSucesso()
        {
            const string caminho = @"c:\teste";
            const string novoArquivoNome = "teste.sql";

            var resultado = _service.DefinirNomeNovoArquivo(caminho, novoArquivoNome);

            Assert.Equal(Path.Combine(caminho, novoArquivoNome), resultado);
        }
    }
}
