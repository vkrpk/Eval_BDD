Feature: Validate

@mytag
Scenario: Validate a credit card number
	Given user is at home page
	And elements are present
#	When provide the <cardNumber> and <expiryDate> and <cvv>
	When provide the information
	And click on payer button
	Then User should be at paymentConfirmed page

#	Examples:
#	  | cardNumber       | expiryDate | cvv |
#	  | 1234567891234567 | 12/2025      | 123 |
#	  | 9876543219876543 | 21/2026      | 321 |