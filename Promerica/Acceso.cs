using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;

namespace Promerica
{
    [TestClass]
    public class Acceso
    {
        [TestMethod]
        public void TestMethod()
        {
            try
            {
                //Se inicializa la instancia WebDriver
                IWebDriver driver = new ChromeDriver();

                //Abrir la pagina en Chrome
                driver.Navigate().GoToUrl("https://www.clubpromerica.com/costarica/");

                //Maximizar la pantalla
                driver.Manage().Window.Maximize();

                String title = driver.Title;
                Assert.AreEqual(title, "Club Promerica");

                
                //Navegar al menu de contactenos
                driver.FindElement(By.XPath("//*[@id=\"headerMenuParent\"]/div/ul[1]/li[6]/a")).Click();

                //Llenar formulario contactenos

                //Se ingresa el nombre completo
                driver.FindElement(By.Id("FullName")).SendKeys("Victor");

                //Se ingresa el correo electronico
                driver.FindElement(By.Id("Email")).SendKeys("vgranados_cr_90@hotmail.com");

                //Se ingresar comentario
                driver.FindElement(By.Id("Enquiry")).SendKeys("Esto es una prueba de examen.");

                //Presionar el boton de enviar
                driver.FindElement(By.Name("send-email")).Click();

                //Se valida el envio exitoso del formulario
                String confirm = driver.FindElement(By.ClassName("result")).Text;
                Assert.AreEqual(confirm, "Su comentario ha sido enviado con éxito al propietario de la tienda.");

                System.Threading.Thread.Sleep(3000);

                //Navegar al formulario de Registro
                driver.FindElement(By.Id("header-links-opener")).Click();
                driver.FindElement(By.XPath("/html/body/div[7]/div[1]/div/div[2]/div[1]/div[3]/div[2]/div/ul/li[1]/a")).Click();

                //Se validan contenido del formulario de registro

                //Se valida el titulo del formulario
                String titleRegister = driver.FindElement(By.XPath("/html/body/div[7]/div[3]/div[3]/div/form/div/div[2]/div/div[1]/strong")).Text;
                Assert.AreEqual(titleRegister, "Sus datos personales");

                //Se valida que el textbox de nombre este visible
                IWebElement txtNombre = driver.FindElement(By.Id("FirstName"));
                bool isVisible = txtNombre.Displayed;
                Console.WriteLine(isVisible);

                //Se captura la imagen del registro
                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();

                //Se guarda la imagen en el archivo
                string rutaArchivo = Path.Combine(Environment.CurrentDirectory, "screenregister.png");
                screenshot.SaveAsFile(rutaArchivo);

                Console.WriteLine($"La imagen fue guardada en: {rutaArchivo}");

                System.Threading.Thread.Sleep(3000);

                // Verificar que driver no es null antes de cerrar
                if (driver != null)
                {
                    driver.Close();
                    driver.Dispose();  // Opcional: libera recursos asociados
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
