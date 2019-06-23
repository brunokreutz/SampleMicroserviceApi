# SimpleMicroserviceApi

A sample Microservice API implementation with test cases.

[Português](#regra-de-negócio)

[English](#business-rule)

## Regra de negócio:
Efetuar um pagamento junto com o débito em uma conta corrente e o credito em outra conta.
O valor líquido a ser creditado deve ser calculado em cima das seguintes taxas:


Número de parcelas | Taxa
------------- | -------------
1  | 3,79 %
2  | 5,78 %
3  | 7,77 %

#### Parâmetros de entrada:
Valor,
Parcelas,
Conta origem,
Conta destino.

#### Parâmetros de saída:
http status code,
Valor líquido,
Saldo conta origem,
Saldo conta destino.

## Estrutura
#### Lançamentos (Installments)
Os lançamentos correspondem as operações de credito ou debito das parcelas na conta corrente. Caso o numero de parcelas for maior
do que 1, o mes da data do lançamento é incrementado a cada nova parcela.   
#### Conta Corrente (CheckingAccount)
A conta corrente corresponde a conta do cliente.
#### Pagamento (Payment)
O pagamento representa a transação entre os clientes. A taxa das parcelas é incorporada ao valor do pagamento.
#### Taxas (Fee)
Taxas correspondentes ao número de parcelas.

## Business rule:
Make a payment along with the debit in a checking account and the credit in another account.
A payment can be break down in several pieces (portions). These portions are debited/credited in the following months.
The net amount to be credited must be calculated over the following rates:

Number of Installments | Fee 
------------- | -------------
1 | 3.79%
2 | 5.78%
3 | 7.77%

#### Input Parameters:
Value,
Portions,
Source account,
Destination account.

#### Output Parameters:
http status code,
Net value (Transaction value after fees),
Source account balance,
Destination account balance.

## Structure
#### Entry
The entries correspond to the credit or debit transactions of the installments in the current account. If the number of installments is greater
than 1, the month of the transaction date is incremented with each new installment.
#### CheckingAccount
The current account corresponds to the customer's account.
#### Payment 
Payment represents the transaction between customers. The installment rate is incorporated into the payment amount.
#### Fee 
Rates corresponding to the number of installments.

		
			

