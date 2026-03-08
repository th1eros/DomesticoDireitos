# 💰 Cálculo de FGTS e Antecipação de Multa (Lei 150/2015)

Este documento define a regra de cálculo para o Fundo de Garantia do Trabalhador Doméstico.

## 📋 Regra de Negócio (Backend .NET 8)
Para garantir a conformidade com a eSocial, o cálculo é unificado:
* **FGTS Mensal:** 8% sobre o salário bruto.
* **Antecipação da Multa Rescisória:** 3,2% sobre o salário bruto.
* **Alíquota Total:** **11,2%**.

## 🧮 Fórmula de Cálculo
> `Valor_FGTS = Salario_Base * 0.112`

## 🎨 Orientação para o Front-end
Exiba o valor total do FGTS de forma destacada, pois é um custo direto para o empregador e um direito acumulado para o empregado.