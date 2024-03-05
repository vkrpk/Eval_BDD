using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using BugzillaWebDriver.BaseClasses;
using BugzillaWebDriver.ComponentHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace EvalDeux.Features;

[Binding]
public sealed class Validate
{
    private readonly ScenarioContext _scenarioContext;

    public Validate(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [Given(@"user is at home page")]
    public void GivenUserIsAtHomePage()
    {
        NavigationHelper.NavigateToHomePage();
    }

    [When(@"provide the (.*) and (.*) and (.*)")]
    public void WhenProvideTheAndAnd(string cardNumber, string expiryDate, string cvv)
    {
        Assert.IsTrue(Int16.TryParse(cardNumber, out _));
        Assert.AreEqual(16, cardNumber.Length);

        // _creditCard.CardNumber = Int16.Parse(cardNumber);;

        TextBoxHelper.TypeInTextBox(By.Id("creditCardNumber"), cardNumber);
        TextBoxHelper.TypeInTextBox(By.Id("expirationDate"), expiryDate);
        TextBoxHelper.TypeInTextBox(By.Id("cvc"), cvv);
    }

    [Then(@"User should be at paymentConfirmed page")]
    public void ThenUserShouldBeAtPaymentConfirmedPage()
    {
        Assert.AreEqual("Paiement confirmé", PageHelper.GetPageTitle());
    }

    [When(@"click on payer button")]
    public void WhenClickOnPayerButton()
    {
        Assert.AreEqual(7, ObjectRepository.Config.GetExpiryDate().Length);
        Assert.AreEqual(3, ObjectRepository.Config.GetCVV().Length);


        DateTime dateTime = DateTime.Now;
        DateTime expiryDateTime = DateTime.ParseExact(ObjectRepository.Config.GetExpiryDate(), "MM/yyyy", CultureInfo.InvariantCulture);
        // DateTime expiryDateTime = DateTime.Parse(ObjectRepository.Config.GetExpiryDate());
        Assert.IsTrue(expiryDateTime > dateTime);

        ButtonHelper.ClickButton(By.Id("submitCard"));
        NavigationHelper.NavigateToUrl("http://localhost/paymentConfirmed.html");
        Assert.IsTrue(PageHelper.GetPageUrl() == "http://localhost/paymentConfirmed.html");
        Assert.AreEqual(PageHelper.GetPageTitle(), "Paiement confirmé");
    }

    [Given(@"elements are present")]
    public void GivenElementsArePresent()
    {
        Assert.IsTrue(GenericHelper.IsElementPresentOnce(By.Id("creditCardNumber")));
        Assert.IsTrue(GenericHelper.IsElementPresentOnce(By.Id("expirationDate")));
        Assert.IsTrue(GenericHelper.IsElementPresentOnce(By.Id("cvc")));
        Assert.IsTrue(GenericHelper.IsElementPresentOnce(By.Id("submitCard")));
    }

    [When(@"provide the information")]
    public void WhenProvideTheInformation()
    {
        // Console.WriteLine(ObjectRepository.Config);
        // Console.WriteLine(ObjectRepository.Driver);
    }
}