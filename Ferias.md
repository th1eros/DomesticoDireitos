# 🏖️ Cálculo de Férias e Terço Constitucional

Regras para o período de descanso remunerado do empregado doméstico.

## 📋 Regra de Negócio (Backend .NET 8)
Após 12 meses de trabalho (período aquisitivo), o trabalhador tem direito a 30 dias de descanso.
* **Base de Cálculo:** Salário integral + 1/3 do salário (Abono Constitucional).

## 🧮 Fórmula de Cálculo
> `Valor_Ferias = Salario_Base + (Salario_Base / 3)`

## 🎨 Orientação para o Front-end
Ao exibir o resultado, separe o valor do "Salário de Férias" do "Terço Constitucional". Isso ajuda o utilizador leigo a entender de onde vem o valor total recebido.