using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BugzillaWebDriver.ComponentHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace EvalDeux.Features;

[Binding]
public sealed class Validate
{
    private CreditCard _creditCard = new CreditCard();
    // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

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

        _creditCard.CardNumber = Int16.Parse(cardNumber);;
        _creditCard.ExpiryDate = expiryDate;
        _creditCard.CVV = cvv;

        TextBoxHelper.TypeInTextBox(By.Id("creditCardNumber"), cardNumber);
        TextBoxHelper.TypeInTextBox(By.Id("expirationDate"), expiryDate);
        TextBoxHelper.TypeInTextBox(By.Id("cvc"), cvv);
    }

    [Then(@"User should be at paymentConfirmed page")]
    public void ThenUserShouldBeAtPaymentConfirmedPage()
    {
        Assert.AreEqual("/paymentConfirmed", PageHelper.GetPageTitle());
    }

    [When(@"click on payer button")]
    public void WhenClickOnPayerButton()
    {
        Assert.AreEqual(7, _creditCard.ExpiryDate.Length);
        Assert.AreEqual(3, _creditCard.CVV.Length);


        DateTime dateTime = DateTime.Now;

        DateTime expiryDateTime = DateTime.Parse(_creditCard.ExpiryDate);
        Assert.IsTrue(expiryDateTime > dateTime);

        ButtonHelper.ClickButton(By.Id("submitCard"));
        Assert.IsTrue(PageHelper.GetPageUrl() == "/paymentConfirmed");
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
}