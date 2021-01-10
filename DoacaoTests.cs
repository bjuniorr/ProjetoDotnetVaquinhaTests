using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Vaquinha.Tests.Common.Fixtures;
using Xunit;

namespace Vaquinha.AutomatedUITests
{
	public class DoacaoTests : IDisposable, IClassFixture<DoacaoFixture>, 
                                               IClassFixture<EnderecoFixture>, 
                                               IClassFixture<CartaoCreditoFixture>
	{
		private DriverFactory _driverFactory = new DriverFactory();
		private IWebDriver _driver;

		private readonly DoacaoFixture _doacaoFixture;
		private readonly EnderecoFixture _enderecoFixture;
		private readonly CartaoCreditoFixture _cartaoCreditoFixture;

		public DoacaoTests(DoacaoFixture doacaoFixture, EnderecoFixture enderecoFixture, CartaoCreditoFixture cartaoCreditoFixture)
        {
            _doacaoFixture = doacaoFixture;
            _enderecoFixture = enderecoFixture;
            _cartaoCreditoFixture = cartaoCreditoFixture;
        }
		public void Dispose()
		{
			_driverFactory.Close();
		}

		[Fact]
		public void DoacaoUI_AcessoTelaHome()
		{
			// Arrange
			_driverFactory.NavigateToUrl("https://vaquinha.azurewebsites.net/");
			_driver = _driverFactory.GetWebDriver();

			// Act
			IWebElement webElement = null;
			webElement = _driver.FindElement(By.ClassName("vaquinha-logo"));

			// Assert
			webElement.Displayed.Should().BeTrue(because:"logo exibido");
		}
		[Fact]
		public void DoacaoUI_CriacaoDoacao()
		{
			//Arrange
			var doacao = _doacaoFixture.DoacaoValida();
            doacao.AdicionarEnderecoCobranca(_enderecoFixture.EnderecoValido());
            doacao.AdicionarFormaPagamento(_cartaoCreditoFixture.CartaoCreditoValido());
			_driverFactory.NavigateToUrl("https://vaquinha.azurewebsites.net/");
			_driver = _driverFactory.GetWebDriver();

			//Act
			IWebElement webElement = null;
			webElement = _driver.FindElement(By.ClassName("btn-yellow"));
			webElement.Click();

			//Assert
			_driver.Url.Should().Contain("/Doacoes/Create");
		}
		[Fact]
		public void DoacaoUI_ConclusaoDoacao()
		{
			//Arrange
			var doacao = _doacaoFixture.DoacaoValida();
            doacao.AdicionarEnderecoCobranca(_enderecoFixture.EnderecoValido());
            doacao.AdicionarFormaPagamento(_cartaoCreditoFixture.CartaoCreditoValido());
			_driverFactory.NavigateToUrl("https://vaquinha.azurewebsites.net/");
			_driver = _driverFactory.GetWebDriver();

			//Act

			IWebElement clickInicial = null;
			clickInicial = _driver.FindElement(By.ClassName("btn-yellow"));
			clickInicial.Click();

			IWebElement campoValor = null;
			campoValor = _driver.FindElement(By.Id("valor"));
			campoValor.SendKeys(doacao.Valor.ToString());

			IWebElement campoNome = null;
			campoNome = _driver.FindElement(By.Id("DadosPessoais_Nome"));
			campoNome.SendKeys(doacao.DadosPessoais.Nome);

			IWebElement campoEmail = null;
			campoEmail = _driver.FindElement(By.Id("DadosPessoais_Email"));
			campoEmail.SendKeys(doacao.DadosPessoais.Email);

			IWebElement campoMensagemApoio = null;
			campoMensagemApoio = _driver.FindElement(By.Id("DadosPessoais_MensagemApoio"));
			campoMensagemApoio.SendKeys(doacao.DadosPessoais.MensagemApoio);

			IWebElement campoEnderecoCobranca = null;
			campoEnderecoCobranca = _driver.FindElement(By.Id("EnderecoCobranca_TextoEndereco"));
			campoEnderecoCobranca.SendKeys(doacao.EnderecoCobranca.TextoEndereco);

			IWebElement campoEnderecoCobrancaNumero = null;
			campoEnderecoCobrancaNumero = _driver.FindElement(By.Id("EnderecoCobranca_Numero"));
			campoEnderecoCobrancaNumero.SendKeys(doacao.EnderecoCobranca.Numero);

			IWebElement campoEnderecoCobrancaCidade = null;
			campoEnderecoCobrancaCidade = _driver.FindElement(By.Id("EnderecoCobranca_Cidade"));
			campoEnderecoCobrancaCidade.SendKeys(doacao.EnderecoCobranca.Cidade);
			
			IWebElement campoEnderecoCobrancaEstado = null;
			campoEnderecoCobrancaEstado = _driver.FindElement(By.Id("estado"));
			campoEnderecoCobrancaEstado.SendKeys(doacao.EnderecoCobranca.Estado);
			
			IWebElement campoEnderecoCobrancaCep = null;
			campoEnderecoCobrancaCep = _driver.FindElement(By.Id("cep"));
			campoEnderecoCobrancaCep.SendKeys(doacao.EnderecoCobranca.CEP);

			IWebElement campoEnderecoCobrancaComplemento = null;
			campoEnderecoCobrancaComplemento = _driver.FindElement(By.Id("EnderecoCobranca_Complemento"));
			campoEnderecoCobrancaComplemento.SendKeys(doacao.EnderecoCobranca.Complemento);

			IWebElement campoEnderecoCobrancaTelefone = null;
			campoEnderecoCobrancaTelefone = _driver.FindElement(By.Id("telefone"));
			campoEnderecoCobrancaTelefone.SendKeys(doacao.EnderecoCobranca.Telefone);

			IWebElement campoFormaPagamentoNome = null;
			campoFormaPagamentoNome = _driver.FindElement(By.Id("FormaPagamento_NomeTitular"));
			campoFormaPagamentoNome.SendKeys(doacao.FormaPagamento.NomeTitular);

			IWebElement campoFormaPagamentoNumeroCartao = null;
			campoFormaPagamentoNumeroCartao = _driver.FindElement(By.Id("cardNumber"));
			campoFormaPagamentoNumeroCartao.SendKeys(doacao.FormaPagamento.NumeroCartaoCredito);

			IWebElement campoFormaPagamentoDataValidade = null;
			campoFormaPagamentoDataValidade = _driver.FindElement(By.Id("validade"));
			campoFormaPagamentoDataValidade.SendKeys(doacao.FormaPagamento.Validade);

			IWebElement campoFormaPagamentoCvv = null;
			campoFormaPagamentoCvv = _driver.FindElement(By.Id("cvv"));
			campoFormaPagamentoCvv.SendKeys(doacao.FormaPagamento.CVV);

			IWebElement botaoDoacao = null;
			botaoDoacao = _driver.FindElement(By.ClassName("btn-yellow"));
			botaoDoacao.Click();

			//Assert
			_driver.Url.Should().BeEquivalentTo("https://vaquinha.azurewebsites.net/");
		}
	}
}