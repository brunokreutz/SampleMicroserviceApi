# SampleMicroserviceApi

A sample Microservice API implementation with test cases.

[Português](#regra-de-negócio)
- [Regra de Negócio](#regra-de-negócio)
- [Dominio](#dominio)
- [Repostas](#respostas)

[English](#business-rule)
- [Business Rule](#business-rule)
- [Domain](#domain)

## Regra de negócio:
Efetuar um pagamento junto com o débito em uma conta corrente e o credito em outra conta.
O valor líquido a ser creditado deve ser calculado em cima das seguintes taxas:


Número de parcelas | Taxa
------------- | -------------
1  | 3,79 %
2  | 5,78 %
3  | 7,77 %

#### Parâmetros de entrada:
Valor (double Amount),
Parcelas (int NumberOfInstallments),
Conta Orgiem (int SourceAccountId),
Conta Destino (int DestinationAccountId).

#### Parâmetros de saída:
http status code,
Valor líquido,
Saldo conta origem,
Saldo conta destino.

## Domínio
#### Lançamentos (Installments)
Os lançamentos correspondem as operações de credito ou debito das parcelas na conta corrente. Caso o numero de parcelas for maior
do que 1, o mes da data do lançamento é incrementado a cada nova parcela.   
#### Conta Corrente (CheckingAccount)
A conta corrente corresponde a conta do cliente.
#### Pagamento (Payment)
O pagamento representa a transação entre os clientes. A taxa das parcelas é incorporada ao valor do pagamento.
#### Taxas (Fee)
Taxas correspondentes ao número de parcelas.

## Respostas
 - O que é domain driven design e sua importância na estratégia de desenvolvimento de software?
 
Domain driven design é uma filosofia de desenvolvimento de software que considera as regras de negócio como a parte mais importante do desenvolvimento. Ou seja, o desenvolvimento deve ter em vista as regras de negócio como peças fundamentais da arquitetura do projeto, elas são a base para tudo que será construído. Tendo como foco o domínio do problema para o desenvolvimento, deve existir uma colaboração intensa entre os experts da área e os de tecnologia para que se possa chegar o mais perto possível do cerne do problema.

- O que é e como funciona uma arquitetura baseada em microservices? Explique ganhos com este modelo, desafios em sua implementação e desvantagens.

Uma arquitetura baseada em microservices utiliza pequenos serviços independentes para agilizar o processo de desenvolvimento, aumentar a capacidade de teste e desacoplar o código. Os micro serviços proporcionam uma maior versatilidade no desenvolvimento de aplicações grandes e complexas, facilitando a incrementação da pilha de desenvolvimento e sua atualização por novas tecnologias. Outra vantagem é a sua maior escalabilidade. Uma de suas desvantagens é o aumento da complexidade, quanto mais o número de tecnologias diferentes na pilha cresse, mais difícil fica a sua manutenção. Além disso, manter a consistência dos dados pode se tornar um problema. 

- Qual a diferença entre processamento síncrono e assíncrono e qual o melhor cenário para utilizar um ou outro? 

Processamento síncrono ocorre de maneira sequencial, este modelo "trava" a execução do programa até que a requisição seja atendida. Já no processamento assíncrono não necessariamente existe esse bloqueio, o programa pode continuar sua execução movendo para outras tarefas enquanto o resultado da requisição não chega. Quando o resultado de uma operação não interfere na outra você pode optar por utilizar o processamento assíncrono, como no caso de recuperar vários objetos de apis diferentes ao mesmo tempo, o que provavelmente acarretará num ganho de velocidade de processamento. Entretanto se alguma dessas consultas depender de outra, as que forem interdependentes devem ser processadas de maneira síncrona.

#
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
Amount (double Amount),
Number Of Installments (int NumberOfInstallments),
Source Account (int SourceAccountId),
Destination Account (int DestinationAccountId).

#### Output Parameters:
http status code,
Net value (Transaction value after fees),
Source account balance,
Destination account balance.

## Domain
#### Entry
The entries correspond to the credit or debit transactions of the installments in the current account. If the number of installments is greater
than 1, the month of the transaction date is incremented with each new installment.
#### CheckingAccount
The current account corresponds to the customer's account.
#### Payment 
Payment represents the transaction between customers. The installment rate is incorporated into the payment amount.
#### Fee 
Rates corresponding to the number of installments.

		
			

