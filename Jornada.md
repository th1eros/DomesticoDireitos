# ⏳ Regras de Jornada de Trabalho e Horas Extras

Definição dos limites de tempo e adicionais conforme a legislação vigente.

## 📋 Regra de Negócio (Backend .NET 8)
* **Limite Semanal:** 44 horas.
* **Limite Diário:** 8 horas (regra geral).
* **Adicional de Hora Extra:** Mínimo de 50% sobre o valor da hora comum.

## 🧮 Lógica de Validação
O sistema verifica se a soma de horas enviada pelo Front-end ultrapassa 44h. Caso ultrapasse, o cálculo do excedente deve aplicar o fator `1.5`.

## 🎨 Orientação para o Front-end
Use um componente de "alerta" ou cor diferenciada (ex: Laranja) quando o usuário inserir horas que ultrapassem o limite legal, para evitar passivos trabalhistas.