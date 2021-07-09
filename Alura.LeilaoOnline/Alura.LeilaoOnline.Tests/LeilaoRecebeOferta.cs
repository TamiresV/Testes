using Xunit;
using Alura.LeilaoOnline.Core;
using System.Linq;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoRecebeOferta
    {
        [Fact]
        public void NaoAceitaLanceProximoLanceDadoMesmoClienteRealizouUltimoLance()
        {
            //Arranje - cenário
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);

            leilao.IniciaPregao();
            leilao.RecebeLance(fulano, 800);

            //Act - método sob teste
            leilao.RecebeLance(fulano, 1000);

            //Assert
            var qtdEsperada = 1;
            var qtdObtido = leilao.Lances.Count();

            Assert.Equal(qtdEsperada, qtdObtido);
        }

        [Theory]
        [InlineData(4, new double[] {100, 1200, 1400, 1300})]
        [InlineData(2, new double[] {800, 900})]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado(int qtdEsperada, double[] ofertas)
        {
            //Arranje - cenário
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.IniciaPregao();

            for(int i = 0; i< ofertas.Length; i++)
            {
                if(i % 2 == 0)
                {
                    leilao.RecebeLance(fulano, ofertas[i]);
                }
                else
                {
                    leilao.RecebeLance(maria, ofertas[i]);
                }
            }
            leilao.TerminaPregao();

            //Act - método sob teste
            leilao.RecebeLance(fulano, 1000);

            //Assert
            var qtdObtido = leilao.Lances.Count();

            Assert.Equal(qtdEsperada, qtdObtido);
        }
    }
}
