# BankManagement

---

![Imágen de las Entidades](./img/Bank.jpg)

## Diagrama Entidad Relación
![ER Diagram](./img/ERBank.png)

---

## Requerimientos del Sistema
- [x] 1. "Creación de la Entidad "Plazo y Tasa de Interés"


- [x] 2. "Simulador de Cuota"

`POST` `/api/SimulateLoan/Simulate-Credit`

```json
{
  "amount": 100000,
  "months": 6
}
```

`Simulate Credit Response`
```json
{
  "interestRate": 9.15,
  "monthyPayment": 17114.309416309,
  "totalPayment": 102685.856497854
}
```