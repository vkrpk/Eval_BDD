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
        TextBoxHelper.TypeInTextBox(By.Id("creditCardNumber"), ObjectRepository.Config.GetCardNumber());
        TextBoxHelper.TypeInTextBox(By.Id("expirationDate"), ObjectRepository.Config.GetExpiryDate());
        TextBoxHelper.TypeInTextBox(By.Id("cvc"), ObjectRepository.Config.GetCVV());
    }

    [When(@"verify the information is valid")]
    public void WhenVerifyTheInformationIsValid()
    {
        Assert.IsTrue(Int64.TryParse(TextBoxHelper.GetTextBoxValue(By.Id("creditCardNumber")), out _));
        Assert.IsTrue(Int16.TryParse(ObjectRepository.Config.GetCVV(), out _));
        Assert.AreEqual(16, ObjectRepository.Config.GetCardNumber().Length);
        Assert.AreEqual(7, ObjectRepository.Config.GetExpiryDate().Length);
        Assert.AreEqual(3, ObjectRepository.Config.GetCVV().Length);

        DateTime dateTime = DateTime.Now;
        DateTime expiryDateTime = DateTime.ParseExact(ObjectRepository.Config.GetExpiryDate(), "MM/yyyy", CultureInfo.InvariantCulture);
        Assert.IsTrue(expiryDateTime > dateTime);
    }

    [When(@"click on payer button")]
    public void WhenClickOnPayerButton()
    {
        ButtonHelper.ClickButton(By.Id("submitCard"));
    }

    [Then(@"User should be at paymentConfirmed page")]
    public void ThenUserShouldBeAtPaymentConfirmedPage()
    {
        Assert.AreEqual("Paiement confirmé", PageHelper.GetPageTitle());
    }

    [When(@"provide the (.*) and (.*) and (.*)")]
    [Ignore]
    public void WhenProvideTheAndAnd(string cardNumber, string expiryDate, string cvv)
    {
        Assert.IsTrue(Int16.TryParse(cardNumber, out _));
        Assert.AreEqual(16, cardNumber.Length);

        TextBoxHelper.TypeInTextBox(By.Id("creditCardNumber"), cardNumber);
        TextBoxHelper.TypeInTextBox(By.Id("expirationDate"), expiryDate);
        TextBoxHelper.TypeInTextBox(By.Id("cvc"), cvv);
    }
}